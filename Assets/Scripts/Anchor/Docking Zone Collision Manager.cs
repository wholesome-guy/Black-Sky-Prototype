using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DockingZoneCollisionManager : MonoBehaviour
{
    // Static action invoked when the player docks (enters the docking zone)
    public static Action On_Player_Docked;

    // Called when another collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            On_Player_Docked.Invoke(); // Notify subscribers that the player has docked
        }
    }

    // Called when another collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 10f); // Destroy this docking zone after 10 seconds
        }
    }
}
