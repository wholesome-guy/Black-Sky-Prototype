using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShip_Movement_Controller : MonoBehaviour
{

    [SerializeField] private float Roll;
                     private float Min_Roll;
                     private float Max_Roll;
    [SerializeField] private float Min_Low_Roll;
    [SerializeField] private float Min_Moderate_Roll;
    [SerializeField] private float Min_High_Roll;
    [SerializeField] private float Max_Low_Roll;
    [SerializeField] private float Max_Moderate_Roll;
    [SerializeField] private float Max_High_Roll;
    [SerializeField] private float Min_To_Max_Duartion_Roll;

    [SerializeField] private float Pitch;
                     private float Min_Pitch;
                     private float Max_Pitch;
    [SerializeField] private float Min_Low_Pitch;
    [SerializeField] private float Min_Moderate_Pitch;
    [SerializeField] private float Min_High_Pitch;
    [SerializeField] private float Max_Low_Pitch;
    [SerializeField] private float Max_Moderate_Pitch;
    [SerializeField] private float Max_High_Pitch;
    [SerializeField] private float Min_To_Max_Duartion_Pitch;

    [SerializeField] private float Yaw;
                     private float Min_Yaw;
                     private float Max_Yaw;
    [SerializeField] private float Min_Low_Yaw;
    [SerializeField] private float Min_Moderate_Yaw;
    [SerializeField] private float Min_High_Yaw;
    [SerializeField] private float Max_Low_Yaw;
    [SerializeField] private float Max_Moderate_Yaw;
    [SerializeField] private float Max_High_Yaw;
    [SerializeField] private float Min_To_Max_Duartion_Yaw;

    [SerializeField] private float Mass;

    [SerializeField] private float Low_Linear_Drag;
    [SerializeField] private float Moderate_Linear_Drag;
    [SerializeField] private float High_Linear_Drag;

    [SerializeField] private float Low_Angular_Drag;
    [SerializeField] private float Moderate_Angular_Drag;
    [SerializeField] private float High_Angular_Drag;

    [SerializeField] private float Throttle;
                     private float Min_Throttle;
                     private float Max_Throttle;
    [SerializeField] private float Max_Low_Throttle;
    [SerializeField] private float Max_Moderate_Throttle;
    [SerializeField] private float Max_High_Throttle;
    [SerializeField] private float Min_To_Max_Duartion_Throttle;



    private bool Is_Rotation_Locked;


    [SerializeField] private Rigidbody Rb;
    private SpaceShipControls Spaceship_Contols;
    private Vector2 Keyboard_Input;
   
    void Awake()
    {
        // Get the C# Script youu generated, ie the action map Script 
        Spaceship_Contols = new SpaceShipControls();
        if (Mouse_Input_Manager.instance == null)
        {
            Debug.LogError("Mouse_Input_Manager singleton is missing");
        }
        
        
    }
     

    /// <summary>
    /// Enable the  action map script 'OnEnable' and Subscribe to the Movement Action
    /// Disable the action map script 'OnDisable' and Unsubscribe to the Movemnent Action
    /// </summary>
    
    
    private void OnEnable()
    {
        Spaceship_Contols.Enable();
        Spaceship_Contols.SpaceShip_Controls.RotationLock.performed += Rotation_Locker;
      
    }
    private void OnDisable()
    {
        Spaceship_Contols.Disable();
        Spaceship_Contols.SpaceShip_Controls.RotationLock.performed -= Rotation_Locker;
    }
    private void Start()
    {
        Min_Throttle = 0;
        Min_Roll = 0;
        Min_Pitch = 0;
        Min_Yaw = 0;
        Max_Throttle = Max_Moderate_Throttle;
        Max_Roll = Max_Moderate_Roll;
        Max_Pitch = Max_Moderate_Pitch;
        Max_Yaw = Max_Moderate_Yaw;
        Rb.mass = Mass;
        Rb.drag = Moderate_Linear_Drag;
        Rb.angularDrag = Moderate_Angular_Drag;
        Is_Rotation_Locked = false;
    }


    private void FixedUpdate()
    {
        Keyboard_Input = Spaceship_Contols.SpaceShip_Controls.Movement.ReadValue<Vector2>();
        Linear_Movement();
        if(!Is_Rotation_Locked)
        {
            Rotational_Movement();
        }
        
    }

    /*
     * Do not Use FOR MOVEMENT, BIG NO NO. Movement in FIXED UPDATE ONLY.
     * 
     public void Movement(InputAction.CallbackContext context)
    {
        Input = context.ReadValue<Vector2>();
        Rb.AddForce(Rb.transform.TransformDirection(Vector3.forward) * Input.y * Throttle, ForceMode.Force);
    }
    */

    private void Linear_Movement()
    {
        
        Rb.AddForce(Rb.transform.TransformDirection(Vector3.forward) * Keyboard_Input.y * Throttle, ForceMode.Force);
        if(Keyboard_Input.y != 0)
        {
            StartCoroutine(Lerping_Routine(Min_Throttle, Max_Throttle, Min_To_Max_Duartion_Throttle, (float Value) => Throttle = Value));
        }
        else
        {
            Throttle = Min_Throttle;
        }
        
    }
    private void Low_Throttle()
    {
        Max_Throttle = Max_Low_Throttle;
        Rb.drag = Low_Linear_Drag;
    }
    private void Low_Handling()
    {
        Min_Roll = Min_Low_Roll;
        Min_Pitch = Min_Low_Pitch;
        Min_Yaw = Min_Low_Yaw;
        Max_Roll = Max_Low_Roll;
        Max_Pitch = Max_Low_Pitch;
        Max_Yaw = Max_Low_Yaw;
        Rb.angularDrag = Low_Angular_Drag;
    }
    private void Moderate_Throttle()
    {
        Max_Throttle = Max_Moderate_Throttle;
        Rb.drag = Moderate_Linear_Drag;
    }
    private void Moderate_Handling()
    {
        Min_Roll = Min_Moderate_Roll;
        Min_Pitch = Min_Moderate_Pitch;
        Min_Yaw = Min_Moderate_Yaw;
        Max_Roll = Max_Moderate_Roll;
        Max_Pitch = Max_Moderate_Pitch;
        Max_Yaw = Max_Moderate_Yaw;
        Rb.angularDrag = Moderate_Angular_Drag;
    }
    private void High_Throttle()
    {
        Max_Throttle = Max_High_Throttle;
        Rb.drag = High_Linear_Drag;
    }
    private void High_Handling()
    {
        Min_Roll = Min_High_Roll;
        Min_Pitch = Min_High_Pitch;
        Min_Yaw = Min_High_Yaw;
        Max_Roll = Max_High_Roll;
        Max_Pitch = Max_High_Pitch;
        Max_Yaw = Max_High_Yaw;
        Rb.angularDrag = High_Angular_Drag;
    }

    private void Rotational_Movement()
    {
        Rb.AddTorque(Rb.transform.TransformDirection(Vector3.forward) * Keyboard_Input.x * Roll, ForceMode.Force);
        if (Keyboard_Input.x != 0)
        {
            StartCoroutine(Lerping_Routine(Min_Roll, Max_Roll, Min_To_Max_Duartion_Roll, (float Value) => Roll = Value));
        }
        else
        {
            Roll = Min_Roll;
        }
        Rb.AddTorque(Rb.transform.TransformDirection(Vector3.right) * Mouse_Input_Manager.instance.Normalised_Mouse_Input.y * Pitch * -1 * Mouse_Input_Manager.instance.Mouse_Sensitivity , ForceMode.Force);
        if(Mouse_Input_Manager.instance.Normalised_Mouse_Input.y != 0)
        {
            StartCoroutine(Lerping_Routine(Min_Pitch,Max_Pitch,Min_To_Max_Duartion_Pitch,(float Value) => Pitch = Value));
        }
        else
        {
            Pitch = Min_Pitch;
        }
        Rb.AddTorque(Rb.transform.TransformDirection(Vector3.up) * Mouse_Input_Manager.instance.Normalised_Mouse_Input.x * Yaw * Mouse_Input_Manager.instance.Mouse_Sensitivity, ForceMode.Force);
        if (Mouse_Input_Manager.instance.Normalised_Mouse_Input.x != 0)
        {
            StartCoroutine(Lerping_Routine(Min_Yaw, Max_Yaw, Min_To_Max_Duartion_Yaw, (float Value) => Yaw = Value));
        }
        else
        {
            Yaw = Min_Yaw;
        }
    }
     private void Rotation_Locker(InputAction.CallbackContext context)
     {
        Is_Rotation_Locked = !Is_Rotation_Locked;
     }


    IEnumerator Lerping_Routine(float Min_Value,float Max_Value,float duration, System.Action<float> Lerped_Value)
    {
        
        float Time_Elapsed = 0;
        while(Time_Elapsed<duration)
        {
            float t = Time_Elapsed/duration;
            Lerped_Value(Mathf.Lerp(Min_Value, Max_Value, t));
            Time_Elapsed += Time.deltaTime;
            yield return null;
        }
        Lerped_Value(Max_Value);
    }
}
