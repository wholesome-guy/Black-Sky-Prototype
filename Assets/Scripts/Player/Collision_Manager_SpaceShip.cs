using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collision_Manager_SpaceShip : MonoBehaviour
{
    public UnityEvent Refuel_Event;             // Event triggered when the ship enters or exits refuel station
    public static Action<float> Take_Damage;    // Static action to notify damage with damage amount as float

    public static Action<Vector3> Debris_VFX;

    private string Refuel_Station_Area = "Refuel_Station";   // Tag used to identify refuel station objects
    private string Docking_Station_Area = "Docking Zone";   // Tag used to identify refuel station objects

    [Range(0,10)]
    [SerializeField] private int Delay_Duration = 5;
    private PlayerSingleton Player_Singleton;

    [SerializeField] private MoneyValues Money_Values;

    private WaitForSeconds WaitForSeconds_Delay_Duration;

    private void Start()
    {
        Player_Singleton = PlayerSingleton.instance;
        WaitForSeconds_Delay_Duration = new WaitForSeconds(Delay_Duration);
    }
    // Called when another collider enters this object's trigger collider
    private void OnTriggerEnter(Collider Collided_GameObject)
    {
        // If the collided object has the refuel station tag, invoke refuel event
        if (Collided_GameObject.gameObject.CompareTag(Refuel_Station_Area))
        {
            Refuel_Event.Invoke();
            MoneyManager.Money_Change_Event.Invoke(Money_Values.Refuel_Station_Cost);
            Player_Singleton.Is_Spaceship_At_Rest = true;
            Player_Singleton.Player_Rigidbody.drag = 10;
            Player_Singleton.Player_Rigidbody.angularDrag = 10;
            TimerManager.Timer_Delay_Event.Invoke(Delay_Duration);

            StartCoroutine(Leave_Refuel_Station());

        }
        if (Collided_GameObject.gameObject.CompareTag(Docking_Station_Area))
        {
            MoneyManager.Money_Change_Event.Invoke(Money_Values.Docking_Station_Cost);
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
        Debris_VFX.Invoke(Collided_GameObject.contacts[0].point);
        // Invoke the Take_Damage event with a fixed damage amount of 5000
        Take_Damage.Invoke(5000);
        CameraManager.Camera_Shake_Event.Invoke();
    }

    private IEnumerator Leave_Refuel_Station()
    {
        yield return WaitForSeconds_Delay_Duration;
        PlayerSingleton.instance.Is_Spaceship_At_Rest = false;
        Player_Singleton.Player_Rigidbody.drag = 1;
        Player_Singleton.Player_Rigidbody.angularDrag = 1;

    }
}
