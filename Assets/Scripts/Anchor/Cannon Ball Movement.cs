using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb_Cannon_Projectile;
    [SerializeField] private float Thrust_Force;
    [SerializeField] private float Torque_Force;


    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    void FixedUpdate()
    {
        Thrust();
    }

    private void Thrust()
    {
        Rb_Cannon_Projectile.AddForce(transform.forward * Thrust_Force, ForceMode.Force);
        Rb_Cannon_Projectile.AddTorque(transform.forward * Torque_Force, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
