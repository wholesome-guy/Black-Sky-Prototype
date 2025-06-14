using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class AnchorPointCollision : MonoBehaviour
{
    public static Action Tether_Asteroid;

    [SerializeField] private string Asteroid_Tag = "Asteroid";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Asteroid_Tag))
        {
            Debug.Log("Hit");
            Tether_Asteroid.Invoke();
        }
    }
}
