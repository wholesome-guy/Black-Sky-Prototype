using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandlingMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject Handling_Wheel;
    [SerializeField] private CanvasGroup Handling_Wheel_Canvas_Group;
    [SerializeField] private GameObject Crosshair_Left;
    [SerializeField] private GameObject Crosshair_Right;

    private bool Is_Handling_Wheel_Active = false;

    private int Previous_Index;

    public static Action<int> Handling_Select_Event;
    private WaitForSecondsRealtime WaitForSeconds_0_0_1 = new WaitForSecondsRealtime(0.01f);

    #region Handling Wheel Images
    [SerializeField] private Image[] Sector_Images = new Image[3];
    [SerializeField] private Image[] Ring_Images = new Image[3];
    [SerializeField] private Image[] Handling_Icon = new Image[3];
    [SerializeField] private Color[] Handling_Colours = new Color[4];
    [SerializeField] private string[] Handling_Names = new string[3];

    [SerializeField] private float Min_Scale = 1.0f;
    [SerializeField] private float Max_Scale = 1.5f;
    [SerializeField] private float Duration_Scale = 0.25f;

    [SerializeField] private TextMeshProUGUI Handling_Text;
    [SerializeField] private TextMeshProUGUI Handling_Icon_Text;
    [SerializeField] private Image Inner_Ring;

    #endregion
    private void Start()
    {
        Handling_Wheel.SetActive(false);
        Handling_Icon_Text.gameObject.SetActive(false);
        Handling_Wheel_Canvas_Group.alpha = 0f;
    }

    public void Handling_Wheel_Dispaly()
    {
        UIVisualEffectsManager.Blur_Slow_Time_Event.Invoke();

        Handling_Wheel.SetActive(true);
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Handling_Wheel_Canvas_Group, 0, 1, 0.25f);
        Crosshair_Left.SetActive(false);
        Crosshair_Right.SetActive(false);

        Is_Handling_Wheel_Active = true;
    }

    public void Handling_Wheel_Hide()
    {
        UIVisualEffectsManager.Unblur_Normal_Time_Event.Invoke();
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Handling_Wheel_Canvas_Group, 1, 0, 0.25f);

        Handling_Wheel.SetActive(false);
        Crosshair_Left.SetActive(true);
        Crosshair_Right.SetActive(true);

        Is_Handling_Wheel_Active = false;
    }


    private void Update()
    {
        if (Is_Handling_Wheel_Active)
        {
            Angle_Selector();
        }
    }

    private void Angle_Selector()
    {
        if (Mouse_Input_Manager.instance.Angle_Mouse_Input > 0)
        {
            Handling_Select(Mouse_Input_Manager.instance.Angle_Mouse_Input);
        }
        else
        {
            Handling_Select(360 + Mouse_Input_Manager.instance.Angle_Mouse_Input);
        }
    }

    private void Handling_Select(float Mouse_Angle)
    {
        int New_Index = -1;

        if (Mouse_Angle > 45f && Mouse_Angle < 135f)
        {
            New_Index = 0;
        }
        else if (Mouse_Angle > 270f || Mouse_Angle < 44f)
        {
            New_Index = 1;
        }
        else if (Mouse_Angle > 135f && Mouse_Angle < 270f)
        {
            New_Index = 2;
        }

        if (New_Index != -1 && New_Index != Previous_Index)
        {
            Handling_Wheel_Select(New_Index);
            Handling_Select_Event.Invoke(New_Index);
            StartCoroutine(Delay_DeSelect(Previous_Index));
            Previous_Index = New_Index;
        }
    }

    public void Handling_Wheel_Select(int Index)
    {
        Sector_Images[Index].color = Handling_Colours[Index];
        Ring_Images[Index].color = Handling_Colours[Index];
        Inner_Ring.color = Handling_Colours[Index];
        Handling_Text.text = Handling_Names[Index];
        Handling_Icon_Text.text = Handling_Names[Index];

        StartCoroutine(Scale_Lerp(Sector_Images[Index].transform, Min_Scale * Vector3.one, Max_Scale * Vector3.one, Duration_Scale));
        StartCoroutine(Scale_Lerp(Ring_Images[Index].transform, Min_Scale * Vector3.one, Max_Scale * Vector3.one, Duration_Scale));
        // StartCoroutine(Scale_Lerp(Projectile_Icon[Index].transform, Min_Scale * Vector3.one, Max_Scale * Vector3.one, Duration_Scale));
    }

    public void Handling_Wheel_DeSelect(int Index)
    {
        Sector_Images[Index].color = Handling_Colours[3];
        Ring_Images[Index].color = Handling_Colours[3];


        StartCoroutine(Scale_Lerp(Sector_Images[Index].transform, Max_Scale * Vector3.one, Min_Scale * Vector3.one, Duration_Scale));
        StartCoroutine(Scale_Lerp(Ring_Images[Index].transform, Max_Scale * Vector3.one, Min_Scale * Vector3.one, Duration_Scale));
        //  StartCoroutine(Scale_Lerp(Projectile_Icon[Index].transform, Max_Scale * Vector3.one, Min_Scale * Vector3.one, Duration_Scale));
    }

    private IEnumerator Delay_DeSelect(int t)
    {
        yield return WaitForSeconds_0_0_1;
        Handling_Wheel_DeSelect(t);
    }

    private IEnumerator Scale_Lerp(Transform transform, Vector3 Current_Scale, Vector3 New_Scale, float Duration)
    {
        float Time_Elapsed = 0;
        while (Time_Elapsed < Duration)
        {
            float t = Time_Elapsed / Duration;
            transform.localScale = Vector3.Slerp(Current_Scale, New_Scale, t);
            Time_Elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale = New_Scale;

    }
}
