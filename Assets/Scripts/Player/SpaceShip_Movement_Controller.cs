using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

/// <summary>
/// Controls the spaceship's physics-based movement including throttle, roll, pitch, and yaw.
/// </summary>

public class SpaceShip_Movement_Controller : MonoBehaviour
{
    //Roll

     private float Roll;
     private float Min_Roll;
     private float Max_Roll;

    //Pitch

    private float Pitch;
    private float Min_Pitch;
    private float Max_Pitch;
    
    //Yaw

    private float Yaw;
    private float Min_Yaw;
    private float Max_Yaw;
    
    //Throttle
   
    private float Throttle;
    private float Min_Throttle = 0;
    private float Max_Throttle;
    private float Nitro;


    [SerializeField] private Rigidbody Rb;

    // Reference to spaceship configuration values

    [SerializeField] private SpaceShipValues SpaceShipValues;

    // Determines whether fuel is exhausted (used to disable movement)

    private bool Is_Fuel_Exhuasted;
    private bool Is_Nitro_Exhuasted;



    
    private void Start()
    {
        // Assign the ship's mass from config values

        Rb.mass = SpaceShipValues.Mass;
        Nitro = SpaceShipValues.Nitro;

        // Ensure the input manager is present

        if (Mouse_Input_Manager.instance == null)
        {
            Debug.LogError("Mouse_Input_Manager singleton is missing");
        }

        Is_Fuel_Exhuasted = false;
    }
    

    private void FixedUpdate()
    {
        if (!PlayerSingleton.instance.Is_Spaceship_At_Rest)
        {
            // Apply movement only if fuel is available
            if (!Is_Fuel_Exhuasted)
            {
                Linear_Movement();
            }

            // Apply rotation only if rotation is not locked

            if (!Mouse_Input_Manager.instance.Is_Rotation_Locked)
            {
                Rotational_Movement();
            }

            if (Keyboard_Input_Manager.instance.Is_Nitro_On && !Is_Nitro_Exhuasted)
            {
                Nitro_Function();
            }
        }

        Dampening_Velocity();

    }




    // Toggle fuel exhaustion state
    public void Fuel_Exhuasted_Bool()
    {
        Is_Fuel_Exhuasted = !Is_Fuel_Exhuasted;
    }
    public void Nitro_Exhuasted_Bool()
    {
        Is_Nitro_Exhuasted = !Is_Nitro_Exhuasted;
    }
    public void Nitro_Drag()
    {
        if(Keyboard_Input_Manager.instance.Is_Nitro_On && !Is_Nitro_Exhuasted)
        {
            High_Handling();
            High_Throttle();
        }
        else
        {
            Low_Handling();
            Low_Throttle();
        }
    }


    #region Spaceship Configuration Functions
    public void Low_Throttle()
    {
        Max_Throttle = SpaceShipValues.Max_Low_Throttle;
        Rb.drag = SpaceShipValues.Low_Linear_Drag;
    }
    public void Low_Handling()
    {
        Min_Roll = SpaceShipValues.Min_Low_Roll;
        Min_Pitch = SpaceShipValues.Min_Low_Pitch;
        Min_Yaw = SpaceShipValues.Min_Low_Yaw;
        Max_Roll = SpaceShipValues.Max_Low_Roll;
        Max_Pitch = SpaceShipValues.Max_Low_Pitch;
        Max_Yaw = SpaceShipValues.Max_Low_Yaw;
        Rb.angularDrag = SpaceShipValues.Low_Angular_Drag;
    }
    public void Moderate_Throttle()
    {
        Max_Throttle = SpaceShipValues.Max_Moderate_Throttle;
        Rb.drag = SpaceShipValues.Moderate_Linear_Drag;
    }
    public void Moderate_Handling()
    {
        Min_Roll = SpaceShipValues.Min_Moderate_Roll;
        Min_Pitch = SpaceShipValues.Min_Moderate_Pitch;
        Min_Yaw = SpaceShipValues.Min_Moderate_Yaw;
        Max_Roll = SpaceShipValues.Max_Moderate_Roll;
        Max_Pitch = SpaceShipValues.Max_Moderate_Pitch;
        Max_Yaw = SpaceShipValues.Max_Moderate_Yaw;
        Rb.angularDrag = SpaceShipValues.Moderate_Angular_Drag;
    }
    public void High_Throttle()
    {
        Max_Throttle = SpaceShipValues.Max_High_Throttle;
        Rb.drag = SpaceShipValues.High_Linear_Drag;
    }
    public void High_Handling()
    {
        Min_Roll = SpaceShipValues.Min_High_Roll;
        Min_Pitch = SpaceShipValues.Min_High_Pitch;
        Min_Yaw = SpaceShipValues.Min_High_Yaw;
        Max_Roll = SpaceShipValues.Max_High_Roll;
        Max_Pitch = SpaceShipValues.Max_High_Pitch;
        Max_Yaw = SpaceShipValues.Max_High_Yaw;
        Rb.angularDrag = SpaceShipValues.High_Angular_Drag;
    }
    #endregion

    // Handles linear (throttle) movement

    private void Linear_Movement()
    {
        Throttle_Function();
    }

    // Handles rotational (pitch, roll, yaw) movement

    private void Rotational_Movement()
    {
        Roll_Function();
        Pitch_Function();
        Yaw_Function();
    }

    #region Movement Functions

    // Applies forward thrust based on vertical input

    private void Throttle_Function()
    {
        Rb.AddForce(Rb.transform.TransformDirection(Vector3.forward) * Keyboard_Input_Manager.instance.Keyboard_Input.y * Throttle , ForceMode.Force);
        if (Keyboard_Input_Manager.instance.Keyboard_Input.y != 0)
        {
            Throttle = Mathf.MoveTowards(Throttle, Max_Throttle, (Max_Throttle) / SpaceShipValues.Min_To_Max_Duartion_Throttle * Time.fixedDeltaTime);
        }
        else
        {
           
           Throttle = Min_Throttle;
        }
    }
    private void Nitro_Function()
    {
        Rb.AddForce(Rb.transform.TransformDirection(Vector3.forward) * Keyboard_Input_Manager.instance.Keyboard_Input.y * Nitro, ForceMode.Force);
    }

    // Applies roll torque based on horizontal input

    private void Roll_Function()
    {
        Rb.AddTorque(Rb.transform.TransformDirection(Vector3.forward) * Keyboard_Input_Manager.instance.Keyboard_Input.x * Roll, ForceMode.Force);
        if (Keyboard_Input_Manager.instance.Keyboard_Input.x != 0)
        {
            // if (Roll_Coroutine != null) { StopCoroutine(Roll_Coroutine); }
            // Roll_Coroutine = StartCoroutine(Lerping_Routine(Min_Roll, Max_Roll, SpaceShipValues.Min_To_Max_Duartion_Roll, (float Value) => Roll = Value));
            Roll = Mathf.MoveTowards(Roll, Max_Roll, (Max_Roll - Min_Roll) / SpaceShipValues.Min_To_Max_Duartion_Roll * Time.fixedDeltaTime);
        }
        else
        {
            
            Roll = Min_Roll;
        }
    }

    // Applies pitch torque based on vertical mouse movement

    private void Pitch_Function()
    {
        
        Rb.AddTorque(Rb.transform.TransformDirection(Vector3.right) * Mouse_Input_Manager.instance.Normalised_Mouse_Input.y * Pitch * -1 * Mouse_Input_Manager.instance.Mouse_Sensitivity, ForceMode.Force);
        if (Mouse_Input_Manager.instance.Normalised_Mouse_Input.y != 0)
        {
            // if (Pitch_Coroutine != null) { StopCoroutine(Pitch_Coroutine); }
            // Pitch_Coroutine = StartCoroutine(Lerping_Routine(Min_Pitch, Max_Pitch, SpaceShipValues.Min_To_Max_Duartion_Pitch, (float Value) => Pitch = Value));
            Pitch = Mathf.MoveTowards(Pitch, Max_Pitch, (Max_Pitch - Min_Pitch) / SpaceShipValues.Min_To_Max_Duartion_Pitch * Time.fixedDeltaTime);
        }
        else
        {

            Pitch = Min_Pitch;
        }
    }

    // Applies yaw torque based on horizontal mouse movement

    private void Yaw_Function()
    {
        Rb.AddTorque(Rb.transform.TransformDirection(Vector3.up) * Mouse_Input_Manager.instance.Normalised_Mouse_Input.x * Yaw * Mouse_Input_Manager.instance.Mouse_Sensitivity, ForceMode.Force);
        if (Mouse_Input_Manager.instance.Normalised_Mouse_Input.x != 0)
        {
            // if (Yaw_Coroutine != null) { StopCoroutine(Yaw_Coroutine); }
            // Yaw_Coroutine = StartCoroutine(Lerping_Routine(Min_Yaw, Max_Yaw, SpaceShipValues.Min_To_Max_Duartion_Yaw, (float Value) => Yaw = Value));
            Yaw = Mathf.MoveTowards(Yaw, Max_Yaw, (Max_Yaw - Min_Yaw) / SpaceShipValues.Min_To_Max_Duartion_Yaw * Time.fixedDeltaTime);
        }
        else
        {

            Yaw = Min_Yaw;
        }
    }
    private void Dampening_Velocity()
    {
        if (PlayerSingleton.instance.Is_Anchored)
        {
            float dampening = (1 - PlayerSingleton.instance.Dampening_Factor);
            Rb.velocity *= dampening;
            Rb.angularVelocity *= dampening;
        }       
    }

    #endregion

}
