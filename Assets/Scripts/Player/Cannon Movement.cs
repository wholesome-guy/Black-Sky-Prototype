using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [SerializeField] private float Max_Shoot_Distance;
    private float Distance_Between_Cannon_And_Centre = 6.0f;

   

    [SerializeField] private GameObject Cannonn_Right_Tip;
    [SerializeField] private GameObject Cannonn_Left_Tip;


    private void Start()
    {
        Cannon_Angle_Offset_Calculator();
    }
    private void Cannon_Angle_Offset_Calculator()
    {
        float Angle_Offset = 90f - (Mathf.Atan(Max_Shoot_Distance / Distance_Between_Cannon_And_Centre) * (180 / 3.14f));
        
        Cannonn_Left_Tip.transform.localRotation = Quaternion.Euler(0, Angle_Offset, 0);
        Cannonn_Right_Tip.transform.localRotation = Quaternion.Euler(0, -Angle_Offset, 0);

    }
  
}
