using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private Rigidbody Asteroid_RigidBody;
    public float Asteroid_Mass;

    [SerializeField] private float Asteroid_Speed = 100;
    [SerializeField] private float Asteroid_Rotation_Speed = 100;
    [SerializeField] private float Pull_Force = 10;

    public bool Is_Asteroid_At_Anchor_Position = true;
    public bool Is_Asteroid_Tethered = false;
    public bool Is_Asteroid_Anchored = false;

    [SerializeField] private GameObject Sticking_Anchor_Right;
    [SerializeField] private GameObject Sticking_Anchor_Left;

    private string Right_Sticking_Anchor = "Sticking Anchor Prefab Right (Master)(Clone)";
    private string Left_Sticking_Anchor = "Sticking Anchor Prefab Left(Clone)";

    
    void Start()
    {
        Asteroid_RigidBody.mass = Asteroid_Mass;
    }
    private void FixedUpdate()
    {
        if (!Is_Asteroid_Tethered)
        {
            Asteroid_Movement();
        }
        if(!Is_Asteroid_At_Anchor_Position)
        {
            Asteroid_Positioner();
        }
    }

    

    private void Asteroid_Movement()
    {
        Vector3 Random_Vector = new Vector3( Random.Range(-1 ,+1) , Random.Range(-1 , +1), Random.Range(-1 , +1));

        Asteroid_RigidBody.AddForce(Random_Vector * Asteroid_Speed,ForceMode.Force);
        Asteroid_RigidBody.AddTorque(Random_Vector * Asteroid_Rotation_Speed, ForceMode.Force);
    }

    private void Asteroid_Positioner()
    {
        Vector3 Direction_Of_Position = PlayerSingleton.instance.Asteroid_Point.position - gameObject.transform.position;

        Asteroid_RigidBody.AddForce(Direction_Of_Position * Pull_Force,ForceMode.Force);
    }

    public void Find_Anchor()
    {
        Sticking_Anchor_Right = transform.Find(Right_Sticking_Anchor).gameObject;
        Sticking_Anchor_Left = transform.Find(Left_Sticking_Anchor).gameObject;

        if(Sticking_Anchor_Right != null && Sticking_Anchor_Left != null)
        {
            Is_Asteroid_Anchored = true;
        }
        else
        {
            Is_Asteroid_Anchored = false;
        }
    }

    public void Destroy_Anchors()
    {
        Destroy(Sticking_Anchor_Right.gameObject);
        Destroy(Sticking_Anchor_Left.gameObject);
    }



}
