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
    public Transform Asteroid_Point;
    public GameObject Left_Cannon_Tip;
    public GameObject Right_Cannon_Tip;

    public float Max_Shoot_Distance = 500f;

    private float Asteroid_Mass;
    [SerializeField] private float Dampening_Constant = 50;
    public float Dampening_Factor = 0;

    public bool Is_Anchored = false;

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
        AsteroidTetherSystemStickingAnchor.Asteroid_Mass_Transfer += Mass_Dampner_Calcultor;
        DockingZoneCollisionManager.On_Player_Docked += Asteroid_Point_Activate;
        AnchorPointCollision.Tether_Asteroid += Asteroid_Point_Deactivate;
        Keyboard_Input_Manager.De_Tether += Dampner_Reset;
    }

    private void OnDisable()
    {
        AsteroidTetherSystemStickingAnchor.Asteroid_Mass_Transfer -= Mass_Dampner_Calcultor;
        DockingZoneCollisionManager.On_Player_Docked -= Asteroid_Point_Activate;
        AnchorPointCollision.Tether_Asteroid -= Asteroid_Point_Deactivate;
        Keyboard_Input_Manager.De_Tether -= Dampner_Reset;


    }

    private void Mass_Dampner_Calcultor(float mass)
    {
        Asteroid_Mass = mass;

        Dampening_Factor = Asteroid_Mass / (Asteroid_Mass + Dampening_Constant);
        Is_Anchored = true;

    }
    private void Dampner_Reset()
    {
        Dampening_Factor = 0;
        Is_Anchored = false;
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
