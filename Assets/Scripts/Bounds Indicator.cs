using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoundsIndicator : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private RectTransform Bounds_Circle;
    [SerializeField] private Image Bounds_Circle_Image;

    [SerializeField] private float X_Z_Limits;
    [SerializeField] private float Y_Limit;

    private float Positive_Y_Constant; // 108/+ve Y Limit
    private float Negative_Y_Constant; // 34/ -ve Y Limit
    private float Y_Constant;

     private float X_Z_Constant; // 56/ X and Z Limit

    private float Positive_Scale_Constant;  // 0.5/ X AND Z Limit
    private float Negative_Scale_Constant;  // 0.2/ X And Z Limit
    private float Scale_Constant;

    [SerializeField] private TextMeshProUGUI Coordinates_Text;
    private void Start()
    {
        Positive_Y_Constant = 108f / Y_Limit;
        Negative_Y_Constant = 34f / Y_Limit;

        X_Z_Constant = 56f / X_Z_Limits;

        Positive_Scale_Constant = 0.5f / X_Z_Limits;
        Negative_Scale_Constant = 0.2f / X_Z_Limits;
    }
    void Update()
    {
        Direction_Constant();
        Bounds_Position();
        Bounds_Scale();
        Coordinates_Text_Updater();
    }
    private void Direction_Constant()
    {
        if(Player.position.y > 0 )
        {
            Y_Constant = Positive_Y_Constant;
        }
        else
        {
            Y_Constant = Negative_Y_Constant;
        }

        if( (Player.position.x > 0) || (Player.position.z < 0))
        {
            Scale_Constant = Positive_Scale_Constant;
        }
        else
        {
            Scale_Constant = Negative_Scale_Constant;
        }
    }

    private void Bounds_Position()
    {
       Vector3 Ship_Location_On_Cube = new Vector3((Player.position.x + Player.position.z) * X_Z_Constant , Player.position.y * Y_Constant , 0);
       Bounds_Circle.anchoredPosition = Ship_Location_On_Cube;
    }
    private void Bounds_Scale()
    {
        float scale = (Player.position.x - Player.position.z) * Scale_Constant;

        scale = Mathf.Clamp(scale, 0.2f, 0.5f);

        Bounds_Circle.localScale = new Vector3(scale, scale, 1f);
    }

    public void Coordinates_Text_Idle()
    {
        Coordinates_Text.enabled = false;
    }
    public void Coordinates_Text_Hover()
    {
        Coordinates_Text.enabled = true;
    }

    private void Coordinates_Text_Updater()
    {
        int X_Coordinate_Player_Int, Y_Coordinate_Player_Int, Z_Coordinate_Player_Int;

        X_Coordinate_Player_Int = Mathf.RoundToInt(Player.position.x);
        Y_Coordinate_Player_Int = Mathf .RoundToInt(Player.position.y);
        Z_Coordinate_Player_Int = Mathf.RoundToInt (Player.position.z);

        string Coordinates_String = X_Coordinate_Player_Int + "," + Y_Coordinate_Player_Int + "," + Z_Coordinate_Player_Int;

        Coordinates_Text.text = Coordinates_String;

    }

}
