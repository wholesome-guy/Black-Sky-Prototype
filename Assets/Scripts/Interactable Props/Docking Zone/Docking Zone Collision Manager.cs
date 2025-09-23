using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private MeshCollider Docking_Mesh_Collider;


    private WaitForSeconds WaitForSeconds_Delay_Duration;

    private PlayerSingleton Player_Singleton;

    private void Start()
    {
        StartCoroutine(Mesh_Dissolve_VFX(true, 10f));
        Player_Singleton = PlayerSingleton.instance;
        WaitForSeconds_Delay_Duration = new WaitForSeconds(Delay_Duration);


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

            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            On_Player_Undocked.Invoke();
            DockingZonePointerManager.Pointer_Event(false, gameObject.transform);
            StartCoroutine(Mesh_Dissolve_VFX(false, 10f));
            Destroy(gameObject, 10.5f); 
        }
    }

    private IEnumerator Delay_On_Player_Dock()
    {
        yield return WaitForSeconds_Delay_Duration;
        Is_Player_Docked = true;
        On_Player_Docked.Invoke();

    }


    private IEnumerator Mesh_Dissolve_VFX(bool Materialise ,float Duration)
    {
        if (Materialise)
        {
            for (int i = 0; i < Mesh_Renderer.Length; i++) 
            {
                Mesh_Renderer[i].sharedMaterial = Materialise_Material;
            }
            Docking_Mesh_Collider.enabled = false;

            float Time_Elapsed = 0;
            while(Time_Elapsed < Duration)
            {
                Time_Elapsed += Time.deltaTime;
                float value = Mathf.Lerp(-100, 30, Time_Elapsed / Duration);
                Materialise_Material.SetFloat("_Cut_Off_Height", value);
                yield return null;
            }
            Materialise_Material.SetFloat("_Cut_Off_Height", 100);


            for (int i = 0; i < ParticleSystems.Length; i++)
            {
                Mesh_Renderer[i].sharedMaterial = Materials[i];
                ParticleSystems[i].Play();
            }         
            Is_Player_Docked = false;
            Docking_Mesh_Collider.enabled = true;
            CameraManager.Camera_Shake_Event.Invoke();
            DockingZonePointerManager.Pointer_Event(true, gameObject.transform);
        }

        else
        {
            for (int i = 0; i < Mesh_Renderer.Length; i++)
            {
                Mesh_Renderer[i].sharedMaterial = Materialise_Material;
            }
            Docking_Mesh_Collider.enabled = false;


            float Time_Elapsed = 0;
            while (Time_Elapsed < Duration)
            {
                Time_Elapsed += Time.deltaTime;
                float value = Mathf.Lerp(50, -100, Time_Elapsed / Duration);
                Materialise_Material.SetFloat("_Cut_Off_Height", value);
                yield return null;
            }
            Materialise_Material.SetFloat("_Cut_Off_Height", -100);
                       
        }
    }
    
}
