using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Mouse_Input_Manager : MonoBehaviour
{
    // Singleton instance for global access
    public static Mouse_Input_Manager instance;

    // Reference to Input Action Asset
    private SpaceShipControls Spaceship_Controls;

    // Raw and normalized mouse input
    private Vector2 Mouse_Input;

    public Vector2 Normalised_Mouse_Input;

    // Mouse sensitivity multiplier (currently unused but public)
    public int Mouse_Sensitivity;

    // Controls whether spaceship rotation is locked
    public bool Is_Rotation_Locked;

    // Flag to prevent shooting when near lever UI
    public bool Is_Mouse_At_Lever_Area;

    // Event triggered when shooting
    public UnityEvent Shoot;


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
        Spaceship_Controls = new SpaceShipControls();
    }

    /// <summary>
    /// Enable input controls and subscribe to events when script is enabled
    /// </summary>
    private void OnEnable()
    {
        Spaceship_Controls.Enable();
        Spaceship_Controls.SpaceShip_Controls.RotationLock.performed += Rotation_Locker;
        Spaceship_Controls.SpaceShip_Controls.Shoot.performed += Shoot_Projectile;
    }

    /// <summary>
    /// Disable input controls and unsubscribe from events when script is disabled
    /// </summary>
    private void OnDisable()
    {
        Spaceship_Controls.Disable();
        Spaceship_Controls.SpaceShip_Controls.RotationLock.performed -= Rotation_Locker;
        Spaceship_Controls.SpaceShip_Controls.Shoot.performed -= Shoot_Projectile;
    }

    private void Start()
    {
        Is_Mouse_At_Lever_Area = false;
        Is_Rotation_Locked = false;
        Mouse_Sensitivity = 1;
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
        
    }

    /// <summary>
    /// Toggle spaceship rotation lock on key press
    /// </summary>
    private void Rotation_Locker(InputAction.CallbackContext context)
    {
        Is_Rotation_Locked = !Is_Rotation_Locked;
    }

    /// <summary>
    /// Toggle the lever area flag (used to prevent shooting near UI)
    /// </summary>
    public void Lever_Area_Bool()
    {
        Is_Mouse_At_Lever_Area = !Is_Mouse_At_Lever_Area;
    }

    /// <summary>
    /// Triggers the Shoot UnityEvent unless player is at lever area
    /// </summary>
    private void Shoot_Projectile(InputAction.CallbackContext context)
    {
        if (!Is_Mouse_At_Lever_Area)
        {
            Shoot.Invoke();
        }
    }
}
