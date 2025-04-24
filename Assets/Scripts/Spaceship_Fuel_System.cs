using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;

public class Spaceship_Fuel_System : MonoBehaviour
{
    private float Max_Fuel;
    private float Fuel_Consumption;
    private float Current_Fuel;

    [SerializeField] private SpaceShipValues SpaceShipValues;
    public UnityEvent Fuel_Exhuasted;

    private bool Fuel_Exhuasted_Event_Check;

    public void Low_Throttle_Fuel_Compustion()
    {
        Fuel_Consumption = SpaceShipValues.Low_Throttle_Fuel_Consumption;
    }
    public void Moderate_Throttle_Fuel_Compustion()
    {
        Fuel_Consumption = SpaceShipValues.Moderate_Throttle_Fuel_Consumption;
    }
    public void High_Throttle_Fuel_Compustion()
    {
        Fuel_Consumption = SpaceShipValues.High_Throttle_Fuel_Consumption;
    }
     
    private void Start()
    {
        Max_Fuel = SpaceShipValues.Max_Fuel;
        Current_Fuel = Max_Fuel;
        Fuel_Exhuasted_Event_Check = false;
    }

    private void Update()
    {
        Fuel_Consumption_Function();
        Fuel_Exhuasted_Function();
        
    }

    private void Fuel_Consumption_Function()
    {
        if(Keyboard_Input_Manager.instance.Keyboard_Input.y != 0)
        {
            Current_Fuel -= Fuel_Consumption*Time.deltaTime;
            Current_Fuel = Mathf.Clamp(Current_Fuel,0f,Max_Fuel);  
        }
    }

    private void Fuel_Exhuasted_Function()
    {
        if(Current_Fuel == 0f && !Fuel_Exhuasted_Event_Check)
        {
            Fuel_Exhuasted.Invoke();
            Fuel_Exhuasted_Event_Check = true;
        }
    }

    private void Refuel_Function()
    {
        Fuel_Exhuasted_Event_Check = true;
    }

}
