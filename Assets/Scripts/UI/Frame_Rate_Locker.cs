using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Frame_Rate_Locker : MonoBehaviour
{
    [SerializeField] private int FPS = 60;

    [SerializeField] private TextMeshProUGUI FPS_Counter;
    [SerializeField] private float Polling_Time = 3f;
    private float Time_Clock;
    private int Frame_Count;

    private void Update()
    {
        Time_Clock += Time.deltaTime;
        Frame_Count++;


        if(Time_Clock >= Polling_Time)
        {
            int Frame_Rate = Mathf.RoundToInt(Frame_Count / Time_Clock);
            FPS_Counter.text = Frame_Count.ToString() + " FPS";


            Time_Clock -= Polling_Time;
            Frame_Count = 0;
        }
    }


}
