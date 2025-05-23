using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collision_Manager_SpaceShip : MonoBehaviour
{
    public UnityEvent Refuel_Event;             // Event triggered when the ship enters or exits refuel station
    public static Action<float> Take_Damage;    // Static action to notify damage with damage amount as float

    private string Refuel_Station_Area = "Refuel_Station";   // Tag used to identify refuel station objects

    // Called when another collider enters this object's trigger collider
    private void OnTriggerEnter(Collider Collided_GameObject)
    {
        // If the collided object has the refuel station tag, invoke refuel event
        if (Collided_GameObject.gameObject.CompareTag(Refuel_Station_Area))
        {
            Refuel_Event.Invoke();
        }
    }

    // Called when another collider exits this object's trigger collider
    private void OnTriggerExit(Collider Collided_GameObject)
    {
        // If the collided object has the refuel station tag, invoke refuel event again
        // (Possibly to stop refueling)
        if (Collided_GameObject.gameObject.CompareTag(Refuel_Station_Area))
        {
            Refuel_Event.Invoke();
        }
    }

    // Called when a collision (non-trigger) happens with this object
    private void OnCollisionEnter(Collision Collided_GameObject)
    {
        // Invoke the Take_Damage event with a fixed damage amount of 5000
        Take_Damage.Invoke(5000);
    }
}
