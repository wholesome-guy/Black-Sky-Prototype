using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_Singleton : MonoBehaviour
{
    // Static instance to access the singleton from anywhere
    public static Anchor_Singleton instance;

    // Public references to the left and right ship anchors
    public GameObject Left_Ship_Anchor;
    public GameObject Right_Ship_Anchor;

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this duplicate
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Assign this as the instance
            instance = this;
        }
    }
}