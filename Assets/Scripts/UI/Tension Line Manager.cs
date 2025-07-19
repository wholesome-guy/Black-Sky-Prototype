using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TensionLineManager : MonoBehaviour
{
    [SerializeField] private GameObject Tension_Line;
    [SerializeField] private CanvasGroup Tension_Line_Canvas_Group;
    [SerializeField] private Image Skull;
    [SerializeField] private Image Danger_Line;
    [SerializeField] private Image Anchor;

    [SerializeField] private Color Normal_Colour;
    [SerializeField] private Color Danger_Colour;

    [SerializeField] private TextMeshProUGUI Tension_Text;

    private bool Is_Tension_Line_Active =false;

    private void OnEnable()
    {
        DockingZoneCollisionManager.On_Player_Docked += Tension_Line_Active;
        PlayerSingleton.No_Asteroids_Attached += Tension_Line_Inactive;
    }

    private void OnDisable()
    {
        DockingZoneCollisionManager.On_Player_Docked -= Tension_Line_Active;
        PlayerSingleton.No_Asteroids_Attached -= Tension_Line_Inactive;
    }

    private void Start()
    {
        Tension_Line_Inactive();
        Tension_Line_Canvas_Group.alpha = 0f;
    }
    private void Tension_Line_Active()
    {
        Tension_Line.SetActive(true);
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Tension_Line_Canvas_Group, 0f, 1f, 0.25f);
        Is_Tension_Line_Active = true;
    }
    private void Tension_Line_Inactive()
    {
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Tension_Line_Canvas_Group, 1f, 0f, 0.25f);
        StartCoroutine(Delay_Hide());
       
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
        string Tension_Meter = Mathf.RoundToInt(PlayerSingleton.instance.Player_Rigidbody.velocity.magnitude * AsteroidData.Fake_Tension_Velocity_Constant) + "MN";
        Tension_Text.text = Tension_Meter;
        if (Anchor.rectTransform.localPosition.y > 242f)
        {
            Danger_Line.color = Danger_Colour;
            Skull.gameObject.SetActive(true);
            Skull.color = Danger_Colour;
        }
        else
        {
            Danger_Line.color = Normal_Colour;
            Skull.color = Normal_Colour;
            Skull.gameObject.SetActive(false);
        }
    }

    private IEnumerator Delay_Hide()
    {
        yield return new WaitForSeconds(0.5f);
        Tension_Line.SetActive(false);
        Is_Tension_Line_Active = false;
    }


}
