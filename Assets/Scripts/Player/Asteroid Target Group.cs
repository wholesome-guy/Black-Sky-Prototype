using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AsteroidTargetGroup : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup Asteroid_Target_Group;
    private CinemachineComposer Asteroid_Camera_Composer;
    [SerializeField] private CinemachineVirtualCamera Asteroid_Camera;


    public static Action Asteroid_Camera_Event;
    public static Action No_Asteroid_Camera_Event;

    private void OnEnable()
    {
        Asteroid_Camera_Event += Target_Group;
        No_Asteroid_Camera_Event += No_Asteroids_Camera;
    }

    private void OnDisable()
    {
        Asteroid_Camera_Event -= Target_Group;
        No_Asteroid_Camera_Event -= No_Asteroids_Camera;
    }
    private void Start()
    {
        Asteroid_Camera_Composer = Asteroid_Camera.GetCinemachineComponent<CinemachineComposer>();
        No_Asteroids_Camera();
    }
    private void Target_Group()
    {
        Asteroid_Camera.LookAt = Asteroid_Target_Group.gameObject.transform;
        Asteroid_Camera_Composer.m_TrackedObjectOffset = new Vector3(0, -7.5f, 0);

        CinemachineTargetGroup.Target[] Targets = new CinemachineTargetGroup.Target[PlayerSingleton.instance.Asteroid_Transfroms.Length];

        for (int i = 0; i < PlayerSingleton.instance.Asteroid_Transfroms.Length; i++)
        {
            Targets[i] = new CinemachineTargetGroup.Target
            {
                target = PlayerSingleton.instance.Asteroid_Transfroms[i],
                weight = 1f, // Set your desired weight
                radius = 0f  // Set your desired radius
            };
        }
        Asteroid_Target_Group.m_Targets = Targets;
    }

    private void No_Asteroids_Camera()
    {
        Asteroid_Camera_Composer.m_TrackedObjectOffset = new Vector3 (0, 35f, 0);
        Asteroid_Camera.LookAt = PlayerSingleton.instance.Player_Transform;
    }
}
