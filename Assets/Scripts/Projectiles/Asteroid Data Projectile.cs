using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AsteroidDataProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb_Asteroid_Data_Projectile;
    [SerializeField] private MeshRenderer Mesh_Renderer_Asteroid_Data_Projectile;
    [SerializeField] private Material Flash_Material;
    [SerializeField] private VisualEffect Particles_VFX;
    [SerializeField] private TrailRenderer Trail_Renderer;

    [SerializeField] private float Thrust_Force;              // Forward force applied to the cannonball
    [SerializeField] private float Torque_Force;              // Rotational force applied to the cannonball

    [SerializeField] private Material Base_Material;
    [SerializeField] private Material Scan_Material;

    private WaitForSeconds WaitForSeconds_1 = new WaitForSeconds(1f);
    private WaitForSeconds WaitForSeconds_4 = new WaitForSeconds(4f);
    private WaitForSeconds WaitForSeconds_Scan_Duration;

    private ObjectPoolingManager Object_Pooling_Manager;
    private GameObject Explosion;

    [Range(0,10)]
    [SerializeField] private float Scan_Duration;


    private bool In_Contact_With_Asteroid = false;

    private Mouse_Input_Manager Mouse_Input_Manager_;
    private PlayerSingleton Player_Singleton;

    private void Awake()
    {
        Mouse_Input_Manager_ = Mouse_Input_Manager.instance;
        Player_Singleton = PlayerSingleton.instance;
        Object_Pooling_Manager = ObjectPoolingManager.Instance;
    }

    // Destroy the cannonball after 10 seconds to prevent cluttering the scene
    private void OnEnable()
    {
        Rb_Asteroid_Data_Projectile.velocity = Vector3.zero;
        Rb_Asteroid_Data_Projectile.angularVelocity = Vector3.zero;
        In_Contact_With_Asteroid = false;

        Object_Pooling_Manager.Destroy_Data_Projectile(15f, gameObject, Trail_Renderer);

        WaitForSeconds_Scan_Duration = new WaitForSeconds(Scan_Duration);


        if (!Mouse_Input_Manager_.Is_Free_Aim_On)
        {
            Rb_Asteroid_Data_Projectile.velocity = Player_Singleton.Player_Rigidbody.velocity;
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
        Rb_Asteroid_Data_Projectile.AddForce(transform.forward * Thrust_Force, ForceMode.Force);
        Rb_Asteroid_Data_Projectile.AddTorque(transform.forward * Torque_Force, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            In_Contact_With_Asteroid = true;
            Rb_Asteroid_Data_Projectile.velocity = Vector3.zero;
            Rb_Asteroid_Data_Projectile.angularVelocity = Vector3.zero;

            CrossHairManager.Hit_Mark_Event.Invoke();
            AsteroidScript Asteroid_Data = collision.gameObject.GetComponent<AsteroidScript>();
            MeshRenderer Asteroid_Mesh_Renderer = collision.gameObject.GetComponent<MeshRenderer>();
            Base_Material = Asteroid_Data.Asteroid_Material;


            StartCoroutine(Asteroid_Data_Fetch(Asteroid_Data, Asteroid_Mesh_Renderer));
            Particles_VFX.Play();

        }
        else
        {
            Explosion = Object_Pooling_Manager.Instantiate_Explosion();
            Explosion.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

            Object_Pooling_Manager.Destroy_Explosion(5f, Explosion);
            Object_Pooling_Manager.Destroy_Data_Projectile(0.05f, gameObject, Trail_Renderer);
        }


    }


    private IEnumerator Asteroid_Data_Fetch(AsteroidScript Asteroid_Data, MeshRenderer Asteroid_Mesh_Renderer)
    {
        Material[] Material_Array = new Material[2];
        Material_Array[0] = Base_Material;
        Material_Array[1] = Scan_Material;
        Asteroid_Mesh_Renderer.sharedMaterials = Material_Array;


        yield return WaitForSeconds_Scan_Duration;

        
        
            AsteroidInformationManager.Asteroid_Information_Event
            (Asteroid_Data.Asteroid_Mass, Asteroid_Data.Sell_Value,
             Asteroid_Data.Asteroid_Elemental_Content[0], Asteroid_Data.Asteroid_Elemental_Content_Percentage[0],
             Asteroid_Data.Asteroid_Elemental_Content[1], Asteroid_Data.Asteroid_Elemental_Content_Percentage[1],
             Asteroid_Data.Asteroid_Elemental_Content[2], Asteroid_Data.Asteroid_Elemental_Content_Percentage[2]);




        yield return WaitForSeconds_1;

        Particles_VFX.Stop();
        Material[] Material_Array_Normal = new Material[1];
        Material_Array_Normal[0] = Base_Material;
        Asteroid_Mesh_Renderer.sharedMaterials = Material_Array_Normal;

        MaterialFlashManager.Object_Single_Flash.Invoke(Mesh_Renderer_Asteroid_Data_Projectile, Mesh_Renderer_Asteroid_Data_Projectile.material, Flash_Material, 5, 0.5f);
        yield return WaitForSeconds_4;

        Explosion = Object_Pooling_Manager.Instantiate_Explosion();
        Explosion.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

        Object_Pooling_Manager.Destroy_Explosion(5f, Explosion);
        Object_Pooling_Manager.Destroy_Data_Projectile(0.05f, gameObject, Trail_Renderer);



    }
}
