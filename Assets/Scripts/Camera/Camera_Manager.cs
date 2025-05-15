using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Ideal_Virtual_Camera;
    [SerializeField] private CinemachineVirtualCamera Positive_Vertical_Virtual_Camera;
    [SerializeField] private CinemachineVirtualCamera Negative_Vertical_Virtual_Camera;

    void Start()
    {
        Ideal_Virtual_Camera.Priority = 1000;
    }

    public void Postive_Vertical_Camera_Offset()
    {
        Positive_Vertical_Virtual_Camera.Priority = 1000;
        Negative_Vertical_Virtual_Camera.Priority = 800;
        Ideal_Virtual_Camera.Priority = 800;
    }
    public void Negative_Vertical_Camera_Offset()
    {
        Negative_Vertical_Virtual_Camera.Priority = 1000;
        Positive_Vertical_Virtual_Camera.Priority = 800;
        Ideal_Virtual_Camera.Priority = 800;   
    }
    public void Ideal_Camera_Offset()
    {
        Ideal_Virtual_Camera.Priority = 1000;
        Positive_Vertical_Virtual_Camera.Priority = 800;
        Negative_Vertical_Virtual_Camera.Priority = 800;
    }
}
