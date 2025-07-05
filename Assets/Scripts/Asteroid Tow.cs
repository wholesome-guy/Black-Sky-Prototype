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
    public float Asteriod_Mass;

    private float Kinetic_Energy;
    
    public static Action Tow_Joint_Broke;

    [SerializeField] private float Minimum_Distance_Player_Asteroid = 50f;
    [SerializeField] private float Maximum_Distnce_Player_Asteroid = 300f;
    [SerializeField] private float Maximum_Velcity_Player = 200f;
    [SerializeField] private float Max_Force_Multiplier = 100;

    

    private void Start()
    {
        Player_Transform = PlayerSingleton.instance.Player_Transform;
        Player_RigidBody = PlayerSingleton.instance.Player_Rigidbody;

        Asteroid_RigidBody = gameObject.GetComponent<Rigidbody>();

        Asteroid_Script = gameObject.GetComponent<AsteroidScript>();
        
    }

    private void FixedUpdate()
    {
        if(Player_RigidBody.velocity.magnitude > Maximum_Velcity_Player)
        {
            Break_Tow_Joint();
        }

        float Distance = Vector3.Distance(Player_Transform.position, gameObject.transform.position);

        Kinetic_Energy_Function();

        if(Distance > Minimum_Distance_Player_Asteroid && Distance < Maximum_Distnce_Player_Asteroid)
        {
            Tow_Asteroid(ForceMode.Force);
        }
        else if(Distance > Maximum_Distnce_Player_Asteroid)
        {
            Tow_Asteroid(ForceMode.Impulse);
        }



    }


    private void Kinetic_Energy_Function()
    {
        float Kinetic_Energy_Local = (0.5f) * (Asteriod_Mass) *Mathf.Pow(Asteroid_RigidBody.velocity.magnitude - Player_RigidBody.velocity.magnitude, 2);
        float Max_Kinetic_Energy = Asteriod_Mass * Max_Force_Multiplier;
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
        Tow_Joint_Broke.Invoke();
        Asteroid_Script.Destroy_Anchors();

        Destroy_Tow_Script();
    }
    public void Destroy_Tow_Script()
    {
        Destroy(this);
    }

}
