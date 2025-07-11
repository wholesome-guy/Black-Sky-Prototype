using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DockingZoneCollisionManager : MonoBehaviour
{
    public static Action On_Player_Docked;
    public static Action On_Player_Undocked;

    [SerializeField] private float Delay_Duration = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TimerManager.Timer_Delay_Event.Invoke(Delay_Duration);
            StartCoroutine(Delay_On_Player_Dock(Delay_Duration));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            On_Player_Undocked.Invoke();
            Destroy(gameObject, 10f); 
        }
    }

    private IEnumerator Delay_On_Player_Dock(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        On_Player_Docked.Invoke();
    }
}
