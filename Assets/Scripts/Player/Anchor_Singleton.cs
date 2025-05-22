using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_Singleton : MonoBehaviour
{
   
    public static Anchor_Singleton instance;


    public GameObject Left_Ship_Anchor;
    public GameObject Right_Ship_Anchor;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

}
