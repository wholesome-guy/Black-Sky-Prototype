using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrottleSteeringIconSprite : MonoBehaviour
{
    [SerializeField] private Sprite Throttle_Idle_Icon;
    [SerializeField] private Sprite Throttle_Hover_Icon;
    [SerializeField] private Sprite Throttle_Low_Icon;
    [SerializeField] private Sprite Throttle_Moderate_Icon;
    [SerializeField] private Sprite Throttle_High_Icon;

    [SerializeField] private Sprite Steering_Idle_Icon;
    [SerializeField] private Sprite Steering_Hover_Icon;
    [SerializeField] private Sprite Steering_Low_Icon;
    [SerializeField] private Sprite Steering_Moderate_Icon;
    [SerializeField] private Sprite Steering_High_Icon;

    [SerializeField] private Image Throttle_Icon;
    [SerializeField] private Image Steering_Icon;
    [SerializeField] private RectTransform Throttle_Icon_Rect;
    [SerializeField] private RectTransform Steering_Icon_Rect;

    [SerializeField] private float Min_Scale;
    [SerializeField] private float Max_Scale;

    public void Throttle_Idle()
    {
        Throttle_Icon.sprite = Throttle_Idle_Icon;
        Throttle_Icon_Rect.localScale = Vector3.one * Min_Scale;
    }
    public void Throttle_Hover()
    {
        Throttle_Icon.sprite = Throttle_Hover_Icon;
        Throttle_Icon_Rect.localScale = Vector3.one * Max_Scale;
    }
    public void Throttle_Low()
    {
        Throttle_Icon.sprite = Throttle_Low_Icon;
    }
    public void Throttle_Moderate()
    {
        Throttle_Icon.sprite = Throttle_Moderate_Icon;
    }
    public void Throttle_High()
    {
        Throttle_Icon.sprite = Throttle_High_Icon;
    }
    public void Steering_Idle()
    {
        Steering_Icon.sprite = Steering_Idle_Icon;
        Steering_Icon_Rect.localScale = Vector3.one * Min_Scale;
    }
    public void Steering_Hover()
    {
        Steering_Icon.sprite = Steering_Hover_Icon;
        Steering_Icon_Rect.localScale = Vector3.one * Max_Scale;
    }
    public void Steering_Low()
    {
        Steering_Icon.sprite = Steering_Low_Icon;
    }
    public void Steering_Moderate()
    {
        Steering_Icon.sprite = Steering_Moderate_Icon;
    }
    public void Steering_High()
    {
        Steering_Icon.sprite = Steering_High_Icon;
    }
    
}
