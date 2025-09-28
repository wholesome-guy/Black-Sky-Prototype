using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MaterialDissolveManager : MonoBehaviour
{



    public static Action<bool, MeshRenderer[], Collider[], Material, Material[], float> Multiple_Mesh_Dissolve_Event;
    public static Action<bool,MeshRenderer, Collider, Material, Material, float> Single_Mesh_Dissolve_Event;

    private void OnEnable()
    {
        Multiple_Mesh_Dissolve_Event += Multiple_Mesh_Dissolve;
        Single_Mesh_Dissolve_Event += Single_Mesh_Dissolve;
    }
    private void OnDisable()
    {
        Multiple_Mesh_Dissolve_Event -= Multiple_Mesh_Dissolve;
        Single_Mesh_Dissolve_Event -= Single_Mesh_Dissolve;

    }




    #region Single Mesh Dissolve
    private void Single_Mesh_Dissolve(bool Dissolve, MeshRenderer Mesh_Renderer,
         Collider Mesh_Collider,
         Material Dissolve_Material,
         Material Original_Material,
         float Duration)
    {
        StartCoroutine
           (Single_Mesh_Dissolve_VFX(
           Dissolve,
           Mesh_Renderer,
           Mesh_Collider,
           Dissolve_Material,
           Original_Material,
           Duration
           ));
    }



    private IEnumerator Single_Mesh_Dissolve_VFX
         (bool Dissolve, MeshRenderer Mesh_Renderer,
          Collider Mesh_Collider,
          Material Dissolve_Material,
          Material Original_Material,
          float Duration)
    {


        if (Dissolve)
        {

            
            
                Mesh_Renderer.sharedMaterial = Dissolve_Material;
                Mesh_Collider.enabled = false;
            

            float Time_Elapsed = 0;

            while (Time_Elapsed < Duration)
            {
                Time_Elapsed += Time.deltaTime;
                float value = Mathf.Lerp(0, 1, Time_Elapsed / Duration);
                Dissolve_Material.SetFloat("_Dissolve_Amount", value);
                yield return null;
            }
            Dissolve_Material.SetFloat("_Dissolve_Amount", 1);


        }
        else
        {

            
                Mesh_Renderer.sharedMaterial = Dissolve_Material;
                Mesh_Collider.enabled = false;
            

            float Time_Elapsed = 0;

            while (Time_Elapsed < Duration)
            {
                Time_Elapsed += Time.deltaTime;
                float value = Mathf.Lerp(1, 0, Time_Elapsed / Duration);
                Dissolve_Material.SetFloat("_Dissolve_Amount", value);
                yield return null;
            }
            Dissolve_Material.SetFloat("_Dissolve_Amount", 0);

            CameraManager.Camera_Shake_Event.Invoke();

            
                Mesh_Renderer.sharedMaterial = Original_Material;
                Mesh_Collider.enabled = true;
            
        }


    }
    #endregion

    #region Multiple Mesh Dissolve
    private void Multiple_Mesh_Dissolve(bool Dissolve, MeshRenderer[] Mesh_Renderer_Array,
        Collider[] Mesh_Colliders,
        Material Dissolve_Material,
        Material[] Original_Material,
        float Duration)
    {
        StartCoroutine
           (Multiple_Mesh_Dissolve_VFX(
           Dissolve,
           Mesh_Renderer_Array,
           Mesh_Colliders,
           Dissolve_Material,
           Original_Material,
           Duration
           ));
    }



    private IEnumerator Multiple_Mesh_Dissolve_VFX
         (bool Dissolve,MeshRenderer[] Mesh_Renderer_Array, 
          Collider[] Mesh_Colliders, 
          Material Dissolve_Material, 
          Material[] Original_Material,
          float Duration)
    {

        int Length = Mesh_Renderer_Array.Length;

        if (Dissolve)
        {

            for (int i = 0; i < Length; i++)
            {
                Mesh_Renderer_Array[i].sharedMaterial = Dissolve_Material;
                Mesh_Colliders[i].enabled = false;
            }

            float Time_Elapsed = 0;

            while (Time_Elapsed < Duration)
            {
                Time_Elapsed += Time.deltaTime;
                float value = Mathf.Lerp(0, 1, Time_Elapsed / Duration);
                Dissolve_Material.SetFloat("_Dissolve_Amount", value);
                yield return null;
            }
            Dissolve_Material.SetFloat("_Dissolve_Amount", 1);

            
        }
        else
        {
            for (int i = 0; i < Length; i++)
            {
                Mesh_Renderer_Array[i].sharedMaterial = Dissolve_Material;
                Mesh_Colliders[i].enabled = false;
            }

            float Time_Elapsed = 0;

            while (Time_Elapsed < Duration)
            {
                Time_Elapsed += Time.deltaTime;
                float value = Mathf.Lerp(1, 0, Time_Elapsed / Duration);
                Dissolve_Material.SetFloat("_Dissolve_Amount", value);
                yield return null;
            }
            Dissolve_Material.SetFloat("_Dissolve_Amount", 0);

            CameraManager.Camera_Shake_Event.Invoke();

            for (int i = 0; i < Length; i++)
            {
                Mesh_Renderer_Array[i].sharedMaterial = Original_Material[i];
                Mesh_Colliders[i].enabled = true;
            }
        }
        

    }
#endregion
}
