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

    private float Kinetic_Energy;

    public static Action Tow_Joint_Broke;

    private float Joint_Break_Velocity_Player;

    private bool Is_Tow_Joint_Broke = false;
    

    private void Start()
    {
        Player_Transform = PlayerSingleton.instance.Player_Transform;
        Player_RigidBody = PlayerSingleton.instance.Player_Rigidbody;

        Asteroid_RigidBody = gameObject.GetComponent<Rigidbody>();

        Asteroid_Script = gameObject.GetComponent<AsteroidScript>();

        Joint_Break_Velocity_Player = AsteroidData.Joint_Break_Velocity_Player(Asteroid_Script.Asteroid_Mass);

        Is_Tow_Joint_Broke = false;
    }

    private void FixedUpdate()
    {
        if(Player_RigidBody.velocity.magnitude > Joint_Break_Velocity_Player && Is_Tow_Joint_Broke == false)
        {
            TimerManager.Timer_Delay_Event.Invoke(AsteroidData.Joint_Break_Delay_Duration);
            StartCoroutine(Delay_Break_Joint(AsteroidData.Joint_Break_Delay_Duration));
            Is_Tow_Joint_Broke = true;
        }

        float Distance = Vector3.Distance(Player_Transform.position, gameObject.transform.position);

        Kinetic_Energy_Function();

        if(Distance > AsteroidData.Minimum_Distance_Player_Asteroid && Distance < AsteroidData.Maximum_Distance_Player_Asteroid)
        {
            Tow_Asteroid(ForceMode.Force);
        }
        else if(Distance > AsteroidData.Maximum_Distance_Player_Asteroid)
        {
            Tow_Asteroid(ForceMode.Impulse);
        }

    }


    private void Kinetic_Energy_Function()
    {
        Kinetic_Energy = AsteroidData.Kinetic_Energy(Asteroid_Script.Asteroid_Mass, Asteroid_RigidBody, Player_RigidBody);
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
