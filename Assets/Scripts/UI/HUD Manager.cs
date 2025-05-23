using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    // Reference to the HUD GameObject in the scene
    [SerializeField] private GameObject HUD;

    // Called when the HUD toggle event is invoked
    public void On_Change_HUD()
    {
        // Sets HUD visibility based on the Keyboard Input Manager's toggle state
        HUD.SetActive(Keyboard_Input_Manager.instance.Is_HUD_On);
    }
}