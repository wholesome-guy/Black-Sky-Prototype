using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse_Input_Manager : MonoBehaviour
{
    public static Mouse_Input_Manager instance;
    private SpaceShipControls Spaceship_Controls;

    private Vector2 Mouse_Input;
    public Vector2 Normalised_Mouse_Input; 
    public int Mouse_Sensitivity;
   
    public bool Is_Rotation_Locked;


    private void Awake()
    {
        if(instance != null && instance !=this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        // Get the C# Script youu generated, ie the action map Script 
        Spaceship_Controls = new SpaceShipControls();
    }

    /// <summary>
    /// Enable the  action map script 'OnEnable' and Subscribe to the Movement Action
    /// Disable the action map script 'OnDisable' and Unsubscribe to the Movemnent Action
    /// </summary>
    

    private void OnEnable()
    {
        Spaceship_Controls.Enable();
        Spaceship_Controls.SpaceShip_Controls.RotationLock.performed += Rotation_Locker;
    }

    private void OnDisable()
    {
        Spaceship_Controls.Disable();
        Spaceship_Controls.SpaceShip_Controls.RotationLock.performed -= Rotation_Locker;

    }

     void Start()
     {
        Is_Rotation_Locked = false;
        Mouse_Sensitivity = 1;
     }


    void Update()
    {
        Normalised_Mouse_Input_Method();
    }

    void Normalised_Mouse_Input_Method()
    {
        //New Input System way to get Mouse Position
        Mouse_Input = Mouse.current.position.ReadValue();
        //Making sure the Mouse Input stays on Screen
        Mouse_Input.x = Mathf.Clamp(Mouse_Input.x, 0, Screen.width);
        Mouse_Input.y = Mathf.Clamp(Mouse_Input.y, 0, Screen.height);

        Normalised_Mouse_Input = new Vector2((Mouse_Input.x / Screen.width) * 2f - 1f, (Mouse_Input.y / Screen.height) * 2f - 1f);
    }
    private void Rotation_Locker(InputAction.CallbackContext context)
    {
        Is_Rotation_Locked = !Is_Rotation_Locked;
    }

}
