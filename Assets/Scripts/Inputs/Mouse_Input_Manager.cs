using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class Mouse_Input_Manager : MonoBehaviour
{
    // Singleton instance for global access
    public static Mouse_Input_Manager instance;

    // Reference to Input Action Asset
    private SpaceShipControls SpaceShip_Controls_Action_Map;

    // Raw and normalized mouse input
    private Vector2 Mouse_Input;

    public Vector2 Normalised_Mouse_Input;

    public float Angle_Mouse_Input;

    // Mouse sensitivity multiplier (currently unused but public)
    public float Mouse_Sensitivity;

    // Controls whether spaceship rotation is locked
    public bool Is_Rotation_Locked;
    public bool Is_Free_Aim_On = false;

    // Event triggered when shooting
    public static Action Shoot_Action;

    public UnityEvent Aim_Released_Event;


    private void Awake()
    {
        // Setup singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        // Instantiate the input control system
        SpaceShip_Controls_Action_Map = new SpaceShipControls();
    }

    /// <summary>
    /// Enable input controls and subscribe to events when script is enabled
    /// </summary>
    private void OnEnable()
    {
        SpaceShip_Controls_Action_Map.Enable();
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.RotationLock.performed += Rotation_Locker;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Shoot.performed += Shoot_Projectile;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Aim.performed += Aim_Held;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Aim.canceled += Aim_Released;
    }

    /// <summary>
    /// Disable input controls and unsubscribe from events when script is disabled
    /// </summary>
    private void OnDisable()
    {
        SpaceShip_Controls_Action_Map.Disable();
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.RotationLock.performed -= Rotation_Locker;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Shoot.performed -= Shoot_Projectile;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Aim.performed -= Aim_Held;
        SpaceShip_Controls_Action_Map.SpaceShip_Controls.Aim.canceled -= Aim_Released;
    }

    private void Start()
    {
        Is_Rotation_Locked = false;
    }

    private void Update()
    {
        Normalised_Mouse_Input_Method();
    }

    /// <summary>
    /// Calculates normalized mouse input (-1 to 1 range) based on screen size
    /// </summary>
    void Normalised_Mouse_Input_Method()
    {
        // Read the current mouse position using the new Input System
        Mouse_Input = Mouse.current.position.ReadValue();


        // Clamp to screen bounds
         Mouse_Input.x = Mathf.Clamp(Mouse_Input.x, 0, Screen.width);
         Mouse_Input.y = Mathf.Clamp(Mouse_Input.y, 0, Screen.height);

        // Normalize to range [-1, 1]
        Normalised_Mouse_Input = new Vector2((Mouse_Input.x / Screen.width) * 2f - 1f, (Mouse_Input.y / Screen.height) * 2f - 1f);

        //Angle between Mouse_Input and 1,0,0 WITH 0,0,1 as the axis of rotation
        Angle_Mouse_Input = Vector3.SignedAngle(Vector3.right, Normalised_Mouse_Input, Vector3.forward);
        
    }

    /// <summary>
    /// Toggle spaceship rotation lock on key press
    /// </summary>
    private void Rotation_Locker(InputAction.CallbackContext context)
    {
      Is_Rotation_Locked = !Is_Rotation_Locked;
    }

    private void Aim_Held(InputAction.CallbackContext callbackContext)
    {
        Is_Free_Aim_On = true;
    }
    private void Aim_Released(InputAction.CallbackContext callbackContext)
    {
        Is_Free_Aim_On =false;
        Aim_Released_Event.Invoke();
    }

    private void Shoot_Projectile(InputAction.CallbackContext context)
    {
        Shoot_Action.Invoke();
    }
}
