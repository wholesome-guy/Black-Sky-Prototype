using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CrossHairManager : MonoBehaviour
{
     private float Max_Shoot_Distance;  // Maximum shooting range of the cannon
     private float Distance_Between_Cannon_And_Centre = 6.0f;  // Fixed distance from cannon to center point
     private float Angle_Offset_Forward_Aim;
     private float Center_Offset_Angle = 9;

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

    [SerializeField] private GameObject Hit_Marker_Left;
    [SerializeField] private GameObject Hit_Marker_Right;

    private WaitForSeconds WaitForSeconds_0_2_5 = new WaitForSeconds(0.25f);


    public static Action Hit_Mark_Event;

    private Mouse_Input_Manager Mouse_Input_Manager_;
    private void OnEnable()
    {
        Hit_Mark_Event += Hit_Marker_Function;
    }
    private void OnDisable()
    {
        Hit_Mark_Event -= Hit_Marker_Function;
    }


    private void Start()
    {
        Cannonn_Left_Tip = PlayerSingleton.instance.Left_Cannon_Tip;
        Cannonn_Right_Tip = PlayerSingleton.instance.Right_Cannon_Tip;
        Max_Shoot_Distance = PlayerSingleton.instance.Max_Shoot_Distance;
        Mouse_Input_Manager_ = Mouse_Input_Manager.instance;

        Hit_Marker_Left.SetActive(false);
        Hit_Marker_Right.SetActive(false);


        Angle_Offset_Forward_Aim = 90f - (Mathf.Atan(Max_Shoot_Distance / Distance_Between_Cannon_And_Centre) * Mathf.Rad2Deg);

        Cannon_Angle_Offset_Forward_Aim();

    }
    private void FixedUpdate()
    {
        if (Mouse_Input_Manager_.Is_Free_Aim_On)
        {
            Ray_To_Mouse();
        }

        Crosshair_RayCaster();
    }
    private void LateUpdate()
    {
        Debug.DrawRay(Cannonn_Left_Tip.transform.position,Cannonn_Left_Tip.transform.forward * Max_Shoot_Distance,Color.yellow);
        Debug.DrawRay(Cannonn_Right_Tip.transform.position, Cannonn_Right_Tip.transform.forward * Max_Shoot_Distance, Color.yellow);
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
            RayCast_Hit_Crosshair(CrossHair_Right, Right_Crosshair_Image , rightHit.point);
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

    public void Crosshair_Hover()
    {
        if (Mouse_Input_Manager_.Is_Free_Aim_On && UIVisualEffectsManager.Is_Pointer_Hovering)
        {
            CrossHair_Left.gameObject.SetActive(false);
            CrossHair_Right.gameObject.SetActive(false);
        }
        else
        {
            CrossHair_Left.gameObject.SetActive(true);
            CrossHair_Right.gameObject.SetActive(true);
        }
    }

    public void Cannon_Angle_Offset_Forward_Aim()
    {
        // Set local rotation of left cannon tip with positive angle offset on Y axis
        Cannonn_Left_Tip.transform.localRotation = Quaternion.Euler(Center_Offset_Angle, Angle_Offset_Forward_Aim, 0);
        // Set local rotation of right cannon tip with negative angle offset on Y axis
        Cannonn_Right_Tip.transform.localRotation = Quaternion.Euler(Center_Offset_Angle, -Angle_Offset_Forward_Aim, 0);
    }

   

    private void Ray_To_Mouse()
    {
        Vector3 Target_Position;
        RaycastHit Hit;
        Ray Mouse_Ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(Mouse_Ray, out Hit))
        {
            Target_Position = Hit.point;
        }
        else
        {
            Target_Position = Mouse_Ray.origin + Mouse_Ray.direction * Max_Shoot_Distance;
        }
        Rotate_To_Mouse(Target_Position);
    }
    private void Rotate_To_Mouse(Vector3 Target)
    {
        Vector3 Direction_Cannonn_Left_Tip = Target - Cannonn_Left_Tip.transform.position + Vector3.left * 5;
        Vector3 Direction_Cannonn_Right_Tip = Target - Cannonn_Right_Tip.transform.position + Vector3.right * 5;

        Cannonn_Left_Tip.transform.forward = Direction_Cannonn_Left_Tip;
        Cannonn_Right_Tip.transform.forward = Direction_Cannonn_Right_Tip;

        Cannonn_Left_Tip.transform.rotation = Quaternion.LookRotation(Direction_Cannonn_Left_Tip);
        Cannonn_Right_Tip.transform.rotation = Quaternion.LookRotation(Direction_Cannonn_Right_Tip);
    }


    private void Hit_Marker_Function()
    {
        StartCoroutine(Hit_Maker_Coroutine());
    }

    private IEnumerator Hit_Maker_Coroutine()
    {
        Hit_Marker_Left.SetActive(false);
        Hit_Marker_Right.SetActive(false);

        Hit_Marker_Left.SetActive(true);
        Hit_Marker_Right.SetActive(true);

        yield return WaitForSeconds_0_2_5;

        Hit_Marker_Left.SetActive(false);
        Hit_Marker_Right.SetActive(false);
    }

}
