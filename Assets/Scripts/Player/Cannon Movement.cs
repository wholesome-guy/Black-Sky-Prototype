using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    private float Max_Shoot_Distance;  // Maximum shooting range of the cannon
    private float Distance_Between_Cannon_And_Centre = 6.0f;  // Fixed distance from cannon to center point

     private GameObject Cannonn_Right_Tip;    // Right cannon tip game object
     private GameObject Cannonn_Left_Tip;     // Left cannon tip game object

   
    // Called on start, calculates the initial angle offset for both cannon tips
    private void Start()
    {
        Max_Shoot_Distance = PlayerSingleton.instance.Max_Shoot_Distance;
        Cannonn_Left_Tip = PlayerSingleton.instance.Left_Cannon_Tip;
        Cannonn_Right_Tip = PlayerSingleton.instance.Right_Cannon_Tip;
        Cannon_Angle_Offset_Calculator();
    }

    // Calculates angle offset for cannon tips based on max shooting distance and distance from center
    private void Cannon_Angle_Offset_Calculator()
    {
        // Calculate angle offset in degrees using inverse tangent and conversion from radians to degrees
        float Angle_Offset = 90f - (Mathf.Atan(Max_Shoot_Distance / Distance_Between_Cannon_And_Centre) * (180 / 3.14f));

        // Set local rotation of left cannon tip with positive angle offset on Y axis
        Cannonn_Left_Tip.transform.localRotation = Quaternion.Euler(0, Angle_Offset, 0);
        // Set local rotation of right cannon tip with negative angle offset on Y axis
        Cannonn_Right_Tip.transform.localRotation = Quaternion.Euler(0, -Angle_Offset, 0);
    }
   
}
