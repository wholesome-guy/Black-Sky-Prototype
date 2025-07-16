using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionLineManager : MonoBehaviour
{
    [SerializeField] private GameObject Tension_Line;
    [SerializeField] private Image Skull;
    [SerializeField] private Image Danger_Line;
    [SerializeField] private Image Anchor;

    [SerializeField] private Color Normal_Colour;
    [SerializeField] private Color Danger_Colour;

    private bool Is_Tension_Line_Active =false;

    private void OnEnable()
    {
        AnchorPointCollision.Asteroid_Collided_Anchor_Point += Tension_Line_Active;
        PlayerSingleton.No_Asteroids_Attached += Tension_Line_Inactive;
    }

    private void OnDisable()
    {
        AnchorPointCollision.Asteroid_Collided_Anchor_Point -= Tension_Line_Active;
        PlayerSingleton.No_Asteroids_Attached -= Tension_Line_Inactive;
    }

    private void Start()
    {
        Tension_Line_Inactive();
    }
    private void Tension_Line_Active()
    {
        Tension_Line.SetActive(true);
        Sequence Pop = DOTween.Sequence();
        Pop.Append(Tension_Line.transform.DOScale(1.25f,0.5f).SetEase(Ease.OutBounce));
        Pop.Append(Tension_Line.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBounce));
        Is_Tension_Line_Active = true;
    }
    private void Tension_Line_Inactive()
    {
        
        Tension_Line.transform.DOScale(0.75f, 0.5f).SetEase(Ease.OutBounce);
        Tension_Line.SetActive(false);
        Is_Tension_Line_Active = false;
    }

    private void Update()
    {
        if(Is_Tension_Line_Active)
        {
            Anchor_Movement();
        }
    }


    private void Anchor_Movement()
    {
        // 400/ 245 = 1.632 , Height/ Max speed ratio
        Anchor.rectTransform.localPosition = new Vector3(0,PlayerSingleton.instance.Player_Rigidbody.velocity.magnitude * 1.632f , 0 );


        if (Anchor.rectTransform.localPosition.y > 242f)
        {
            Danger_Line.color = Danger_Colour;
            Skull.color = Danger_Colour;
        }
        else
        {
            Danger_Line.color = Normal_Colour;
            Skull.color = Normal_Colour;
        }
    }

}
