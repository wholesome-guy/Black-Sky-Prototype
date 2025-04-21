using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsIndicator : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private RectTransform Bounds_Circle;

    [SerializeField] private float Positive_Y_Constant;
    [SerializeField] private float Negative_Y_Constant;
    [SerializeField] private float Y_Constant;

    [SerializeField] private float X_Z_Constant;

    [SerializeField] private float Positive_Scale_Constant;
    [SerializeField] private float Negative_Scale_Constant;
    [SerializeField] private float Scale_Constant;

    
    void Update()
    {
        Direction_Constant();
        Bounds_Position();
        Bounds_Scale();
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

        scale = Mathf.Clamp(scale, 0.25f, 0.5f);

        Bounds_Circle.localScale = new Vector3(scale, scale, 1f);
    }
}
