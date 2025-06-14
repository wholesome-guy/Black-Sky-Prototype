using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private Rigidbody Asteroid_RigidBody;
    public float Asteroid_Mass;

    [SerializeField] private float Asteroid_Speed = 100;
    [SerializeField] private float Asteroid_Rotation_Speed = 100;
    [SerializeField] private float Pull_Force = 10;

    public bool Is_Asteroid_At_Position = true;
    public bool Is_Asteroid_Tethered = false;

    void Start()
    {
        Asteroid_Mass = Asteroid_RigidBody.mass;
    }
    private void FixedUpdate()
    {
        if (!Is_Asteroid_Tethered)
        {
            Asteroid_Movement();
        }
        if(!Is_Asteroid_At_Position)
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

}
