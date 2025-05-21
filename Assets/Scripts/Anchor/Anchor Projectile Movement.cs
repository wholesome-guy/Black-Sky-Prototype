using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorProjectileMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb_Anchor_Projectile;
    [SerializeField] private float Thrust_Force;
    [SerializeField] private float Torque_Force;
    [SerializeField] private GameObject Sticking_Anchor;

    public static Action<Vector3> Sticking_Anchor_Deployed;

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
        Rb_Anchor_Projectile.AddForce(transform.forward * Thrust_Force , ForceMode.Force);
        Rb_Anchor_Projectile.AddTorque(transform.forward * Torque_Force, ForceMode.Force);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            ContactPoint contactPoint = collision.contacts[0];

            Vector3 position = contactPoint.point + contactPoint.normal * -1f;

            Quaternion rotation = Quaternion.LookRotation(-contactPoint.normal);

            GameObject Anchor = Instantiate(Sticking_Anchor, position, rotation);

            Anchor.transform.SetParent(collision.transform);

            Sticking_Anchor_Deployed.Invoke(contactPoint.normal);

            Destroy(gameObject);

        }
    }






}
