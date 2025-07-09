using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb_Deselect_Projectile;  // Rigidbody component of the cannonball projectile
    [SerializeField] private float Thrust_Force;              // Forward force applied to the cannonball
    [SerializeField] private float Torque_Force;              // Rotational force applied to the cannonball

    public static Action<AsteroidScript> Deselect_Asteroid;

    // Destroy the cannonball after 10 seconds to prevent cluttering the scene
    private void Start()
    {
        Destroy(gameObject, 10f);
        Rb_Deselect_Projectile.velocity = PlayerSingleton.instance.Player_Rigidbody.velocity;

    }

    // Called at fixed intervals to apply physics-based movement
    void FixedUpdate()
    {
        Thrust();
    }

    // Applies forward thrust and rotational torque to the cannonball Rigidbody
    private void Thrust()
    {
        Rb_Deselect_Projectile.AddForce(transform.forward * Thrust_Force, ForceMode.Force);
        Rb_Deselect_Projectile.AddTorque(transform.forward * Torque_Force, ForceMode.Force);
    }

    // On collision with any object, destroy both the cannonball and the collided object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Deselect_Asteroid_Function(collision);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);    
        }
    }

    private void Deselect_Asteroid_Function(Collision Asteroid)
    {
        AsteroidScript Asteroid_Script = Asteroid.transform.GetComponent<AsteroidScript>();
        AsteroidTow Asteroid_Tow = Asteroid.transform.GetComponent<AsteroidTow>();

        if(Asteroid_Script.Is_Asteroid_Anchored)
        {
            Deselect_Asteroid.Invoke(Asteroid_Script);
            if(Asteroid_Tow != null)
            {
                Asteroid_Tow.Destroy_Tow_Script();
            }
            Asteroid_Script.Destroy_Anchors();
        }
    }
}
