using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System;

public class ProjectileWheelManager : MonoBehaviour
{
    [SerializeField] private GameObject Projectile_Wheel;
    [SerializeField] private GameObject Crosshair_Left;
    [SerializeField] private GameObject Crosshair_Right;
    [SerializeField] private Volume Global_Volume;

    private DepthOfField Depth_Of_Field;
    [SerializeField] private float Focus_Distance = 1.75f;
    [SerializeField] private int Focal_Length = 130;
    [SerializeField] private int Aperture = 32;

    private bool Is_Projectile_Wheel_Active =false;

    private int Previous_Index;

    public static Action<int> Projectile_Select_Event;
    #region Projectile Wheel Images

    [SerializeField] private Image[] Sector_Images = new Image[6];
    [SerializeField] private Image[] Ring_Images = new Image[6];
    [SerializeField] private Image[] Projectile_Icon = new Image[6];
    [SerializeField] private Color[] Projectile_Colours = new Color[7];
    [SerializeField] private string[] Projectile_Names = new string[6];

    [SerializeField] private float Min_Scale = 1.0f;
    [SerializeField] private float Max_Scale = 1.5f;
    [SerializeField] private float Duration_Scale = 0.25f;

    [SerializeField] private TextMeshProUGUI Projectile_Text;
    [SerializeField] private Image Inner_Ring;

    #endregion


    private void Start()
    {
        if (Global_Volume.profile.TryGet<DepthOfField>(out Depth_Of_Field))
        {
            Depth_Of_Field.active = false;
            Depth_Of_Field.focusDistance.value = Focus_Distance;
            Depth_Of_Field.focalLength.value = Focal_Length;
        }
        Projectile_Wheel.SetActive(false); 
       
        Depth_Of_Field.aperture.value = Aperture;
    }

    public void Projectile_Wheel_Dispaly()
    {
        Depth_Of_Field.active = true;

        Projectile_Wheel.SetActive(true);
        Crosshair_Left.SetActive(false);
        Crosshair_Right.SetActive(false);

        Is_Projectile_Wheel_Active = true;
        Time.timeScale = 0.25f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void Projectile_Wheel_Hide()
    {
        Depth_Of_Field.active = false;

        Projectile_Wheel.SetActive(false);
        Crosshair_Left.SetActive(true);
        Crosshair_Right.SetActive(true);

        Is_Projectile_Wheel_Active = false;
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

    }

    private void Update()
    {
        if (Is_Projectile_Wheel_Active)
        {
            Angle_Seletor();
        }
    }

    private void Angle_Seletor()
    {
        if (Mouse_Input_Manager.instance.Angle_Mouse_Input > 0)
        {
            Projectile_Select(Mouse_Input_Manager.instance.Angle_Mouse_Input);
        }
        else
        {
            Projectile_Select(360 + Mouse_Input_Manager.instance.Angle_Mouse_Input);
        }
    }

    private void Projectile_Select(float Angle)
    {
        int New_Index = -1;

        if(Angle > 70 && Angle < 106f)
        {
            New_Index = 0;
        }

        else if(Angle > 0f && Angle < 69f)
        {
            New_Index = 1;
        }

        else if (Angle > 288f && Angle < 359f)
        {
            New_Index = 2;

        }

        else if (Angle > 252f && Angle < 287f)
        {
            New_Index = 3;

        }

        else if (Angle > 184f && Angle < 251f)
        {
            New_Index = 4;

        }

        else if (Angle > 107f && Angle < 176f)
        {
            New_Index = 5;

        }
        if (New_Index != -1 && New_Index != Previous_Index)
        {
            Projectile_Wheel_Select(New_Index);
            Projectile_Select_Event.Invoke(New_Index);
            StartCoroutine(Delay_DeSelect(Previous_Index)); 
            Previous_Index = New_Index; 
        }

    }

    public void Projectile_Wheel_Select(int Index)
    {
        Sector_Images[Index].color = Projectile_Colours[Index];
        Ring_Images[Index].color = Projectile_Colours[Index];
        Inner_Ring.color = Projectile_Colours[Index];
        Projectile_Text.text = Projectile_Names[Index];

        StartCoroutine(Scale_Lerp(Sector_Images[Index].transform,Min_Scale * Vector3.one, Max_Scale * Vector3.one, Duration_Scale));
        StartCoroutine(Scale_Lerp(Ring_Images[Index].transform, Min_Scale * Vector3.one, Max_Scale * Vector3.one, Duration_Scale));
       // StartCoroutine(Scale_Lerp(Projectile_Icon[Index].transform, Min_Scale * Vector3.one, Max_Scale * Vector3.one, Duration_Scale));
    }

    public void Projectile_Wheel_DeSelect(int Index)
    {
        Sector_Images[Index].color = Projectile_Colours[6];
        Ring_Images[Index].color = Projectile_Colours[6];


        StartCoroutine(Scale_Lerp(Sector_Images[Index].transform, Max_Scale * Vector3.one, Min_Scale * Vector3.one, Duration_Scale));
        StartCoroutine(Scale_Lerp(Ring_Images[Index].transform, Max_Scale * Vector3.one, Min_Scale * Vector3.one, Duration_Scale));
        //  StartCoroutine(Scale_Lerp(Projectile_Icon[Index].transform, Max_Scale * Vector3.one, Min_Scale * Vector3.one, Duration_Scale));
    }

    private IEnumerator Delay_DeSelect(int t)
    {
        yield return new WaitForSecondsRealtime(0.01f);
        Projectile_Wheel_DeSelect(t);
    }

    private IEnumerator Scale_Lerp(Transform transform, Vector3 Current_Scale, Vector3 New_Scale, float Duration)
    {
        float Time_Elapsed = 0;
         while (Time_Elapsed < Duration)
         {
            float t = Time_Elapsed / Duration;
            transform.localScale = Vector3.Slerp(Current_Scale , New_Scale , t);
            Time_Elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale = New_Scale;

    }

}
