using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Keyboard_Input_Manager : MonoBehaviour
{
    public static Keyboard_Input_Manager instance;
    private SpaceShipControls Spaceship_Controls;

    public Vector2 Keyboard_Input;
    public UnityEvent On_Chnage_HUD;
    public UnityEvent Ammo_Switch;
    public bool Is_HUD_On = true;

    private void Awake()
    {
        if(instance != null && instance != this )
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        Spaceship_Controls = new SpaceShipControls();
    }

    
    
    private void OnEnable()
    {
        Spaceship_Controls.Enable();

        Spaceship_Controls.SpaceShip_Controls.HUDSwitch.performed += HUD_Switch;
        Spaceship_Controls.SpaceShip_Controls.SwtichAmmo.performed += Ammo_Switch_Funtion;
    }

    private void OnDisable()
    {
        Spaceship_Controls.Disable();
        Spaceship_Controls.SpaceShip_Controls.HUDSwitch.performed -= HUD_Switch;
        Spaceship_Controls.SpaceShip_Controls.SwtichAmmo.performed -= Ammo_Switch_Funtion;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Keyboard_Input = Spaceship_Controls.SpaceShip_Controls.Movement.ReadValue<Vector2>();
    }
    public void HUD_Switch(InputAction.CallbackContext context)
    {
        Is_HUD_On = !Is_HUD_On;
        On_Chnage_HUD.Invoke();

    }

    private void Ammo_Switch_Funtion(InputAction.CallbackContext context)
    {
        Ammo_Switch.Invoke();
    }

}
