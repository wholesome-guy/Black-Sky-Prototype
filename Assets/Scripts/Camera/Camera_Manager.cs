using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Ideal_Virtual_Camera;             // Reference to the ideal camera
    [SerializeField] private CinemachineVirtualCamera Positive_Vertical_Virtual_Camera;  // Reference to the positive vertical offset camera
    [SerializeField] private CinemachineVirtualCamera Negative_Vertical_Virtual_Camera;  // Reference to the negative vertical offset camera

    // Initialize by setting the ideal camera as the highest priority on start
    void Start()
    {
        Ideal_Virtual_Camera.Priority = 1000;
    }

    // Set the positive vertical offset camera as active by giving it highest priority
    public void Postive_Vertical_Camera_Offset()
    {
        Positive_Vertical_Virtual_Camera.Priority = 1000;
        Negative_Vertical_Virtual_Camera.Priority = 800;
        Ideal_Virtual_Camera.Priority = 800;
    }

    // Set the negative vertical offset camera as active by giving it highest priority
    public void Negative_Vertical_Camera_Offset()
    {
        Negative_Vertical_Virtual_Camera.Priority = 1000;
        Positive_Vertical_Virtual_Camera.Priority = 800;
        Ideal_Virtual_Camera.Priority = 800;
    }

    // Set the ideal camera as active by giving it highest priority
    public void Ideal_Camera_Offset()
    {
        Ideal_Virtual_Camera.Priority = 1000;
        Positive_Vertical_Virtual_Camera.Priority = 800;
        Negative_Vertical_Virtual_Camera.Priority = 800;
    }
}
