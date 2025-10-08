using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;
using DG.Tweening;
using System.Threading.Tasks;

public class UIVisualEffectsManager : MonoBehaviour
{
    [SerializeField] private Volume Global_Volume;

    private DepthOfField Depth_Of_Field;
    [SerializeField] private float Focus_Distance = 1.75f;
    [SerializeField] private int Focal_Length = 130;
    [SerializeField] private int Aperture = 32;

    public static bool Is_Pointer_Hovering;
    public static Action Blur_Slow_Time_Event;
    public static Action Unblur_Normal_Time_Event;

    public static Action<CanvasGroup, float, float, float> UI_Fader_Event;

    public static Action<float, float, float> Transition_Screen_Event;


    [SerializeField] private Transform Transition_Transform;

    private void OnEnable()
    {
        Blur_Slow_Time_Event += Blur_TimeSlow;
        Unblur_Normal_Time_Event += Unblur_Time_Normal;

        UI_Fader_Event += UI_Fader_Function;

        Transition_Screen_Event += Transition_Effect;
    }
    private void OnDisable()
    {
        Blur_Slow_Time_Event -= Blur_TimeSlow;
        Unblur_Normal_Time_Event -= Unblur_Time_Normal;

        UI_Fader_Event -= UI_Fader_Function;
        Transition_Screen_Event -= Transition_Effect;

    }
    private void Start()
    {
        if (Global_Volume.profile.TryGet<DepthOfField>(out Depth_Of_Field))
        {
            Depth_Of_Field.active = false;
            Depth_Of_Field.focusDistance.value = Focus_Distance;
            Depth_Of_Field.focalLength.value = Focal_Length;
        }
        Depth_Of_Field.aperture.value = Aperture;

        Transition_Transform.gameObject.SetActive(false);

    }

    private void Blur_TimeSlow()
    {
        Depth_Of_Field.active = true;
        Time.timeScale = 0.25f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void Unblur_Time_Normal()
    {
        Depth_Of_Field.active = false;
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void UI_Fader_Function(CanvasGroup Canvas_Group, float Start_Value, float End_Value, float Duration)
    {
        StartCoroutine(UI_Fader(Canvas_Group, Start_Value, End_Value, Duration));
    }

    private IEnumerator UI_Fader(CanvasGroup Canvas_Group, float Start_Value,float End_Value, float Duration)
    {
        float t = 0;
        Canvas_Group.alpha = Start_Value;
        while(t < Duration)
        {
            t += Time.deltaTime;
            Canvas_Group.alpha = Mathf.Lerp(Start_Value, End_Value, t / Duration);
            yield return null;
        }

        Canvas_Group.alpha = End_Value;
    }

    public void UI_Pointer_Hover()
    {
        Is_Pointer_Hovering = !Is_Pointer_Hovering;
    }

    private  void Transition_Effect(float X_Start,float X_End,float Duration)
    {
        Transition_Transform.gameObject.SetActive(true);

        Transition_Transform.localPosition = new Vector3(X_Start,0,0);
        Vector3 End_Position = new Vector3 (X_End,0,0);

        Transition_Transform.DOLocalMove(End_Position, Duration);

    }
}
