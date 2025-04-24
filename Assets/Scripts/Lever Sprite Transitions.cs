using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverSpriteTransitions : MonoBehaviour
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

    [SerializeField] private Image Throttle_Handle;
    [SerializeField] private Image Steering_Handle;
    [SerializeField] private RectTransform Throttle_Handle_Rect;
    [SerializeField] private RectTransform Steering_Handle_Rect;

    [SerializeField] private float Min_Scale;
    [SerializeField] private float Max_Scale;
 
    public void Throttle_Idle()
    {
        Throttle_Handle.sprite = Throttle_Idle_Icon;
        Throttle_Handle_Rect.localScale = Vector3.one * Min_Scale;
    }
    public void Throttle_Hover()
    {
        Throttle_Handle.sprite = Throttle_Hover_Icon;
        Throttle_Handle_Rect.localScale = Vector3.one * Max_Scale;

    }
    public void Throttle_Low()
    {
        Throttle_Handle.sprite = Throttle_Low_Icon;
    }
    public void Throttle_Moderate()
    {
        Throttle_Handle.sprite = Throttle_Moderate_Icon;
    }
    public void Throttle_High()
    {
        Throttle_Handle.sprite = Throttle_High_Icon;
    }
    public void Steering_Idle()
    {
        Steering_Handle.sprite = Steering_Idle_Icon;
        Steering_Handle_Rect.localScale = Vector3.one * Min_Scale;
    }
    public void Steering_Hover()
    {
        Steering_Handle.sprite = Steering_Hover_Icon;
        Steering_Handle_Rect.localScale = Vector3.one * Max_Scale;
    }
    public void Steering_Low()
    {
        Steering_Handle.sprite = Steering_Low_Icon;
    }
    public void Steering_Moderate()
    {
        Steering_Handle.sprite = Steering_Moderate_Icon;
    }
    public void Steering_High()
    {
        Steering_Handle.sprite = Steering_High_Icon;
    }
    private IEnumerator Sprite_Change_Delay( Image image, Sprite sprite_1,Sprite sprite_2)
    {
        image.sprite = sprite_1;
        yield return new WaitForSeconds(3f);
        image.sprite = sprite_2;
    }
}
