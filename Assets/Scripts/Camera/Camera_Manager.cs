using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Ideal_Virtual_Camera;             // Reference to the ideal camera
    [SerializeField] private CinemachineVirtualCamera Asteroid_Vitural_Camera;
    private CinemachineBasicMultiChannelPerlin Ideal_Noise_Component;

    public static Action Camera_Shake_Event;

    private WaitForSeconds WaitForSeconds_0_2 = new WaitForSeconds(0.2f);

    private void OnEnable()
    {
       Camera_Shake_Event += Camera_Shake;
    }
    private void OnDisable()
    {
        Camera_Shake_Event -= Camera_Shake;
    }
    void Start()
    {
        Ideal_Virtual_Camera.Priority = 1000;
        Ideal_Noise_Component = Ideal_Virtual_Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Ideal_Noise_Component.m_AmplitudeGain = 0f;

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

    private void Camera_Shake()
    {
        StartCoroutine(Camera_Shake_Coroutine());
    }

    private IEnumerator Camera_Shake_Coroutine()
    {
        Ideal_Noise_Component.m_AmplitudeGain = 0f;
        Ideal_Noise_Component.m_AmplitudeGain = 10f;

        yield return WaitForSeconds_0_2;

        Ideal_Noise_Component.m_AmplitudeGain = 0f;
    }
}
