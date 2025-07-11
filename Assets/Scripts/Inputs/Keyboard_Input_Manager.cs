using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class Keyboard_Input_Manager : MonoBehaviour
{
    public static Keyboard_Input_Manager instance;
    private SpaceShipControls SpaceShip_Controls_Action_Map;

    // Stores current keyboard movement input
    public Vector2 Keyboard_Input;

    // UnityEvents to hook up HUD toggling and ammo switching
    public UnityEvent On_Chnage_HUD;
    public UnityEvent Projectile_Wheel_Display;
    public UnityEvent Projectile_Wheel_Hide;
    public static Action De_Tether;

    [SerializeField] private float Delay_Duration = 2.0f;
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
        SpaceShip_Controls_Action_Map = new SpaceShipControls();
    }

    private void OnEnable()
    {
        // Enable the input system and register input callbacks
        SpaceShip_Controls_Action_Map.Enable();
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.HUDSwitch.performed += HUD_Switch;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ProjectileWheel.performed += Projectile_Wheel_Display_Function;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ProjectileWheel.canceled += Projectile_Wheel_Hide_Function;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.UnTether.performed += De_Tether_Fuction;
    }

    private void OnDisable()
    {
        // Disable input system and unregister callbacks
        SpaceShip_Controls_Action_Map.Disable();
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.HUDSwitch.performed -= HUD_Switch;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ProjectileWheel.performed -= Projectile_Wheel_Display_Function;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ProjectileWheel.canceled -= Projectile_Wheel_Hide_Function;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.UnTether.performed -= De_Tether_Fuction;

    }

    // Called at a fixed time step for consistent physics-based input reading
    void FixedUpdate()
    {
        Keyboard_Input = SpaceShip_Controls_Action_Map.SpaceShip_Controls.Movement.ReadValue<Vector2>();
    }

    // Toggles HUD on key press
    public void HUD_Switch(InputAction.CallbackContext context)
    {
        Is_HUD_On = !Is_HUD_On;
        On_Chnage_HUD.Invoke();
    }

    // Triggers ammo switching event
    private void Projectile_Wheel_Display_Function(InputAction.CallbackContext context)
    {
        Projectile_Wheel_Display.Invoke();
    }
    private void Projectile_Wheel_Hide_Function(InputAction.CallbackContext context)
    {
        Projectile_Wheel_Hide.Invoke();
    }

    private void De_Tether_Fuction(InputAction.CallbackContext context)
    {
        if(PlayerSingleton.instance.Is_Anchored)
        {
            TimerManager.Timer_Delay_Event.Invoke(Delay_Duration);
            StartCoroutine(Delay_Detether(Delay_Duration));
        }        
    }

    private IEnumerator Delay_Detether(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        De_Tether.Invoke();
    }
}

