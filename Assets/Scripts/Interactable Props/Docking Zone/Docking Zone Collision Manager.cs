using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using static UnityEngine.Rendering.DebugUI;

public class DockingZoneCollisionManager : MonoBehaviour
{
    public static Action On_Player_Docked;
    public static Action On_Player_Undocked;

    private bool Is_Player_Docked;

    [SerializeField] private float Delay_Duration = 5.0f;

    [SerializeField] private MeshRenderer[] Mesh_Renderer;
    [SerializeField] private Material[] Materials;
    [SerializeField] private Material Materialise_Material;
    [SerializeField] private ParticleSystem[] ParticleSystems;
    [SerializeField] private Collider[] Docking_Mesh_Collider;
    [SerializeField] private VisualEffect Disintegrate_VFX;


    private WaitForSeconds WaitForSeconds_Delay_Duration;
    private WaitForSeconds WaitForSeconds_10 = new WaitForSeconds(10f); 

    private PlayerSingleton Player_Singleton;
    private ObjectPoolingManager Object_Pooling_Manager;

    private void Start()
    {

        Player_Singleton = PlayerSingleton.instance;
        Object_Pooling_Manager = ObjectPoolingManager.Instance;
        WaitForSeconds_Delay_Duration = new WaitForSeconds(Delay_Duration);

    }

    private void OnEnable()
    {
        Is_Player_Docked = false;
        Disintegrate_VFX.Play();
        StartCoroutine(Materialise_Effect());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!Is_Player_Docked)
            {
                TimerManager.Timer_Delay_Event.Invoke(Delay_Duration);
                StartCoroutine(Delay_On_Player_Dock());
                Player_Singleton.Is_Spaceship_At_Rest = true;
                Player_Singleton.Is_Spaceship_Able_To_Shoot = false;

            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            On_Player_Undocked.Invoke();
            DockingZonePointerManager.Pointer_Event(false, gameObject.transform);
            StartCoroutine(Delay_On_Player_UnDock());
            Player_Singleton.Is_Spaceship_Able_To_Shoot = true;

        }
    }

    private IEnumerator Delay_On_Player_Dock()
    {
        yield return WaitForSeconds_Delay_Duration;
        Is_Player_Docked = true;
        On_Player_Docked.Invoke();

    }
    private IEnumerator Delay_On_Player_UnDock()
    {
        yield return WaitForSeconds_Delay_Duration;
        MaterialDissolveManager.Multiple_Mesh_Dissolve_Event.Invoke(true, Mesh_Renderer, Docking_Mesh_Collider, Materialise_Material, Materials, 10f);
        Object_Pooling_Manager.Destroy_Docking_Zone(10.5f, gameObject);

    }

    private IEnumerator Materialise_Effect()
    {
        MaterialDissolveManager.Multiple_Mesh_Dissolve_Event.Invoke(false, Mesh_Renderer, Docking_Mesh_Collider, Materialise_Material, Materials,10f);

        yield return WaitForSeconds_10;

        for (int i = 0; i < 2; i++)
        {
            ParticleSystems[i].Play();
        }
        DockingZonePointerManager.Pointer_Event(true, gameObject.transform);
    }
    
}
