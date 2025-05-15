using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Spaceship_Fuel_System : MonoBehaviour
{
    private float Max_Fuel;
    private float Fuel_Consumption;
    private float Current_Fuel;
    private float Refuel_Amount;

    private float Ratio_Of_Current_To_Max_Fuel;
    private float Fill_Amount;
    private float Percentage_Fuel;

    [SerializeField] private SpaceShipValues SpaceShipValues;
    [SerializeField] private Image Fuel_Fill_Bar;
    [SerializeField] private TextMeshProUGUI Fuel_Amount_Text;

    public UnityEvent Fuel_Exhausted;

    private bool Fuel_Exhausted_Event_Check;
    private bool Is_Refueling;

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
        Is_Refueling = false;
        Fuel_Exhausted_Event_Check = false;
        Max_Fuel = SpaceShipValues.Max_Fuel;
        Current_Fuel = Max_Fuel;     
        Refuel_Amount = SpaceShipValues.Refuel_Amount;
        Fuel_Fill_Bar.fillAmount = 1;
        Fuel_Amount_Text.text = "100%";
    }

    private void Update()
    {
        Fuel_Consumption_Function();
        Fuel_Exhausted_Function();

        Ratio_Of_Current_To_Max_Fuel = Current_Fuel / Max_Fuel;
        Percentage_Fuel = Mathf.RoundToInt(Ratio_Of_Current_To_Max_Fuel * 100);

        Update_Fuel_UI();

        if(Is_Refueling)
        {
            Refuel_Function();
        }
    }

    private void Update_Fuel_UI()
    {
        Fill_Amount = Mathf.Lerp(Fuel_Fill_Bar.fillAmount,Ratio_Of_Current_To_Max_Fuel,Time.deltaTime);
        Fuel_Fill_Bar.fillAmount = Fill_Amount;
        Fuel_Amount_Text.text = Percentage_Fuel + "%";
    }

    private void Fuel_Consumption_Function()
    {
        if(Keyboard_Input_Manager.instance.Keyboard_Input.y != 0)
        {
            Current_Fuel -= Fuel_Consumption*Time.deltaTime;
            Current_Fuel = Mathf.Clamp(Current_Fuel,0f,Max_Fuel);
           
        }
    }

    private void Fuel_Exhausted_Function()
    {
        if(Current_Fuel == 0f && !Fuel_Exhausted_Event_Check)
        {
            Fuel_Exhausted.Invoke();
            Fuel_Exhausted_Event_Check = true;
        }
    }

    public void Refueling_Bool()
    {
        Is_Refueling = !Is_Refueling;
    }

    public void Refuel_Function()
    {
        Fuel_Exhausted_Event_Check = false;
        Current_Fuel += Refuel_Amount * Time.deltaTime;
        Current_Fuel = Mathf.Clamp(Current_Fuel, 0f, Max_Fuel);
    }

}
