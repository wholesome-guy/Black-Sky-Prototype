using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{

    [SerializeField] private GameObject HUD;




    public void On_Change_HUD()
    {
        HUD.SetActive(Keyboard_Input_Manager.instance.Is_HUD_On);
    }

    
}
