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

    [SerializeField] private SpaceShip_Movement_Controller SpaceShip_Movement_Controller;

    // UI sliders representing throttle and handling levers

    [SerializeField] private Slider Throttle_Lever;
    [SerializeField] private Slider Handling_Lever;

    // Events triggered based on throttle level

    public UnityEvent Low_Throttle;
    public UnityEvent Moderate_Throttle;
    public UnityEvent High_Throttle;

    // Events triggered based on handling level

    public UnityEvent Low_Handling;
    public UnityEvent Moderate_Handling;
    public UnityEvent High_Handling;

    // Called when the script instance is being loaded
    void Start()
    {
        // Bind each event to the corresponding method in the movement controller

        Low_Throttle.AddListener(SpaceShip_Movement_Controller.Low_Throttle);
        Low_Handling.AddListener(SpaceShip_Movement_Controller.Low_Handling);

        Moderate_Throttle.AddListener(SpaceShip_Movement_Controller.Moderate_Throttle);
        Moderate_Handling.AddListener(SpaceShip_Movement_Controller.Moderate_Handling);

        High_Throttle.AddListener(SpaceShip_Movement_Controller.High_Throttle);
        High_Handling.AddListener(SpaceShip_Movement_Controller.High_Handling);

        // Set initial preset values (low throttle and handling)

        Preset_Throttle_Hnadling();
    }

    /// <summary>
    /// Sets the throttle and handling to their lowest levels by default.
    /// Also resets the UI sliders to 0.
    /// </summary>
    
    private void Preset_Throttle_Hnadling()
    {
        Low_Throttle.Invoke();
        Low_Handling.Invoke();
        Throttle_Lever.value = 0;
        Handling_Lever.value = 0;
    }

    /// <summary>
    /// Called when the throttle lever value changes.
    /// Invokes the corresponding event based on the slider value.
    /// </summary>

    public void Throttle_Function()
    {
        switch (Throttle_Lever.value)
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
    
    public void Handling_Function()
    {
        switch (Handling_Lever.value)
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
