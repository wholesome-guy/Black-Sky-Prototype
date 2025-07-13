using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class AnchorPointCollision : MonoBehaviour
{
    public static Action Asteroid_Collided_Anchor_Point;

    private bool Is_Asteroid_Collieded;

    private void OnEnable()
    {
        Is_Asteroid_Collieded = false;
    }

    [SerializeField] private string Asteroid_Tag = "Asteroid";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Asteroid_Tag))
        {
            if(!Is_Asteroid_Collieded)
            {
                Asteroid_Collided_Anchor_Point.Invoke();
                Is_Asteroid_Collieded = true;

            }
        }
    }
}
