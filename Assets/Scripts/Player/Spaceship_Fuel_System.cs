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
    // Fuel stats
    private float Max_Fuel;
    private float Fuel_Consumption;
    private float Current_Fuel;
    private float Refuel_Amount;

    // UI values
    private float Ratio_Of_Current_To_Max_Fuel;
    private float Fill_Amount;
    private float Percentage_Fuel;

    [Header("Data & UI References")]
    [SerializeField] private SpaceShipValues SpaceShipValues;   // Reference to spaceship stats
    [SerializeField] private Image Fuel_Fill_Bar;               // UI bar to show fuel level
    [SerializeField] private TextMeshProUGUI Fuel_Amount_Text;  // Text to show fuel percentage

    [Header("Events")]
    public UnityEvent Fuel_Exhausted;                           // Event triggered when fuel hits zero

    private bool Fuel_Exhausted_Event_Check;                    // To ensure the event only fires once
    private bool Is_Refueling;                                  // Tracks if ship is in refueling mode

    // Sets fuel consumption for low throttle level
    public void Low_Throttle_Fuel_Compustion()
    {
        Fuel_Consumption = SpaceShipValues.Low_Throttle_Fuel_Consumption;
    }

    // Sets fuel consumption for moderate throttle level
    public void Moderate_Throttle_Fuel_Compustion()
    {
        Fuel_Consumption = SpaceShipValues.Moderate_Throttle_Fuel_Consumption;
    }

    // Sets fuel consumption for high throttle level
    public void High_Throttle_Fuel_Compustion()
    {
        Fuel_Consumption = SpaceShipValues.High_Throttle_Fuel_Consumption;
    }

    // Initialization on game start
    private void Start()
    {
        Is_Refueling = false;
        Fuel_Exhausted_Event_Check = false;
        Max_Fuel = SpaceShipValues.Max_Fuel;
        Current_Fuel = Max_Fuel;
        Refuel_Amount = SpaceShipValues.Refuel_Amount;

        // Set UI to full
        Fuel_Fill_Bar.fillAmount = 1;
        Fuel_Amount_Text.text = "100%";
    }

    // Runs every frame
    private void Update()
    {
        Fuel_Consumption_Function();      // Consume fuel based on input
        Fuel_Exhausted_Function();        // Check if fuel is empty

        // Update UI values
        Ratio_Of_Current_To_Max_Fuel = Current_Fuel / Max_Fuel;
        Percentage_Fuel = Mathf.RoundToInt(Ratio_Of_Current_To_Max_Fuel * 100);
        Update_Fuel_UI();

        // If refueling is enabled, refuel gradually
        if (Is_Refueling)
        {
            Refuel_Function();
        }
    }

    // Smoothly updates the fuel bar and percentage text
    private void Update_Fuel_UI()
    {
        Fill_Amount = Mathf.Lerp(Fuel_Fill_Bar.fillAmount, Ratio_Of_Current_To_Max_Fuel, Time.deltaTime);
        Fuel_Fill_Bar.fillAmount = Fill_Amount;
        Fuel_Amount_Text.text = Percentage_Fuel + "%";
    }

    // Handles gradual fuel consumption when input is active
    private void Fuel_Consumption_Function()
    {
        if (Keyboard_Input_Manager.instance.Keyboard_Input.y != 0)
        {
            Current_Fuel -= Fuel_Consumption * Time.deltaTime;
            Current_Fuel = Mathf.Clamp(Current_Fuel, 0f, Max_Fuel);
        }
    }

    // Invokes fuel exhausted event once when fuel hits zero
    private void Fuel_Exhausted_Function()
    {
        if (Current_Fuel == 0f && !Fuel_Exhausted_Event_Check)
        {
            Fuel_Exhausted.Invoke();
            Fuel_Exhausted_Event_Check = true;
        }
    }

    // Toggles the refueling state (used by triggers or refuel stations)
    public void Refueling_Bool()
    {
        Is_Refueling = !Is_Refueling;
    }

    // Gradually restores fuel over time
    public void Refuel_Function()
    {
        Fuel_Exhausted_Event_Check = false; // Reset exhaustion check when refueling
        Current_Fuel += Refuel_Amount * Time.deltaTime;
        Current_Fuel = Mathf.Clamp(Current_Fuel, 0f, Max_Fuel);
    }
}