using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class StickingAnchorScript : MonoBehaviour
{

    public GameObject Ship_Anchor;

    public bool Is_Right_Anchor_Taken;

    private void Start()
    {
        Anchor_Selector();
    }

    private void Anchor_Selector()
    {
        if (!Is_Right_Anchor_Taken)
        {
            Ship_Anchor = PlayerSingleton.instance.Left_Ship_Anchor;
        }
        else
        {
            Ship_Anchor = PlayerSingleton.instance.Right_Ship_Anchor;
        }
    }
}
