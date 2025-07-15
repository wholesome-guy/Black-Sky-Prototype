using System.Collections;
using System.Collections.Generic;
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
    //Same as Anchor Point
    public Transform Asteroid_Point;

    public GameObject Left_Cannon_Tip;
    public GameObject Right_Cannon_Tip;

    public float Max_Shoot_Distance = 500f;
    

    private Dictionary<AsteroidScript, float> Active_Asteroids_Tethered = new Dictionary<AsteroidScript, float>();
    [SerializeField] private float Asteroid_Mass;
    [SerializeField] private float Dampening_Constant = 50;
    public float Dampening_Factor = 0;

    //Used by Movement Script to kno whether to apply Damping or no, False = No damping True = Damping
    public bool Is_Anchored = false;

    public bool Is_Spaceship_At_Rest = false;


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
    

    private void OnEnable()
    {
        DockingZoneCollisionManager.On_Player_Docked += Asteroid_Point_Activate;
        AnchorPointCollision.Asteroid_Collided_Anchor_Point += Asteroid_Point_Deactivate;
        AsteroidTetherSystemStickingAnchor.Asteroid_Mass_Transfer += Mass_Dampner_Calcultor;

        Keyboard_Input_Manager.De_Tether += Dampner_Reset;

        AsteroidTow.Tow_Joint_Broke += Dampner_Reset;
        AsteroidTow.Tow_Joint_Broke += Clear_Payload;

        DeselectProjectile.Deselect_Asteroid += Asteroid_Deselect_Mass_Calulator;
    }

    private void OnDisable()
    {
        DockingZoneCollisionManager.On_Player_Docked -= Asteroid_Point_Activate;
        AnchorPointCollision.Asteroid_Collided_Anchor_Point -= Asteroid_Point_Deactivate;
        AsteroidTetherSystemStickingAnchor.Asteroid_Mass_Transfer -= Mass_Dampner_Calcultor;

        Keyboard_Input_Manager.De_Tether -= Dampner_Reset;

        AsteroidTow.Tow_Joint_Broke -= Dampner_Reset;
        AsteroidTow.Tow_Joint_Broke -= Clear_Payload;

        DeselectProjectile.Deselect_Asteroid -= Asteroid_Deselect_Mass_Calulator;
    }
    private void Start()
    {
        Asteroid_Point_Deactivate();
    }


    private void Mass_Dampner_Calcultor(AsteroidScript Asteroid_Script, bool Is_Tethered)
    {
        float Total_Mass = 0;

        if (Is_Tethered)
        {
            Active_Asteroids_Tethered[Asteroid_Script] = Asteroid_Script.Asteroid_Mass;
            foreach(var Asteroid in Active_Asteroids_Tethered)
            {

                var Asteroid_local = Asteroid.Key; 
                Total_Mass += Asteroid.Value;               
            }
        }
        else
        {
            Clear_Payload();
        }
        Asteroid_Mass = Total_Mass;
        Is_Anchored = true;

        Damping_Factor_Calculator();

    }

    private void Asteroid_Deselect_Mass_Calulator(AsteroidScript Asteroid_Script)
    {
        if(Active_Asteroids_Tethered.ContainsKey(Asteroid_Script))
        {
            Asteroid_Mass -= Active_Asteroids_Tethered[Asteroid_Script];           
            Active_Asteroids_Tethered.Remove(Asteroid_Script);
            
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
        Is_Anchored = false;
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
