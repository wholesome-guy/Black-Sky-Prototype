using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class AsteroidTow : MonoBehaviour
{
    private Rigidbody Player_RigidBody;
    private Transform Player_Transform;

    private Rigidbody Asteroid_RigidBody;
    private AsteroidScript Asteroid_Script;
    public float Asteroid_Mass;

    private float Kinetic_Energy;
    public float Tension_On_Tether;

    


    public static Action Tow_Joint_Broke;

    private float Minimum_Distance_Player_Asteroid;
    private float Maximum_Distance_Player_Asteroid;

    private float Maximum_Tether_Tension;
    private float Velocity_Tension_Constant;
    private float Asteroid_Tension_Constant;

    private float Max_Force_Multiplier;

    private float Delay_Duration;

    private float Rope_Length;
    private float Rope_Spring_Constant;

    private bool Is_Tow_Joint_Broke = false;
    

    private void Start()
    {
        Player_Transform = PlayerSingleton.instance.Player_Transform;
        Player_RigidBody = PlayerSingleton.instance.Player_Rigidbody;

        Asteroid_RigidBody = gameObject.GetComponent<Rigidbody>();

        Asteroid_Script = gameObject.GetComponent<AsteroidScript>();

        #region Asigning Values
        Asteroid_Mass = Asteroid_Script.Asteroid_Mass;
        Minimum_Distance_Player_Asteroid = Asteroid_Script.Minimum_Distance_Player_Asteroid;
        Maximum_Distance_Player_Asteroid = Asteroid_Script.Maximum_Distance_Player_Asteroid;

        Maximum_Tether_Tension = Asteroid_Script.Maximum_Tether_Tension;
        Velocity_Tension_Constant = Asteroid_Script.Velocity_Tension_Constant;
        Asteroid_Tension_Constant = Asteroid_Script.Asteroid_Tension_Constant;

        Max_Force_Multiplier = Asteroid_Script.Max_Force_Multiplier;

        Delay_Duration = Asteroid_Script.Delay_Duration;

        Rope_Length = Asteroid_Script.Rope_Length;
        Rope_Spring_Constant = Asteroid_Script.Rope_Spring_Constant;
        #endregion


        Is_Tow_Joint_Broke = false;
        
    }

    private void FixedUpdate()
    {
        if(Tension_On_Tether > Maximum_Tether_Tension && Is_Tow_Joint_Broke == false)
        {
            TimerManager.Timer_Delay_Event.Invoke(Delay_Duration);
            StartCoroutine(Delay_Break_Joint(Delay_Duration));
            Is_Tow_Joint_Broke = true;
        }

        float Distance = Vector3.Distance(Player_Transform.position, gameObject.transform.position);

        Kinetic_Energy_Function();

        if(Distance > Minimum_Distance_Player_Asteroid && Distance < Maximum_Distance_Player_Asteroid)
        {
            Tow_Asteroid(ForceMode.Force);
        }
        else if(Distance > Maximum_Distance_Player_Asteroid)
        {
            Tow_Asteroid(ForceMode.Impulse);
        }
        Tension_Calculator();


    }

    private void Tension_Calculator()
    {
        float Velocity = Player_RigidBody.velocity.magnitude;

        float Tension_Due_To_Motion = Velocity * Velocity_Tension_Constant;

        float Tension_Due_To_Asteroid = Asteroid_Mass * Asteroid_Tension_Constant;

        #region Stretch Tension

        float Distance = (gameObject.transform.position - Player_Transform.position).magnitude;

        float Tension_Due_To_Stretch = 0;

        if (Distance>Rope_Length)
        {
            float Stretch_Amount = Distance - Rope_Length;

            Tension_Due_To_Stretch = Stretch_Amount * Rope_Spring_Constant;
        }
        else
        {
            Tension_Due_To_Stretch = 0;
        }

        #endregion

        Tension_On_Tether = Tension_Due_To_Motion + Tension_Due_To_Stretch + Tension_Due_To_Asteroid;
    }

    private void Kinetic_Energy_Function()
    {
        float Kinetic_Energy_Local = (0.5f) * (Asteroid_Mass) *Mathf.Pow(Asteroid_RigidBody.velocity.magnitude - Player_RigidBody.velocity.magnitude, 2);
        float Max_Kinetic_Energy = Asteroid_Mass * Max_Force_Multiplier;
        Kinetic_Energy = Mathf.Clamp(Kinetic_Energy_Local, 0 , Max_Kinetic_Energy);
    }

    private void Tow_Asteroid(ForceMode Force_Mode)
    {
        Vector3 Direction  = (Player_Transform.position - gameObject.transform.position).normalized;

        Asteroid_RigidBody.AddForce(Direction * Kinetic_Energy, Force_Mode);
        Asteroid_RigidBody.AddTorque(Direction * Kinetic_Energy, Force_Mode);
    }

    

    private void Break_Tow_Joint()
    {
        Debug.Log("Jnt_Broke");
        Tow_Joint_Broke.Invoke();
        Asteroid_Script.Destroy_Anchors();

        Destroy_Tow_Script();
    }
    public void Destroy_Tow_Script()
    {
        Destroy(this);
    }

    private IEnumerator Delay_Break_Joint(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        Break_Tow_Joint();

    }

}
