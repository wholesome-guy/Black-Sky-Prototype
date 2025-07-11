using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private GameObject Timer;
    [SerializeField] private Image Fill_Image;
    [SerializeField] private TextMeshProUGUI Timer_Text;

    public static Action<float> Timer_Delay_Event;

    private void OnEnable()
    {
        Timer_Delay_Event += Timer_Funtion;
    }
    private void OnDisable()
    {
        Timer_Delay_Event -= Timer_Funtion;
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
}
