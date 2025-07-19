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
    private float Distance;
    public static Action<AsteroidScript> Tow_Joint_Broke;

    private float Joint_Break_Velocity_Player;

    private bool Is_Tow_Joint_Broke = false;
    private bool Is_Warning_Displayed =false;
    

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
        Distance = Vector3.Distance(Player_Transform.position, gameObject.transform.position);
        Joint_Velocity_Check();
        Kinetic_Energy_Function();
        Asteroid_Tow();

    }


    private void Kinetic_Energy_Function()
    {
        Kinetic_Energy = AsteroidData.Kinetic_Energy(Asteroid_Script.Asteroid_Mass, Asteroid_RigidBody, Player_RigidBody);
    }

    private void Tow_Force(ForceMode Force_Mode)
    {
        Vector3 Direction  = (Player_Transform.position - gameObject.transform.position).normalized;

        Asteroid_RigidBody.AddForce(Direction * Kinetic_Energy, Force_Mode);
        Asteroid_RigidBody.AddTorque(Direction * Kinetic_Energy, Force_Mode);
    }

    private void Asteroid_Tow()
    {
        if (Distance > AsteroidData.Minimum_Distance_Player_Asteroid && Distance < AsteroidData.Maximum_Distance_Player_Asteroid)
        {
            Tow_Force(ForceMode.Force);
        }
        else if (Distance > AsteroidData.Maximum_Distance_Player_Asteroid)
        {
            Tow_Force(ForceMode.Impulse);
        }
    }

    private void Joint_Velocity_Check()
    {
        if (Player_RigidBody.velocity.magnitude > Joint_Break_Velocity_Player && Is_Tow_Joint_Broke == false)
        {
           TimerManager.Timer_Delay_Event.Invoke(AsteroidData.Joint_Break_Delay_Duration);
           StartCoroutine(Delay_Break_Joint(AsteroidData.Joint_Break_Delay_Duration));
           Is_Tow_Joint_Broke = true;
        }

        if (Player_RigidBody.velocity.magnitude > (Joint_Break_Velocity_Player - AsteroidData.Warning_Velocity_Difference_Constant) && !Is_Tow_Joint_Broke && !Is_Warning_Displayed)
        {
            DisplayTextManager.Display_Text_Event.Invoke(AsteroidData.Approaching_Max_Tether_Velocity);
            Is_Warning_Displayed = true;
        }

        if (Is_Warning_Displayed)
        {
            if (Player_RigidBody.velocity.magnitude < Joint_Break_Velocity_Player - AsteroidData.Warning_Velocity_Difference_Constant)
            {
                Is_Warning_Displayed = false;
            }
        }
    }
    private void Break_Tow_Joint()
    {
        DisplayTextManager.Display_Text_Event.Invoke(AsteroidData.Joint_Break);
        Tow_Joint_Broke.Invoke(Asteroid_Script);
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
