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

        
    }

    private void Asteroid_Movement()
    {
        Vector3 Random_Vector = new Vector3( Random.Range(-1 ,+1) , Random.Range(-1 , +1), Random.Range(-1 , +1));

        Asteroid_RigidBody.AddForce(Random_Vector * Asteroid_Speed,ForceMode.Force);
        Asteroid_RigidBody.AddTorque(Random_Vector * Asteroid_Rotation_Speed, ForceMode.Force);
    }

   
     

}
