using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Keyboard_Input_Manager : MonoBehaviour
{
    public static Keyboard_Input_Manager instance;
    private SpaceShipControls Spaceship_Controls;

    // Stores current keyboard movement input
    public Vector2 Keyboard_Input;

    // UnityEvents to hook up HUD toggling and ammo switching
    public UnityEvent On_Chnage_HUD;
    public UnityEvent Ammo_Switch;

    // Tracks whether the HUD is currently visible
    public bool Is_HUD_On = true;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        // Initialize the generated input actions
        Spaceship_Controls = new SpaceShipControls();
    }

    private void OnEnable()
    {
        // Enable the input system and register input callbacks
        Spaceship_Controls.Enable();
        Spaceship_Controls.SpaceShip_Controls.HUDSwitch.performed += HUD_Switch;
        Spaceship_Controls.SpaceShip_Controls.SwtichAmmo.performed += Ammo_Switch_Funtion;
    }

    private void OnDisable()
    {
        // Disable input system and unregister callbacks
        Spaceship_Controls.Disable();
        Spaceship_Controls.SpaceShip_Controls.HUDSwitch.performed -= HUD_Switch;
        Spaceship_Controls.SpaceShip_Controls.SwtichAmmo.performed -= Ammo_Switch_Funtion;
    }

    // Called at a fixed time step for consistent physics-based input reading
    void FixedUpdate()
    {
        Keyboard_Input = Spaceship_Controls.SpaceShip_Controls.Movement.ReadValue<Vector2>();
    }

    // Toggles HUD on key press
    public void HUD_Switch(InputAction.CallbackContext context)
    {
        Is_HUD_On = !Is_HUD_On;
        On_Chnage_HUD.Invoke();
    }

    // Triggers ammo switching event
    private void Ammo_Switch_Funtion(InputAction.CallbackContext context)
    {
        Ammo_Switch.Invoke();
    }
}
