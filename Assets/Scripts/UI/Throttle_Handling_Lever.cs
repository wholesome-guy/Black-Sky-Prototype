using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Controls the UI levers for throttle and handling,
/// and triggers appropriate events to update the spaceship's movement states.
/// </summary>

public class Throttle_Handling_Lever : MonoBehaviour
{
    // Reference to the spaceship movement controller

    private SpaceShip_Movement_Controller SpaceShip_Movement_Controller;
    [SerializeField] private Settings Game_Settings;
    [SerializeField] private ThrottleSteeringIconSprite Icon_Script;

 
    // Events triggered based on throttle level

    public UnityEvent Low_Throttle;
    public UnityEvent Moderate_Throttle;
    public UnityEvent High_Throttle;

    // Events triggered based on handling level

    public UnityEvent Low_Handling;
    public UnityEvent Moderate_Handling;
    public UnityEvent High_Handling;

    // Called when the script instance is being loaded

    private void OnEnable()
    {
        ThrottleMenuManager.Throttle_Select_Event += Throttle_Select;
        HandlingMenuManager.Handling_Select_Event += Handling_Select;
    }
    private void OnDisable()
    {
        ThrottleMenuManager.Throttle_Select_Event -= Throttle_Select; 
        HandlingMenuManager.Handling_Select_Event -= Handling_Select;

    }
    void Start()
    {
        // Bind each event to the corresponding method in the movement controller
        SpaceShip_Movement_Controller = PlayerSingleton.instance.SpaceShip_Movement_Controller;

        Low_Throttle.AddListener(SpaceShip_Movement_Controller.Low_Throttle);

        Moderate_Throttle.AddListener(SpaceShip_Movement_Controller.Moderate_Throttle);

        High_Throttle.AddListener(SpaceShip_Movement_Controller.High_Throttle);

        Low_Handling.AddListener(SpaceShip_Movement_Controller.Low_Handling);

        Moderate_Handling.AddListener(SpaceShip_Movement_Controller.Moderate_Handling);

        High_Handling.AddListener(SpaceShip_Movement_Controller.High_Handling);

        if (!Game_Settings.Steering_Full_Control)
        {
            Low_Throttle.AddListener(SpaceShip_Movement_Controller.Low_Handling);
            Moderate_Throttle.AddListener(SpaceShip_Movement_Controller.Moderate_Handling);
            High_Throttle.AddListener(SpaceShip_Movement_Controller.High_Handling);

            Low_Throttle.AddListener(Icon_Script.Steering_Low);
            Moderate_Throttle.AddListener(Icon_Script.Steering_Moderate);
            High_Throttle.AddListener(Icon_Script.Steering_High);
        }
       
        // Set initial preset values (low throttle and handling)

        Preset_Throttle_Hnadling();
    }

    private void Throttle_Select(int Index)
    {
        Throttle_Function(Index);
    }
    private void Handling_Select(int Index)
    {
        Handling_Function(Index);
    }

    public void Steering_Automatic_Manual()
    {
        if (Game_Settings.Steering_Full_Control)
        {
            Low_Throttle.RemoveListener(SpaceShip_Movement_Controller.Low_Handling);
            Moderate_Throttle.RemoveListener(SpaceShip_Movement_Controller.Moderate_Handling);
            High_Throttle.RemoveListener(SpaceShip_Movement_Controller.High_Handling);

            Low_Throttle.RemoveListener(Icon_Script.Steering_Low);
            Moderate_Throttle.RemoveListener(Icon_Script.Steering_Moderate);
            High_Throttle.RemoveListener(Icon_Script.Steering_High);
        }
        else
        {
            Low_Throttle.AddListener(SpaceShip_Movement_Controller.Low_Handling);
            Moderate_Throttle.AddListener(SpaceShip_Movement_Controller.Moderate_Handling);
            High_Throttle.AddListener(SpaceShip_Movement_Controller.High_Handling);

            Low_Throttle.AddListener(Icon_Script.Steering_Low);
            Moderate_Throttle.AddListener(Icon_Script.Steering_Moderate);
            High_Throttle.AddListener(Icon_Script.Steering_High);
        }
    }


    /// <summary>
    /// Sets the throttle and handling to their lowest levels by default.
    /// Also resets the UI sliders to 0.
    /// </summary>

    private void Preset_Throttle_Hnadling()
    {
        Low_Throttle.Invoke();
        Low_Handling.Invoke();
        
    }

    /// <summary>
    /// Called when the throttle lever value changes.
    /// Invokes the corresponding event based on the slider value.
    /// </summary>

    public void Throttle_Function(int Index)
    {
        switch (Index)
        {
            case 0:

                Low_Throttle.Invoke();
                
                break;
            case 1:

                Moderate_Throttle.Invoke();
                
                break;
            case 2:

                High_Throttle.Invoke();
                
                break;

        }
    }

    /// <summary>
    /// Called when the handling lever value changes.
    /// Invokes the corresponding event based on the slider value.
    /// </summary>
    
    public void Handling_Function(int Index)
    {
        switch (Index)
        {
            case 0:

                Low_Handling.Invoke();
                
                break;
            case 1:

                Moderate_Handling.Invoke();
                
                break;
            case 2:

                High_Handling.Invoke();
                
                break;

        }
    }

}
