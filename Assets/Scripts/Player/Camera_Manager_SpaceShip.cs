using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Camera_Manager_SpaceShip : MonoBehaviour
{
    public UnityEvent Positive_Vertical_Camera_Offset;    // Event triggered for positive vertical camera offset
    public UnityEvent Negative_Vertical_Camera_Offset;    // Event triggered for negative vertical camera offset
    public UnityEvent Ideal_Camera_Offset;                // Event triggered when camera is in ideal offset position

    [SerializeField] private float Positive_Vertical_Angle_X_Lower_Value = -35f;   // Lower bound for positive vertical angle range
    [SerializeField] private float Positive_Vertical_Angle_X_Higher_Value = -170f; // Upper bound for positive vertical angle range

    [SerializeField] private float Negative_Vertical_Angle_X_Lower_Value = 65f;    // Lower bound for negative vertical angle range
    [SerializeField] private float Negative_Vertical_Angle_X_Higher_Value = 100f;  // Upper bound for negative vertical angle range

    private bool Is_Camera_Offseted = false;              // Flag to prevent repeated triggering of events
    private float Angle_X;                                 // Current camera rotation angle around the X axis

    // Called once per frame to check camera rotation and invoke events accordingly
    void Update()
    {
        Angle_X = gameObject.transform.rotation.eulerAngles.x;

        Positive_Vetical_Camera_Offseter();
        Negative_Vetical_Camera_Offseter();
        Ideal_Camera_Offseter();
    }

    // Checks if camera angle is within positive vertical offset range and invokes event if so
    private void Positive_Vetical_Camera_Offseter()
    {
        if (!Is_Camera_Offseted && (Angle_Calculator(Angle_X) <= Positive_Vertical_Angle_X_Lower_Value && Angle_Calculator(Angle_X) >= Positive_Vertical_Angle_X_Higher_Value))
        {
            Positive_Vertical_Camera_Offset.Invoke();
            Is_Camera_Offseted = true;
            StartCoroutine(Bool_Flag_Reseter());
        }
    }

    // Checks if camera angle is within negative vertical offset range and invokes event if so
    private void Negative_Vetical_Camera_Offseter()
    {
        if (!Is_Camera_Offseted && Angle_Calculator(Angle_X) >= Negative_Vertical_Angle_X_Lower_Value && Angle_Calculator(Angle_X) <= Negative_Vertical_Angle_X_Higher_Value)
        {
            Negative_Vertical_Camera_Offset.Invoke();
            Is_Camera_Offseted = true;
            StartCoroutine(Bool_Flag_Reseter());
        }
    }

    // Invokes event if camera returns to ideal angle range between positive and negative vertical bounds
    private void Ideal_Camera_Offseter()
    {
        if (Is_Camera_Offseted && Angle_Calculator(Angle_X) < Negative_Vertical_Angle_X_Lower_Value && Angle_Calculator(Angle_X) > Positive_Vertical_Angle_X_Lower_Value)
        {
            Ideal_Camera_Offset.Invoke();
        }
    }

    // Coroutine to reset the camera offset flag after a delay to prevent rapid repeated event firing
    private IEnumerator Bool_Flag_Reseter()
    {
        yield return new WaitForSeconds(2);
        Is_Camera_Offseted = false;
        yield return null;
    }

    // Converts an angle from 0-360 range to -180 to 180 range for easier comparison
    private float Angle_Calculator(float angle)
    {
        return angle > 180 ? angle - 360 : angle;
    }
}
