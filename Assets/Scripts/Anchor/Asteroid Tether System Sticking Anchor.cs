using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidTetherSystemStickingAnchor : MonoBehaviour
{
    private HingeJoint Asteroid_HingeJoint;
    private AsteroidScript Asteroid_Script;
    private Rigidbody Player_Rigidbody;

    public static Action<float> Asteroid_Mass_Transfer;


    private void OnEnable()
    {
        AnchorPointCollision.Tether_Asteroid += Instantiate_Joint;
        DockingZoneCollisionManager.On_Player_Docked += Asteroid_Positioner_Bool;
        Keyboard_Input_Manager.De_Tether += De_Tether_Function;
    }

    private void OnDisable()
    {
        AnchorPointCollision.Tether_Asteroid -= Instantiate_Joint;
        DockingZoneCollisionManager.On_Player_Docked -= Asteroid_Positioner_Bool;
        Keyboard_Input_Manager.De_Tether -= De_Tether_Function;
    }
    private void Start()
    {
        Player_Rigidbody = PlayerSingleton.instance.Player_Rigidbody;
        Asteroid_Script = gameObject.transform.parent.GetComponent<AsteroidScript>();
    }
    private void Asteroid_Positioner_Bool()
    {
        Asteroid_Script.Is_Asteroid_At_Position = false;
    }

    private void Instantiate_Joint()
    {
        
        Asteroid_Script.Is_Asteroid_At_Position = true;

        Asteroid_HingeJoint = gameObject.transform.parent.AddComponent<HingeJoint>();

        Asteroid_HingeJoint.connectedBody = Player_Rigidbody;
        Asteroid_Tethered();
    }

    private void Asteroid_Tethered()
    {
        Asteroid_Script.Is_Asteroid_Tethered = true;
        Asteroid_Mass_Transfer.Invoke(Asteroid_Script.Asteroid_Mass);
    }

    private void De_Tether_Function()
    {
        Destroy(Asteroid_HingeJoint);
        Asteroid_Script.Is_Asteroid_Tethered = false;

        Asteroid_HingeJoint.connectedBody = null;

    }
}
