using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PayloadManager : MonoBehaviour
{
     private SpaceShipValues SpaceShip_Values;
    [SerializeField] private CanvasGroup Payload_Canvas_Group;
    private float Payload_Ratio;
    [SerializeField] private TextMeshProUGUI Percentage_Text;

    public static Action Update_Payload_Percentage_Event;
    public static Action<bool> Payload_Fade_Event;

    private WaitForSeconds WaitForSeconds_0_5 = new WaitForSeconds(0.5f);

    private PlayerSingleton Player_Singleton;
    private void OnEnable()
    {
        Update_Payload_Percentage_Event += Percentage_Payload;
        Payload_Fade_Event += Fade_Percentage_Payload;
    }

    private void OnDisable()
    {
        Update_Payload_Percentage_Event -= Percentage_Payload;
        Payload_Fade_Event -= Fade_Percentage_Payload;
    }
    private void Start()
    {
        Player_Singleton = PlayerSingleton.instance;
        SpaceShip_Values = Player_Singleton.SpaceShip_Select_Values;
        Percentage_Payload();
        Payload_Canvas_Group.gameObject.SetActive(false);
        Payload_Canvas_Group.alpha = 0f;
    }
    private void Fade_Percentage_Payload(bool Set_Active)
    {
        if (Set_Active)
        {
            Payload_Canvas_Group.gameObject.SetActive(true);
            UIVisualEffectsManager.UI_Fader_Event(Payload_Canvas_Group, 0, 1, 0.25f);
        }
        else
        {
            UIVisualEffectsManager.UI_Fader_Event(Payload_Canvas_Group, 1, 0, 0.25f);
            StartCoroutine(Delay_Hide());
        }
    }
    private void Percentage_Payload()
    {
        Payload_Ratio = Player_Singleton.Asteroid_Mass / SpaceShip_Values.Payload;
        string Payload_Text = Mathf.Clamp(Mathf.RoundToInt(Payload_Ratio * 100),0,100) + "%";
        Percentage_Text.text = Payload_Text;
    }
    private IEnumerator Delay_Hide()
    {
        yield return WaitForSeconds_0_5;
        Payload_Canvas_Group.gameObject.SetActive(false);
    }
}
