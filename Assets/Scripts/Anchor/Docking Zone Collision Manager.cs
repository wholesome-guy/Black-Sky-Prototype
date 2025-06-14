using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DockingZoneCollisionManager : MonoBehaviour
{
    public static Action On_Player_Docked;
    public static Action On_Player_Undocked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            On_Player_Docked.Invoke(); 
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
}
