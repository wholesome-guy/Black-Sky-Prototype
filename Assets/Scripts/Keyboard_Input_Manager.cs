using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_Input_Manager : MonoBehaviour
{
    public static Keyboard_Input_Manager instance;
    private SpaceShipControls Spaceship_Controls;

    public Vector2 Keyboard_Input;

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
    }

    private void OnDisable()
    {
        Spaceship_Controls.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Keyboard_Input = Spaceship_Controls.SpaceShip_Controls.Movement.ReadValue<Vector2>();
    }
}
