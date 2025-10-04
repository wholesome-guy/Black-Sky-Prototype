using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SellingZoneManager : MonoBehaviour
{
    [SerializeField] private string Asteriod_String;

    [SerializeField] private Material Dissolve_Material_Asteroid;
    [SerializeField] private Material Scan_Material_Asteroid;

    [SerializeField] private Transform Selling_Dock_Transform;

    [SerializeField] private VisualEffect Visual_Effect_VFX;

    #region Asteroid and Player Data
    private AsteroidScript Asteroid_Script;
    private Rigidbody Asteroid_Rigidbody;
    private GameObject Asteroid_Gameobject;
    private MeshRenderer Asteriod_Mesh_Renderer;
    private MeshCollider Asteroid_Collider;
    private Material Base_Material;

    private PlayerSingleton Player_Singleton;
    private Rigidbody Player_Rigidbody;
    private float Sell_Value = 0;
    #endregion

    private bool Asteroid_At_Selling_Zone = false;
    public static Action Selling_Zone_Enter;
    public static Action Selling_Zone_Exit;

    #region Wait For Seconds Cache
    private WaitForSeconds Wait_For_Seconds_025 = new WaitForSeconds(0.25f);
    private WaitForSeconds Wait_For_Seconds_05 = new WaitForSeconds(0.5f);
    private WaitForSeconds Wait_For_Seconds_1 = new WaitForSeconds(1f);
    private WaitForSeconds Wait_For_Seconds_5 = new WaitForSeconds(5f);
    private WaitForSeconds Wait_For_Seconds_6 = new WaitForSeconds(6f);

    #endregion

    private void Start()
    {
        Player_Singleton = PlayerSingleton.instance;

        Player_Rigidbody = Player_Singleton.Player_Rigidbody;
        Visual_Effect_VFX.Stop();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Asteriod_String))
        {
            Asteroid_Script = other.gameObject.GetComponent<AsteroidScript>();
            Asteroid_Rigidbody = other.gameObject.GetComponent<Rigidbody>();
            Asteriod_Mesh_Renderer = other.gameObject.GetComponent<MeshRenderer>();
            Asteroid_Collider = other.gameObject.GetComponent<MeshCollider>();
            Asteroid_Gameobject = other.gameObject;

            Base_Material = Asteriod_Mesh_Renderer.sharedMaterial;

            Sell_Value = Asteroid_Script.Sell_Value;

            Asteroid_Rigidbody.drag = 10;
            Asteroid_Rigidbody.angularDrag = 10;
            

            Player_Rigidbody.velocity = Vector3.zero;
            Player_Rigidbody.angularVelocity = Vector3.zero;
            Player_Singleton.Is_Spaceship_At_Rest = true;
            Player_Singleton.Is_Spaceship_At_Selling_Zone = true;





        }       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Asteriod_String) && !Asteroid_Script.Is_Asteroid_Tethered && !Asteroid_At_Selling_Zone)
        {
            Asteroid_At_Selling_Zone = true;
            Selling_Zone_Enter.Invoke();

            StartCoroutine(Asteroid_Pull_To_Sell());
                
            
        }
    }

    private IEnumerator Asteroid_Pull_To_Sell()
    {

        Asteroid_Rigidbody.DOMove(Selling_Dock_Transform.position, 5f);

        yield return Wait_For_Seconds_5;

        Asteroid_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        Material[] Material_Array = new Material[2];
        Material_Array[0] = Base_Material;
        Material_Array[1] = Scan_Material_Asteroid;
        Asteriod_Mesh_Renderer.sharedMaterials = Material_Array;

        yield return Wait_For_Seconds_5;

        Material[] Material_Array_Normal = new Material[1];
        Material_Array_Normal[0] = Base_Material;
        Asteriod_Mesh_Renderer.sharedMaterials = Material_Array_Normal;

        yield return Wait_For_Seconds_025;

        MaterialDissolveManager.Single_Mesh_Dissolve_Event.Invoke(true, Asteriod_Mesh_Renderer, Asteroid_Collider, Dissolve_Material_Asteroid, Base_Material, 10f);
        Visual_Effect_VFX.Play();

        yield return Wait_For_Seconds_6;

        MoneyManager.Money_Change_Event.Invoke(Sell_Value);

        Destroy(Asteroid_Gameobject, 1f);
        Player_Singleton.Is_Spaceship_At_Rest = false;
        Player_Singleton.Is_Spaceship_At_Selling_Zone = false;
        Selling_Zone_Exit.Invoke();

    }
}
