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

    private bool Is_Asteroid_Behind_SpaceShip = true;
    public bool Is_Asteroid_Tethered = false;
    private void OnEnable()
    {
        AnchorPointCollision.Tether_Asteroid += Asteroid_Behind_Spaceship_Bool_Switch;
    }
    private void OnDisable()
    {
        AnchorPointCollision.Tether_Asteroid -= Asteroid_Behind_Spaceship_Bool_Switch;
    }

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

        if(!Is_Asteroid_Behind_SpaceShip)
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
        Vector3 Asteroid_Point_Direction = PlayerSingleton.instance.Asteroid_Point.position - gameObject.transform.position;

        Asteroid_RigidBody.AddForce(Asteroid_Point_Direction * Pull_Force,ForceMode.Force);
   }

    private void Asteroid_Behind_Spaceship_Bool_Switch()
    {
        Asteroid_RigidBody.velocity = Vector3.zero;
        Is_Asteroid_Behind_SpaceShip = !Is_Asteroid_Behind_SpaceShip;
    }

}
