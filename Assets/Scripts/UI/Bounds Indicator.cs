using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoundsIndicator : MonoBehaviour
{
    [SerializeField] private Transform Player;                 // Reference to the player transform
    [SerializeField] private RectTransform Bounds_Circle;      // UI element representing the bounds indicator
    [SerializeField] private Image Bounds_Circle_Image;        // Image component of the bounds circle (not currently used in code)

    [SerializeField] private float X_Z_Limits;                 // Limit for X and Z axis for calculation
    [SerializeField] private float Y_Limit;                    // Limit for Y axis for calculation

    private float Positive_Y_Constant;      // Constant used when Player's Y is positive (108 / Y_Limit)
    private float Negative_Y_Constant;      // Constant used when Player's Y is negative (34 / Y_Limit)
    private float Y_Constant;               // Current Y constant chosen based on Player Y position

    private float X_Z_Constant;             // Constant used for X and Z axis scaling (56 / X_Z_Limits)

    private float Positive_Scale_Constant; // Scale constant when Player is in positive X or negative Z region
    private float Negative_Scale_Constant; // Scale constant when Player is in other region
    private float Scale_Constant;           // Current scale constant chosen based on Player position

    [SerializeField] private TextMeshProUGUI Coordinates_Text; // UI text displaying player's coordinates

    private void Start()
    {
        // Calculate constants for position and scale based on limits
        Positive_Y_Constant = 108f / Y_Limit;
        Negative_Y_Constant = 34f / Y_Limit;

        X_Z_Constant = 56f / X_Z_Limits;

        Positive_Scale_Constant = 0.5f / X_Z_Limits;
        Negative_Scale_Constant = 0.2f / X_Z_Limits;
    }

    void Update()
    {
        Direction_Constant();       // Decide which constants to use based on player position
        Bounds_Position();          // Update position of bounds circle on UI
        Bounds_Scale();             // Update scale of bounds circle on UI
        Coordinates_Text_Updater(); // Update coordinate text display
    }

    // Selects Y_Constant and Scale_Constant based on player's position in the world
    private void Direction_Constant()
    {
        if (Player.position.y > 0)
        {
            Y_Constant = Positive_Y_Constant;
        }
        else
        {
            Y_Constant = Negative_Y_Constant;
        }

        if ((Player.position.x > 0) || (Player.position.z < 0))
        {
            Scale_Constant = Positive_Scale_Constant;
        }
        else
        {
            Scale_Constant = Negative_Scale_Constant;
        }
    }

    // Calculates the UI position of the bounds indicator based on player position and constants
    private void Bounds_Position()
    {
        Vector3 Ship_Location_On_Cube = new Vector3(
            (Player.position.x + Player.position.z) * X_Z_Constant,
            Player.position.y * Y_Constant,
            0);

        Bounds_Circle.anchoredPosition = Ship_Location_On_Cube;
    }

    // Calculates and clamps the scale of the bounds circle based on player position and scale constant
    private void Bounds_Scale()
    {
        float scale = (Player.position.x - Player.position.z) * Scale_Constant;
        scale = Mathf.Clamp(scale, 0.2f, 0.5f);

        Bounds_Circle.localScale = new Vector3(scale, scale, 1f);
    }

    // Disables the coordinate text display (e.g., when idle)
    public void Coordinates_Text_Idle()
    {
        Coordinates_Text.enabled = false;
    }

    // Enables the coordinate text display (e.g., on hover)
    public void Coordinates_Text_Hover()
    {
        Coordinates_Text.enabled = true;
    }

    // Updates the coordinate text UI to show the player's current integer position
    private void Coordinates_Text_Updater()
    {
        int X_Coordinate_Player_Int = Mathf.RoundToInt(Player.position.x);
        int Y_Coordinate_Player_Int = Mathf.RoundToInt(Player.position.y);
        int Z_Coordinate_Player_Int = Mathf.RoundToInt(Player.position.z);

        string Coordinates_String = X_Coordinate_Player_Int + "," + Y_Coordinate_Player_Int + "," + Z_Coordinate_Player_Int;

        Coordinates_Text.text = Coordinates_String;
    }
}
