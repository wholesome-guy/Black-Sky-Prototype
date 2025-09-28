using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb_Cannon_Projectile;  // Rigidbody component of the cannonball projectile
    [SerializeField] private float Thrust_Force;              // Forward force applied to the cannonball
    [SerializeField] private float Torque_Force;

    private ObjectPoolingManager Object_Pooling_Manager;
    private GameObject Explosion;


    private Mouse_Input_Manager Mouse_Input_Manager_;
    private PlayerSingleton Player_Singleton;

    
    private void Start()
    {
        Object_Pooling_Manager = ObjectPoolingManager.Instance;
        Mouse_Input_Manager_ = Mouse_Input_Manager.instance;
        Player_Singleton = PlayerSingleton.instance;
        Destroy(gameObject, 10f);
        if (!Mouse_Input_Manager_.Is_Free_Aim_On)
        {
            Rb_Cannon_Projectile.velocity = Player_Singleton.Player_Rigidbody.velocity;
        }

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
        CameraManager.Camera_Shake_Event.Invoke();
        CrossHairManager.Hit_Mark_Event.Invoke();

        Explosion = Object_Pooling_Manager.Instantiate_Explosion();
        Explosion.transform.position = transform.position;
        Explosion.transform.rotation = transform.rotation;

        Object_Pooling_Manager.Destroy_Explosion(5f, Explosion);

        Destroy(gameObject);
    }
}
