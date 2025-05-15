using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Camera_Manager_SpaceShip : MonoBehaviour
{
    public UnityEvent Positive_Vertical_Camera_Offset;
    public UnityEvent Negative_Vertical_Camera_Offset;
    public UnityEvent Ideal_Camera_Offset;
    [SerializeField] private float Positive_Vertical_Angle_X_Lower_Value = -35f;
    [SerializeField] private float Positive_Vertical_Angle_X_Higher_Value = -170f;

    [SerializeField] private float Negative_Vertical_Angle_X_Lower_Value = 65f;
    [SerializeField] private float Negative_Vertical_Angle_X_Higher_Value = 100f;
    private bool Is_Camera_Offseted = false;
    private float Angle_X;

    // Update is called once per frame
    void Update()
    {
       Angle_X = gameObject.transform.rotation.eulerAngles.x;
       Positive_Vetical_Camera_Offseter();
       Negative_Vetical_Camera_Offseter();
       Ideal_Camera_Offseter();
    }

    private void Positive_Vetical_Camera_Offseter()
    {
        if (!Is_Camera_Offseted && (Angle_Calculator(Angle_X) <= Positive_Vertical_Angle_X_Lower_Value && Angle_Calculator(Angle_X) >= Positive_Vertical_Angle_X_Higher_Value))
        {
            Positive_Vertical_Camera_Offset.Invoke();
            Is_Camera_Offseted = true;  
            StartCoroutine(Bool_Flag_Reseter());
        }
    }
    private void Negative_Vetical_Camera_Offseter()
    {
        if (!Is_Camera_Offseted && Angle_Calculator(Angle_X) >= Negative_Vertical_Angle_X_Lower_Value && Angle_Calculator(Angle_X)<= Negative_Vertical_Angle_X_Higher_Value)
        {
            Negative_Vertical_Camera_Offset.Invoke();
            Is_Camera_Offseted = true;
            StartCoroutine(Bool_Flag_Reseter());
        }
    }

    private void Ideal_Camera_Offseter()
    {
        if(Is_Camera_Offseted && Angle_Calculator(Angle_X) < Negative_Vertical_Angle_X_Lower_Value && Angle_Calculator(Angle_X) > Positive_Vertical_Angle_X_Lower_Value)
        {
            Ideal_Camera_Offset.Invoke();
        }
    }


    private IEnumerator Bool_Flag_Reseter()
    {
        yield return new WaitForSeconds(2);
        Is_Camera_Offseted = false;
        yield return null;
    }  

    private float Angle_Calculator(float angle)
    {
        return angle > 180 ? angle - 360 : angle;
    }
}
