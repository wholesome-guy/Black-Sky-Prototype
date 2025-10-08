using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb_Deselect_Projectile;  // Rigidbody component of the cannonball projectile
    [SerializeField] private TrailRenderer Trail_Renderer;
    [SerializeField] private float Thrust_Force;              // Forward force applied to the cannonball
    [SerializeField] private float Torque_Force;              // Rotational force applied to the cannonball

    public static Action<AsteroidScript> Deselect_Asteroid;
    public static Action Deselect;

    [SerializeField] private float Delay_Duration = 2.0f;

    [SerializeField] private MeshRenderer Mesh_Renderer_Asteroid_Anchor_Deselect_Projectile;
    [SerializeField] private Material Flash_Material;

    private bool In_Contact_With_Asteroid = false;

    private WaitForSeconds WaitForSeconds_5 = new WaitForSeconds(5f);
    private WaitForSeconds WaitForSeconds_Delay_Duration;

    private Mouse_Input_Manager Mouse_Input_Manager_;
    private ObjectPoolingManager Object_Pooling_Manager;
    private PlayerSingleton Player_Singleton;
    private GameObject Explosion;

    private void Awake()
    {
        Mouse_Input_Manager_ = Mouse_Input_Manager.instance;
        Player_Singleton = PlayerSingleton.instance;
        Object_Pooling_Manager = ObjectPoolingManager.Instance;
    }
    private void OnEnable()
    {
        Rb_Deselect_Projectile.velocity = Vector3.zero;
        Rb_Deselect_Projectile.angularVelocity = Vector3.zero;

        Object_Pooling_Manager.Destroy_Deselect_Projectile(10f, gameObject, Trail_Renderer);
        In_Contact_With_Asteroid = false;
        WaitForSeconds_Delay_Duration = new WaitForSeconds(Delay_Duration);

        if (!Mouse_Input_Manager_.Is_Free_Aim_On)
        {
            Rb_Deselect_Projectile.velocity = Player_Singleton.Player_Rigidbody.velocity;
        }


    }

    // Called at fixed intervals to apply physics-based movement
    void FixedUpdate()
    {
        if (!In_Contact_With_Asteroid)
        {
            Thrust();
        }
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
        CameraManager.Camera_Shake_Event.Invoke();
        PlayerDamageVFX.Debris_Event.Invoke(collision.contacts[0].point);


        if (collision.gameObject.CompareTag("Asteroid"))
        {
            

            In_Contact_With_Asteroid = true;
            Rb_Deselect_Projectile.velocity = Vector3.zero;
            Rb_Deselect_Projectile.angularVelocity = Vector3.zero;


            StartCoroutine(Deselect_Asteroid_Function(collision));
            CrossHairManager.Hit_Mark_Event.Invoke();
        }
        else
        {
            Explosion = Object_Pooling_Manager.Instantiate_Explosion();
            Explosion.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

            Object_Pooling_Manager.Destroy_Explosion(5f, Explosion);

            Object_Pooling_Manager.Destroy_Deselect_Projectile(0.05f, gameObject, Trail_Renderer);

        }
    }

    private IEnumerator Deselect_Asteroid_Function(Collision Asteroid)
    {
        AsteroidScript Asteroid_Script = Asteroid.transform.GetComponent<AsteroidScript>();
        AsteroidTow Asteroid_Tow = Asteroid.transform.GetComponent<AsteroidTow>();
        Rigidbody Asteroid_Rb = Asteroid.transform.GetComponent<Rigidbody>();

        if (Asteroid_Script.Is_Asteroid_Anchored)
        {
            Asteroid_Rb.constraints = RigidbodyConstraints.FreezeAll;

            Deselect.Invoke();
            TimerManager.Timer_Delay_Event.Invoke(Delay_Duration);
            yield return WaitForSeconds_Delay_Duration;

            Deselect_Asteroid.Invoke(Asteroid_Script);
            if(Asteroid_Tow != null)
            {
                Asteroid_Tow.Destroy_Tow_Script();
            }
            Asteroid_Script.Destroy_Anchors();
            Asteroid_Rb.constraints = RigidbodyConstraints.None;

            MaterialFlashManager.Object_Single_Flash.Invoke(Mesh_Renderer_Asteroid_Anchor_Deselect_Projectile, Mesh_Renderer_Asteroid_Anchor_Deselect_Projectile.material, Flash_Material, 5, 0.5f);
            yield return WaitForSeconds_5;

            Explosion = Object_Pooling_Manager.Instantiate_Explosion();
            Explosion.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

            Object_Pooling_Manager.Destroy_Explosion(5f, Explosion);
            Object_Pooling_Manager.Destroy_Deselect_Projectile(0.05f, gameObject, Trail_Renderer);

        }

        else
        {
            MaterialFlashManager.Object_Single_Flash.Invoke(Mesh_Renderer_Asteroid_Anchor_Deselect_Projectile, Mesh_Renderer_Asteroid_Anchor_Deselect_Projectile.material, Flash_Material, 5, 0.5f);
            yield return WaitForSeconds_5;

            Explosion = Object_Pooling_Manager.Instantiate_Explosion();
            Explosion.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

            Object_Pooling_Manager.Destroy_Explosion(5f, Explosion);
            Object_Pooling_Manager.Destroy_Deselect_Projectile(0.05f, gameObject, Trail_Renderer);
        }
    }

    
}
