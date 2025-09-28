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

    public UnityEvent Throttle_Wheel_Display;
    public UnityEvent Throttle_Wheel_Hide;

    public UnityEvent Handling_Wheel_Display;
    public UnityEvent Handling_Wheel_Hide;

    public UnityEvent Asteroid_Camera_On;
    public UnityEvent Asteroid_Camera_Off;

    public static Action De_Tether;
    public UnityEvent Nitro;
    public UnityEvent Steering_Automatic_Manual_Switch_Event;

    [SerializeField] private float Delay_Duration = 2.0f;
    // Tracks whether the HUD is currently visible
    public bool Is_HUD_On = true;
    public bool Is_Nitro_On = false;

    [SerializeField] private Settings Game_Settings;

    private WaitForSeconds WaitForSeconds_Delay_Duration;
    private PlayerSingleton Player_Singleton;

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

        WaitForSeconds_Delay_Duration = new WaitForSeconds(Delay_Duration);


    }
    private void Start()
    {
        Player_Singleton = PlayerSingleton.instance;
    }

    private void OnEnable()
    {
        // Enable the input system and register input callbacks
        SpaceShip_Controls_Action_Map.Enable();
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.HUDSwitch.performed += HUD_Switch;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Nitro.performed += Nitro_Switch;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Nitro.canceled += Nitro_Switch;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ProjectileWheel.performed += Projectile_Wheel_Display_Function;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ProjectileWheel.canceled += Projectile_Wheel_Hide_Function;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ThrottleWheel.performed += Throttle_Wheel_Display_Function;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ThrottleWheel.canceled += Throttle_Wheel_Hide_Function;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.SteeringWheel.performed += Handling_Wheel_Display_Function;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.SteeringWheel.canceled += Handling_Wheel_Hide_Function;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.AsteroidCamera.performed += Asteroid_Camera_Activate;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.AsteroidCamera.canceled += Asteroid_Camera_Deactivate;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.UnTether.performed += De_Tether_Fuction;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.SteeringFullControlSwitch.performed += Steering_Full_Control_Switch;
    }

    private void OnDisable()
    {
        // Disable input system and unregister callbacks
        SpaceShip_Controls_Action_Map.Disable();
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.HUDSwitch.performed -= HUD_Switch;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Nitro.performed -= Nitro_Switch;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Nitro.canceled -= Nitro_Switch;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ProjectileWheel.performed -= Projectile_Wheel_Display_Function;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ProjectileWheel.canceled -= Projectile_Wheel_Hide_Function;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ThrottleWheel.performed -= Throttle_Wheel_Display_Function;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.ThrottleWheel.canceled -= Throttle_Wheel_Hide_Function;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.SteeringWheel.performed -= Handling_Wheel_Display_Function;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.SteeringWheel.canceled -= Handling_Wheel_Hide_Function;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.AsteroidCamera.performed -= Asteroid_Camera_Activate;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.AsteroidCamera.canceled -= Asteroid_Camera_Deactivate;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.UnTether.performed -= De_Tether_Fuction;

        SpaceShip_Controls_Action_Map.SpaceShip_Controls.SteeringFullControlSwitch.performed -= Steering_Full_Control_Switch;

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
    private void Nitro_Switch(InputAction.CallbackContext context)
    {
        Is_Nitro_On = !Is_Nitro_On;

        Nitro.Invoke();
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

    private void Throttle_Wheel_Display_Function(InputAction.CallbackContext context)
    {
        Throttle_Wheel_Display.Invoke();
    }
    private void Throttle_Wheel_Hide_Function(InputAction.CallbackContext context)
    {
        Throttle_Wheel_Hide.Invoke();
    }
    private void Handling_Wheel_Display_Function(InputAction.CallbackContext context)
    {
        if (Game_Settings.Steering_Full_Control)
        {
            Handling_Wheel_Display.Invoke();
        }
    }
    private void Handling_Wheel_Hide_Function(InputAction.CallbackContext context)
    {
        if (Game_Settings.Steering_Full_Control)
        {
            Handling_Wheel_Hide.Invoke();
        }
    }

    private void Asteroid_Camera_Activate(InputAction.CallbackContext context)
    {
        Asteroid_Camera_On.Invoke();
    }
    private void Asteroid_Camera_Deactivate(InputAction.CallbackContext context)
    {
        Asteroid_Camera_Off.Invoke();
    }

    private void De_Tether_Fuction(InputAction.CallbackContext context)
    {
        if(Player_Singleton.Is_Anchored)
        {
            TimerManager.Timer_Delay_Event.Invoke(Delay_Duration);
            StartCoroutine(Delay_Detether());
        }        
    }

    private IEnumerator Delay_Detether()
    {
        yield return WaitForSeconds_Delay_Duration;
        De_Tether.Invoke();
    }

    private void Steering_Full_Control_Switch(InputAction.CallbackContext context)
    {
        Game_Settings.Steering_Full_Control = !Game_Settings.Steering_Full_Control;

        Steering_Automatic_Manual_Switch_Event.Invoke();
    }
}

