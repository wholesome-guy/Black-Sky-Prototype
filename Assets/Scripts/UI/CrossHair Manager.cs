using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairManager : MonoBehaviour
{
     private float Max_Shoot_Distance;  // Maximum shooting range of the cannon
     private GameObject Cannonn_Right_Tip;    // Right cannon tip game object
     private GameObject Cannonn_Left_Tip;     // Left cannon tip game object

    [SerializeField] private RectTransform CrossHair_Left;
    [SerializeField] private RectTransform CrossHair_Right;
    [SerializeField] private Image Left_Crosshair_Image;
    [SerializeField] private Image Right_Crosshair_Image;

    [SerializeField] private float Min_Scale = 0.5f;
    [SerializeField] private float Max_Scale = 0.75f;
    [SerializeField] private float Crosshair_Lerp_Speed = 10f;

    public LayerMask Hit_Mask;

    [SerializeField] private Color No_Hit_Colour = new Vector4(252, 226, 88,50);
    [SerializeField] private Color Hit_Colour = new Vector4(168, 31, 40, 50);


    private void Start()
    {
        Cannonn_Left_Tip = PlayerSingleton.instance.Left_Cannon_Tip;
        Cannonn_Right_Tip = PlayerSingleton.instance.Right_Cannon_Tip;
        Max_Shoot_Distance = PlayerSingleton.instance.Max_Shoot_Distance;
    }
    private void Update()
    {
        Crosshair_RayCaster();
        Debug.DrawRay(Cannonn_Left_Tip.transform.position, Cannonn_Left_Tip.transform.forward * Max_Shoot_Distance, Color.white);
        Debug.DrawRay(Cannonn_Right_Tip.transform.position, Cannonn_Right_Tip.transform.forward * Max_Shoot_Distance, Color.white);
    }


    private void Crosshair_RayCaster()
    {
        Ray leftRay = new Ray(Cannonn_Left_Tip.transform.position, Cannonn_Left_Tip.transform.forward);
        Ray rightRay = new Ray(Cannonn_Right_Tip.transform.position, Cannonn_Right_Tip.transform.forward);

        if (Physics.Raycast(leftRay, out RaycastHit leftHit, Max_Shoot_Distance, Hit_Mask))
        {
            RayCast_Hit_Crosshair(CrossHair_Left, Left_Crosshair_Image, leftHit.point);
        }
        else
        {
            RayCast_No_Hit_Crosshair(CrossHair_Left, Left_Crosshair_Image);
        }

        if (Physics.Raycast(rightRay, out RaycastHit rightHit, Max_Shoot_Distance, Hit_Mask))
        {
            RayCast_Hit_Crosshair(CrossHair_Right, Right_Crosshair_Image, rightHit.point);
        }
        else
        {
            RayCast_No_Hit_Crosshair(CrossHair_Right , Right_Crosshair_Image);

        }
    }
    private void RayCast_Hit_Crosshair(RectTransform CrossHair, Image Crosshair_Image, Vector3 World_Position)
    {
        Vector3 Screen_Position = Camera.main.WorldToScreenPoint(World_Position);
        CrossHair.position = Vector3.Lerp(CrossHair.position, Screen_Position, Time.deltaTime * Crosshair_Lerp_Speed);
        CrossHair.localScale = Vector3.Lerp(CrossHair.localScale, Vector3.one * Max_Scale, Time.deltaTime * Crosshair_Lerp_Speed);
        Crosshair_Image.color = Hit_Colour;
        CrossHair.rotation = Quaternion.identity;
    }

    private void RayCast_No_Hit_Crosshair(RectTransform Crosshair, Image Crosshair_Image)
    {
        Vector3 CrossHair_World_Coordinates = ((Cannonn_Left_Tip.transform.position + Cannonn_Left_Tip.transform.forward * Max_Shoot_Distance) + (Cannonn_Right_Tip.transform.position + Cannonn_Right_Tip.transform.forward * Max_Shoot_Distance)) * 0.5f;
        Vector3 Crosshair_Screen_Coordinates = Camera.main.WorldToScreenPoint(CrossHair_World_Coordinates);
        Crosshair.position = Vector3.Lerp(Crosshair.position, Crosshair_Screen_Coordinates, Time.deltaTime * Crosshair_Lerp_Speed);
        Crosshair.localScale = Vector3.Lerp(Crosshair.localScale, Vector3.one * Min_Scale, Time.deltaTime * Crosshair_Lerp_Speed);
        Crosshair_Image.color =No_Hit_Colour;
        Crosshair.rotation = Quaternion.identity;
    }
}
