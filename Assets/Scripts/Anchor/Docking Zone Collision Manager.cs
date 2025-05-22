using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DockingZoneCollisionManager : MonoBehaviour
{
    //public static Action<Vector3, Vector3> On_Player_Docked;

    public UnityEvent On_Player_Docked;
    
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Delay_On_Player_Docked());
        }
    }

    IEnumerator Delay_On_Player_Docked()
    {
        yield return new WaitForSeconds(2f);
        
        On_Player_Docked.Invoke();
    }

   
}
