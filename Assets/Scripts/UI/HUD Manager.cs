using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    // Reference to the HUD GameObject in the scene
    [SerializeField] private GameObject HUD;
    [SerializeField] private CanvasGroup HUD_Canvas_Group;

    // Called when the HUD toggle event is invoked

    private void Start()
    {
        HUD_Canvas_Group.alpha = 0f;
        UIVisualEffectsManager.UI_Fader_Event.Invoke(HUD_Canvas_Group, 0, 1, 0.5f);
    }
    public void On_Change_HUD()
    {
        // Sets HUD visibility based on the Keyboard Input Manager's toggle state


        if (Keyboard_Input_Manager.instance.Is_HUD_On)
        {
            HUD.SetActive(true);
            UIVisualEffectsManager.UI_Fader_Event.Invoke(HUD_Canvas_Group, 0, 1, 0.5f);
        }
        else
        {
            UIVisualEffectsManager.UI_Fader_Event.Invoke(HUD_Canvas_Group, 1, 0, 0.5f);
            HUD.SetActive(false);
        }
    }
}