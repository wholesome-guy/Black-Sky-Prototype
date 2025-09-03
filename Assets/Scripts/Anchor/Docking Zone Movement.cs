using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingZoneMovement : MonoBehaviour
{

    [SerializeField] private float Rotate_Speed;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Rotate_Speed);
    }
}
