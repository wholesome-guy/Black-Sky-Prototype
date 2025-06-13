using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnchorPointCollision : MonoBehaviour
{
    public static UnityEvent Tether_Asteroid;

    [SerializeField] private string Asteroid_Tag = "Asteroid";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Asteroid_Tag))
        {
            Tether_Asteroid.Invoke();
        }
    }
}
