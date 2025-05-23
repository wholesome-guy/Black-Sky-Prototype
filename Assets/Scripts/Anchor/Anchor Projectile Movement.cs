using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorProjectileMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb_Anchor_Projectile;
    [SerializeField] private float Thrust_Force;
    [SerializeField] private float Torque_Force;
    [SerializeField] private GameObject Sticking_Anchor;

    // Event invoked when the sticking anchor is deployed, passing the contact normal vector
    public static Action<Vector3> Sticking_Anchor_Deployed;

    private void Start()
    {
        // Destroy the projectile after 10 seconds to prevent lingering objects
        Destroy(gameObject, 10f);
    }

    void FixedUpdate()
    {
        // Apply thrust and torque forces every physics update
        Thrust();
    }

    private void Thrust()
    {
        // Add forward force to propel the projectile
        Rb_Anchor_Projectile.AddForce(transform.forward * Thrust_Force, ForceMode.Force);
        // Add torque to create rotational movement for realism
        Rb_Anchor_Projectile.AddTorque(transform.forward * Torque_Force, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if projectile collided with an asteroid
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Get the contact point information
            ContactPoint contactPoint = collision.contacts[0];

            // Calculate position slightly offset into the surface of the asteroid
            Vector3 position = contactPoint.point + contactPoint.normal * -1f;

            // Determine rotation so the anchor faces opposite to the collision normal
            Quaternion rotation = Quaternion.LookRotation(-contactPoint.normal);

            // Instantiate the sticking anchor at the calculated position and rotation
            GameObject Anchor = Instantiate(Sticking_Anchor, position, rotation);

            // Parent the anchor to the asteroid so it moves with it
            Anchor.transform.SetParent(collision.transform);

            // Invoke event to notify that the sticking anchor has been deployed
            Sticking_Anchor_Deployed.Invoke(contactPoint.normal);

            // Destroy this projectile since it has stuck
            Destroy(gameObject);
        }
        else
        {
            // Destroy the projectile immediately on collision with anything else
            Destroy(gameObject);
        }
    }
}
