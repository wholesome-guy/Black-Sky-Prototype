using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Ideal_Virtual_Camera;             // Reference to the ideal camera
    [SerializeField] private CinemachineVirtualCamera Asteroid_Vitural_Camera;

    void Start()
    {
        Ideal_Virtual_Camera.Priority = 1000;
    }

    public void Ideal_Offset()
    {
        Asteroid_Vitural_Camera.Priority = 800;
        Ideal_Virtual_Camera.Priority = 1000;
    }

    public void Back_Camera_Offset()
    {
        Asteroid_Vitural_Camera.Priority = 1000;
        Ideal_Virtual_Camera.Priority = 800;
    }
}
