using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass_Element : MonoBehaviour
{

    [SerializeField] private Transform Player;
    [SerializeField] private RectTransform Compass;
    [SerializeField] private float Compass_Sensitivity;
    [SerializeField] private float Lower_Snap_Value;
    [SerializeField] private float Upper_Snap_Value;
    

    // Update is called once per frame
    void LateUpdate()
    {
        Compass_Angle_Function();

        if(Compass.anchoredPosition.x > Upper_Snap_Value|| Compass.anchoredPosition.x < Lower_Snap_Value)
        {
            Compass.anchoredPosition = Vector2.zero;
        }
        
    }

    private void Compass_Angle_Function()
    {
        float Degree_Of_Rotation = (-1) * (Player.rotation.eulerAngles.y) * (Compass_Sensitivity);

        Compass.anchoredPosition = new Vector3(Degree_Of_Rotation, 0 , 0);
    }
}
