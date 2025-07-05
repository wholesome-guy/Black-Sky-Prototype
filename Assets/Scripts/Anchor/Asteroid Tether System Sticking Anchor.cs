using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidTetherSystemStickingAnchor : MonoBehaviour
{
    private AsteroidTow Asteroid_Tow;
    private AsteroidScript Asteroid_Script;
    private Rigidbody Player_Rigidbody;

    public static Action<AsteroidScript, bool> Asteroid_Mass_Transfer;

    private void OnEnable()
    {
        DockingZoneCollisionManager.On_Player_Docked += Asteroid_Positioner_Bool;
        AnchorPointCollision.Asteroid_Collided_Anchor_Point += Instantiate_Joint;
        Keyboard_Input_Manager.De_Tether += De_Tether_Function;
    }

    private void OnDisable()
    {
        DockingZoneCollisionManager.On_Player_Docked -= Asteroid_Positioner_Bool; 
        AnchorPointCollision.Asteroid_Collided_Anchor_Point -= Instantiate_Joint;
        Keyboard_Input_Manager.De_Tether -= De_Tether_Function;
    }
    private void Start()
    {
        Player_Rigidbody = PlayerSingleton.instance.Player_Rigidbody;
        Asteroid_Script = gameObject.transform.parent.GetComponent<AsteroidScript>();
        Asteroid_Script.Invoke("Find_Anchor", 2f);
    }

    
    private void Asteroid_Positioner_Bool()
    {
        //Bool to make Asteroid hurl towards anchor point(Behind Spaceship), False = Hurl
        Asteroid_Script.Is_Asteroid_At_Anchor_Position = false;
    }

    private void Instantiate_Joint()
    {
        //Bool to make Asteroid hurl towards anchor point(Behind Spaceship), True = AtPosition/Dont move
        Asteroid_Script.Is_Asteroid_At_Anchor_Position = true;

        Asteroid_Tow = gameObject.transform.parent.AddComponent<AsteroidTow>();
        Configure_HingeJoint();
        Asteroid_Tethered();
    }

    private void Asteroid_Tethered()
    { 
        Asteroid_Script.Is_Asteroid_Tethered = true;
        Asteroid_Mass_Transfer.Invoke(Asteroid_Script,true);
    }

    private void Configure_HingeJoint()
    {
        Asteroid_Tow.Asteriod_Mass = Asteroid_Script.Asteroid_Mass;
    }
    private void De_Tether_Function()
    {
        Destroy(Asteroid_Tow);
        Asteroid_Mass_Transfer.Invoke(Asteroid_Script, false);
        Asteroid_Script.Is_Asteroid_Tethered = false;
    }
    
}
