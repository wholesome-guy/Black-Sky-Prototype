using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass_Element : MonoBehaviour
{

    [SerializeField] private Transform Player;
    [SerializeField] private RectTransform Compass;
    [SerializeField] private int Compass_Sensitivity;
    

    // Update is called once per frame
    void LateUpdate()
    {
        Compass_Angle_Function();
    }

    private void Compass_Angle_Function()
    {
        float Degree_Of_Rotation = (-1) * (Player.rotation.eulerAngles.y) * (Compass_Sensitivity);
        
        Compass.anchoredPosition = new Vector3(Degree_Of_Rotation, 445, 0);
    }
}
