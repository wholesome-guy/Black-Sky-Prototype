using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DockingZonePointerManager : MonoBehaviour
{
    [SerializeField] private GameObject Docking_Zone_Pointer;
    [SerializeField] private CanvasGroup Indicator_Canvas_Group;
    [SerializeField] private Transform Pointer;

    [SerializeField] private Transform Docking_Zone;

    private bool Is_Pointer_Active = false;

    public static Action<bool, Transform> Pointer_Event;

    private WaitForSeconds WaitForSeconds_1 = new WaitForSeconds(1f);

    private void OnEnable()
    {
        Pointer_Event += Pointer_Display;
    }
    private void OnDisable()
    {
        Pointer_Event -= Pointer_Display;   
    }
    private void Start()
    {
        Docking_Zone_Pointer.SetActive(false);
        Indicator_Canvas_Group.alpha = 0f;
        Is_Pointer_Active = false;
    }
    private void Update()
    {
        if (Is_Pointer_Active)
        {
            Pointer_Function();
        }
        
    }

    private void Pointer_Display(bool Is_Active,Transform Docking_Zone_Transform)
    {
        if(Is_Active)
        {
            Docking_Zone_Pointer.SetActive(true);
            UIVisualEffectsManager.UI_Fader_Event.Invoke(Indicator_Canvas_Group, 0, 1, 0.2f);
            Docking_Zone = Docking_Zone_Transform;
            Is_Pointer_Active = true;
        }
        else
        {
            UIVisualEffectsManager.UI_Fader_Event.Invoke(Indicator_Canvas_Group, 1, 0, 0.2f);
            Is_Pointer_Active = false;
            StartCoroutine(Delay_Hide());
        }
    }
    private void Pointer_Function()
    {

        Vector3 Pointer_Position = PlayerSingleton.instance.transform.InverseTransformPoint(Docking_Zone.transform.position);

        float Angle  = Mathf.Atan2(Pointer_Position.x, Pointer_Position.z) *Mathf.Rad2Deg;

        Pointer.transform.localEulerAngles = new Vector3(0, 180, Angle);
    }

    private IEnumerator Delay_Hide()
    {
        yield return WaitForSeconds_1;
        Docking_Zone_Pointer.SetActive(false);
        Docking_Zone = null;
    }
}
