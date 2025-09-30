using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorProjectileMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb_Anchor_Projectile;
    [SerializeField] private TrailRenderer Trail_Renderer;
    [SerializeField] private float Thrust_Force;
    [SerializeField] private float Torque_Force;
     private GameObject Sticking_Anchor;
    [SerializeField] private GameObject Left_Sticking_Anchor;
    [SerializeField] private GameObject Right_Sticking_Anchor;
    [SerializeField] private bool Is_Right;

    private ObjectPoolingManager Object_Pooling_Manager;
    private GameObject Explosion;
    private Mouse_Input_Manager Mouse_Input_Manager_;
    private PlayerSingleton Player_Singleton;

    // Event invoked when the sticking anchor is deployed, passing the contact normal vector
    public static Action<Vector3> Sticking_Anchor_Deployed;


    private void Awake()
    {
        Mouse_Input_Manager_ = Mouse_Input_Manager.instance;
        Player_Singleton = PlayerSingleton.instance;
        Object_Pooling_Manager = ObjectPoolingManager.Instance;
    }
    private void OnEnable()
    {

        Rb_Anchor_Projectile.velocity = Vector3.zero;
        Rb_Anchor_Projectile.angularVelocity = Vector3.zero;

        if(Is_Right)
        {
            Sticking_Anchor = Right_Sticking_Anchor;
        }
        else
        {
            Sticking_Anchor = Left_Sticking_Anchor;
        }

        Destroy_Projectile(10f);

        if (!Mouse_Input_Manager_.Is_Free_Aim_On)
        {
            Rb_Anchor_Projectile.velocity = Player_Singleton.Player_Rigidbody.velocity;
        }

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
        CameraManager.Camera_Shake_Event.Invoke();

        // Check if projectile collided with an asteroid
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            CrossHairManager.Hit_Mark_Event.Invoke();


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
            if(Is_Right)
            {
                Sticking_Anchor_Deployed.Invoke(contactPoint.normal);
            }

            // Destroy this projectile since it has stuck
            Destroy_Projectile(0.05f);

        }
        else
        {

            Explosion = Object_Pooling_Manager.Instantiate_Explosion();
            Explosion.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

            Object_Pooling_Manager.Destroy_Explosion(5f, Explosion);
            // Destroy the projectile immediately on collision with anything else
            Destroy_Projectile(0.05f);
        }
    }
    private void Destroy_Projectile(float duration)
    {
        if (Is_Right)
        {
            Object_Pooling_Manager.Destroy_Anchor_Projectile_Right(duration, gameObject, Trail_Renderer);
        }
        else
        {
            Object_Pooling_Manager.Destroy_Anchor_Projectile_Left(duration, gameObject, Trail_Renderer);

        }
    }
}
