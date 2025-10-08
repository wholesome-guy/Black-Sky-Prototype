using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    // Static instance to access the singleton from anywhere
    public static PlayerSingleton instance;

    // Public references to the left and right ship anchors
    public GameObject Left_Ship_Anchor;
    public GameObject Right_Ship_Anchor;

    public Rigidbody Player_Rigidbody;
    public Transform Player_Transform;
    public SpaceShip_Movement_Controller SpaceShip_Movement_Controller;

    //Same as Anchor Point
    public Transform Asteroid_Point;

    public GameObject Left_Cannon_Tip;
    public GameObject Right_Cannon_Tip;

    public float Max_Shoot_Distance = 500f;
    

    private Dictionary<AsteroidScript, float> Active_Asteroids_Tethered = new Dictionary<AsteroidScript, float>();
    public Transform[] Asteroid_Transfroms;
    public float Asteroid_Mass;
    [SerializeField] private float Dampening_Constant = 1.3e+07f;
    public float Dampening_Factor = 0;

    //Used by Movement Script to kno whether to apply Damping or no, False = No damping True = Damping
    public bool Is_Anchored = false;

    public static Action No_Asteroids_Attached;

    public bool Is_Spaceship_At_Rest = false;
    public bool Is_Spaceship_Able_To_Shoot = true;
    public bool Is_Spaceship_At_Selling_Zone = false;
    private bool Is_Payload_Hide = true;

    public int Space_Ship_Index = 0;
    public SpaceShipValues SpaceShip_Select_Values;
    private GameObject[] SpaceShip_Select_Objects;

    [SerializeField] private GameObject[] SpaceShip_0_Objects;
    [SerializeField] private GameObject[] SpaceShip_1_Objects;
    [SerializeField] private GameObject[] SpaceShip_2_Objects;

    [SerializeField] private SpaceShipValues SpaceShip_0;
    [SerializeField] private SpaceShipValues SpaceShip_1;
    [SerializeField] private SpaceShipValues SpaceShip_2;


    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this duplicate
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Assign this as the instance
            instance = this;
        }
        
    }

    public void SpaceShip_Select_Function()
    {
        switch (Space_Ship_Index)
        {
            case 0:
                SpaceShip_Select_Values = SpaceShip_0;
                SpaceShip_Select_Objects = SpaceShip_0_Objects;
                break;

            case 1:
                SpaceShip_Select_Values = SpaceShip_1;
                SpaceShip_Select_Objects = SpaceShip_1_Objects;

                break;

            case 2:
                SpaceShip_Select_Values = SpaceShip_2;
                SpaceShip_Select_Objects = SpaceShip_2_Objects;

                break;
        }

        Left_Cannon_Tip = SpaceShip_Select_Objects[0];
        Right_Cannon_Tip = SpaceShip_Select_Objects[1];
        Left_Ship_Anchor = SpaceShip_Select_Objects[2];
        Right_Ship_Anchor = SpaceShip_Select_Objects[3];
    }
    

    private void OnEnable()
    {
        DockingZoneCollisionManager.On_Player_Docked += Asteroid_Point_Activate;
        AnchorPointCollision.Asteroid_Collided_Anchor_Point += Asteroid_Point_Deactivate;
        AsteroidTetherSystemStickingAnchor.Asteroid_Mass_Transfer += Mass_Dampner_Calcultor;

        Keyboard_Input_Manager.De_Tether += Dampner_Reset;

        AsteroidTow.Tow_Joint_Broke += Asteroid_Deselect_Mass_Calulator;

        DeselectProjectile.Deselect_Asteroid += Asteroid_Deselect_Mass_Calulator;
    }

    private void OnDisable()
    {
        DockingZoneCollisionManager.On_Player_Docked -= Asteroid_Point_Activate;
        AnchorPointCollision.Asteroid_Collided_Anchor_Point -= Asteroid_Point_Deactivate;
        AsteroidTetherSystemStickingAnchor.Asteroid_Mass_Transfer -= Mass_Dampner_Calcultor;

        Keyboard_Input_Manager.De_Tether -= Dampner_Reset;

        AsteroidTow.Tow_Joint_Broke -= Asteroid_Deselect_Mass_Calulator;

        DeselectProjectile.Deselect_Asteroid -= Asteroid_Deselect_Mass_Calulator;
    }
    private void Start()
    {
        Asteroid_Point_Deactivate();
    }


    private void Mass_Dampner_Calcultor(AsteroidScript Asteroid_Script, bool Is_Tethered)
    {
        float Total_Mass = 0;
        int Index = 0;

        if (Is_Tethered)
        {
            Active_Asteroids_Tethered[Asteroid_Script] = Asteroid_Script.Asteroid_Mass;
            Asteroid_Transfroms = new Transform[Active_Asteroids_Tethered.Count];
            foreach(var Asteroid in Active_Asteroids_Tethered)
            {
                var Asteroid_local = Asteroid.Key;
                Asteroid_Transfroms[Index] = Asteroid_local.gameObject.transform;
                Index++;
                Total_Mass += Asteroid.Value;               
            }
            AsteroidTargetGroup.Asteroid_Camera_Event.Invoke();
            Asteroid_Mass = Total_Mass;
            Is_Anchored = true;
            PayloadManager.Update_Payload_Percentage_Event.Invoke();
            if (Is_Payload_Hide)
            {
                PayloadManager.Payload_Fade_Event.Invoke(true);
                Is_Spaceship_At_Rest = false;
                Is_Payload_Hide = false;

            }
            Damping_Factor_Calculator();
        }
        else
        {
            Clear_Payload();
        }
        

    }
    

    private void Asteroid_Deselect_Mass_Calulator(AsteroidScript Asteroid_Script)
    {
        if(Active_Asteroids_Tethered.ContainsKey(Asteroid_Script))
        {
            Asteroid_Mass -= Active_Asteroids_Tethered[Asteroid_Script];
            Active_Asteroids_Tethered.Remove(Asteroid_Script);

            var Asteroid_Transfroms_List = Asteroid_Transfroms.ToList();
            Asteroid_Transfroms_List.Remove(Asteroid_Script.gameObject.transform);
            Asteroid_Transfroms = Asteroid_Transfroms_List.ToArray();

            AsteroidTargetGroup.Asteroid_Camera_Event.Invoke();
            PayloadManager.Update_Payload_Percentage_Event.Invoke();
            Asteroid_Script.Is_Asteroid_Tethered = false;

            if (Asteroid_Mass <= 0)
            {
                Dampner_Reset();
            }

            Damping_Factor_Calculator();

        }

    }
    private void Damping_Factor_Calculator()
    {
        Dampening_Factor = Asteroid_Mass / (Asteroid_Mass + Dampening_Constant);

    }
    private void Dampner_Reset()
    {
        Asteroid_Mass = 0;
        Dampening_Factor = 0;

        var Asteroid_Transfroms_List = Asteroid_Transfroms.ToList();
        Asteroid_Transfroms_List.Clear();
        Asteroid_Transfroms = Asteroid_Transfroms_List.ToArray();

        AsteroidTargetGroup.No_Asteroid_Camera_Event.Invoke();
        PayloadManager.Payload_Fade_Event.Invoke(false);
        Is_Payload_Hide = true;
        Is_Anchored = false;
        No_Asteroids_Attached.Invoke();
        Clear_Payload();

        
    }
   
    private void Clear_Payload()
    {
        Active_Asteroids_Tethered.Clear();
    }

    private void Asteroid_Point_Activate()
    {
        Asteroid_Point.gameObject.SetActive(true);
    }
    private void Asteroid_Point_Deactivate()
    {
        Asteroid_Point.gameObject.SetActive(false);
    }


}
