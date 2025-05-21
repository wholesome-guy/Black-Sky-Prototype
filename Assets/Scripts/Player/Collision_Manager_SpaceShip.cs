using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collision_Manager_SpaceShip : MonoBehaviour
{
    public UnityEvent Refuel_Event;
    public static Action<float> Take_Damage;

    private string Refuel_Station_Area = "Refuel_Station";


    private void OnTriggerEnter(Collider Collided_GameObject)
    {
        if (Collided_GameObject.gameObject.CompareTag(Refuel_Station_Area))
        {
            Refuel_Event.Invoke();
        }
    }
    private void OnTriggerExit(Collider Collided_GameObject)
    {
        if (Collided_GameObject.gameObject.CompareTag(Refuel_Station_Area))
        {       
            Refuel_Event.Invoke();
        }
    }

    private void OnCollisionEnter(Collision Collided_GameObject)
    {
           Take_Damage.Invoke(5000);
    }


}
