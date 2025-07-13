using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DockingZoneCollisionManager : MonoBehaviour
{
    public static Action On_Player_Docked;
    public static Action On_Player_Undocked;

    private bool Is_Player_Docked;

    [SerializeField] private float Delay_Duration = 5.0f;

    private void Start()
    {
        Is_Player_Docked = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!Is_Player_Docked)
            {
                TimerManager.Timer_Delay_Event.Invoke(Delay_Duration);
                StartCoroutine(Delay_On_Player_Dock(Delay_Duration));
            }
            
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
        Is_Player_Docked = true;
        On_Player_Docked.Invoke();
    }
}
