using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private GameObject Timer;
    [SerializeField] private Image Fill_Image;
    [SerializeField] private TextMeshProUGUI Timer_Text;

    [SerializeField] private Image Cannon_Reload_Image;
    [SerializeField] private Color Ready_To_Shoot_Colour;
    [SerializeField] private Color Not_Ready_To_Shoot_Colour;

    public static Action<float> Timer_Delay_Event;
    public static Action<float> Cannon_Reload_Event;

    private void OnEnable()
    {
        Timer_Delay_Event += Timer_Funtion;
        Cannon_Reload_Event += Cannon_Reload_Function;
    }
    private void OnDisable()
    {
        Timer_Delay_Event -= Timer_Funtion;
        Cannon_Reload_Event -= Cannon_Reload_Function;
    }

    private void Start()
    {
        Timer.SetActive(false);
    }
   
    private void Timer_Funtion(float Duration)
    {
        Timer.SetActive(true);
        StartCoroutine(Timer_Coroutine(Duration));
    }

    private IEnumerator Timer_Coroutine(float Duration)
    {
        float t = Duration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            Fill_Image.fillAmount = t / Duration;
            Timer_Text.text = t.ToString("0.00");
            yield return null;
        }
        Timer.SetActive(false);
    }

    private void Cannon_Reload_Function(float Duration)
    {
        StartCoroutine(Cannon_Reload_Coroutine(Duration));
    }
    private IEnumerator Cannon_Reload_Coroutine(float Duration)
    {
        float t = 0;
        Cannon_Reload_Image.color = Not_Ready_To_Shoot_Colour;
        while (t < Duration)
        {
            t += Time.deltaTime;
            Cannon_Reload_Image.fillAmount = t / Duration;
            yield return null;
        }
        Sequence Pop = DOTween.Sequence();
        Pop.Append(Cannon_Reload_Image.transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutFlash));
        Cannon_Reload_Image.color = Ready_To_Shoot_Colour;
        Pop.Append(Cannon_Reload_Image.transform.DOScale(1.0f, 0.1f).SetEase(Ease.OutFlash));
    }

}
