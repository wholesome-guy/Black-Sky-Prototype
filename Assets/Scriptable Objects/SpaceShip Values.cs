using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpaceShip Values" , menuName ="ScriptableObjects/SpaceShip Values")]
public class SpaceShipValues : ScriptableObject
{
    // Roll

    [SerializeField] public float Min_Low_Roll;
    [SerializeField] public float Min_Moderate_Roll;
    [SerializeField] public float Min_High_Roll;
    [SerializeField] public float Max_Low_Roll;
    [SerializeField] public float Max_Moderate_Roll;
    [SerializeField] public float Max_High_Roll;
    [SerializeField] public float Min_To_Max_Duartion_Roll;

    //Pitch

    [SerializeField] public float Min_Low_Pitch;
    [SerializeField] public float Min_Moderate_Pitch;
    [SerializeField] public float Min_High_Pitch;
    [SerializeField] public float Max_Low_Pitch;
    [SerializeField] public float Max_Moderate_Pitch;
    [SerializeField] public float Max_High_Pitch;
    [SerializeField] public float Min_To_Max_Duartion_Pitch;

    //Yaw

    [SerializeField] public float Min_Low_Yaw;
    [SerializeField] public float Min_Moderate_Yaw;
    [SerializeField] public float Min_High_Yaw;
    [SerializeField] public float Max_Low_Yaw;
    [SerializeField] public float Max_Moderate_Yaw;
    [SerializeField] public float Max_High_Yaw;
    [SerializeField] public float Min_To_Max_Duartion_Yaw;

    //Throttle

    [SerializeField] public float Max_Low_Throttle;
    [SerializeField] public float Max_Moderate_Throttle;
    [SerializeField] public float Max_High_Throttle;
    [SerializeField] public float Min_To_Max_Duartion_Throttle;

    // Mass

    [SerializeField] public float Mass;

    //Linear Drag

    [SerializeField] public float Low_Linear_Drag;
    [SerializeField] public float Moderate_Linear_Drag;
    [SerializeField] public float High_Linear_Drag;

    //Angular Drag

    [SerializeField] public float Low_Angular_Drag;
    [SerializeField] public float Moderate_Angular_Drag;
    [SerializeField] public float High_Angular_Drag;

    //Fuel

    [SerializeField] public float Max_Fuel;
    [SerializeField] public float Low_Throttle_Fuel_Consumption;
    [SerializeField] public float Moderate_Throttle_Fuel_Consumption;
    [SerializeField] public float High_Throttle_Fuel_Consumption;
    [SerializeField] public float Refuel_Amount;

    //Health

    [SerializeField] public float Max_Health;
}
