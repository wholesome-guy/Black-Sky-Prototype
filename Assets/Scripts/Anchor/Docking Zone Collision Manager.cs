using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DockingZoneCollisionManager : MonoBehaviour
{
    public static Action<Vector3, Vector3> On_Player_Docked;
    private string Right_Ship_Anchor_Name = "Right_Anchor";
    private string Left_Ship_Anchor_Name = "Left_Anchor";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 Right_Anchor_Position = other.gameObject.transform.Find(Right_Ship_Anchor_Name).position;
            Vector3 Left_Anchor_Position = other.gameObject.transform.Find(Left_Ship_Anchor_Name).position;

            On_Player_Docked.Invoke(Left_Anchor_Position, Right_Anchor_Position);   


        }
    }


   
}
