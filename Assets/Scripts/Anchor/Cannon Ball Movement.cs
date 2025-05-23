using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb_Cannon_Projectile;  // Rigidbody component of the cannonball projectile
    [SerializeField] private float Thrust_Force;              // Forward force applied to the cannonball
    [SerializeField] private float Torque_Force;              // Rotational force applied to the cannonball

    // Destroy the cannonball after 10 seconds to prevent cluttering the scene
    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Called at fixed intervals to apply physics-based movement
    void FixedUpdate()
    {
        Thrust();
    }

    // Applies forward thrust and rotational torque to the cannonball Rigidbody
    private void Thrust()
    {
        Rb_Cannon_Projectile.AddForce(transform.forward * Thrust_Force, ForceMode.Force);
        Rb_Cannon_Projectile.AddTorque(transform.forward * Torque_Force, ForceMode.Force);
    }

    // On collision with any object, destroy both the cannonball and the collided object
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
