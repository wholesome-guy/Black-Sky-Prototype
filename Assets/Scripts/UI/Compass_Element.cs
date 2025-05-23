using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass_Element : MonoBehaviour
{
    [SerializeField] private Transform Player;               // Reference to the player's transform
    [SerializeField] private RectTransform Compass;           // Reference to the UI compass RectTransform
    [SerializeField] private float Compass_Sensitivity;       // Sensitivity multiplier for compass movement

    //Compass Sensitivity = CompassElement/ Y Angle of Player, = 413.604 / 45

    [SerializeField] private float Lower_Snap_Value;           // Lower threshold to snap the compass back to center
    [SerializeField] private float Upper_Snap_Value;           // Upper threshold to snap the compass back to center


    // Called once per frame, after all Update calls
    void LateUpdate()
    {
        Compass_Angle_Function();  // Update compass position based on player rotation

        // Snap the compass position back to center if it exceeds snap boundaries
        if (Compass.anchoredPosition.x > Upper_Snap_Value || Compass.anchoredPosition.x < Lower_Snap_Value)
        {
            Compass.anchoredPosition = Vector2.zero;
        }

    }

    // Calculates and applies the compass position based on the player's rotation
    private void Compass_Angle_Function()
    {
        // Calculate rotation degree based on player's Y rotation and sensitivity
        float Degree_Of_Rotation = (-1) * (Player.rotation.eulerAngles.y) * Compass_Sensitivity;

        // Move the compass horizontally according to the calculated degree
        Compass.anchoredPosition = new Vector3(Degree_Of_Rotation, 0, 0);
    }
}