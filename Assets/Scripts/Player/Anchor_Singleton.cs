using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_Singleton : MonoBehaviour
{
   
    public static Anchor_Singleton instance;


    [SerializeField] private GameObject Left_Ship_Anchor;
    [SerializeField] private GameObject Right_Ship_Anchor;
    public Vector3 Right_Ship_Anchor_Position;
    public Vector3 Left_Ship_Anchor_Position;

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

    private void Update()
    {
        Right_Ship_Anchor_Position = Right_Ship_Anchor.transform.position;
        Left_Ship_Anchor_Position = Left_Ship_Anchor.transform.position;
    }


}
