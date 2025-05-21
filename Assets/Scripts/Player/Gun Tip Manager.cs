using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTipManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Projectiles;
    private int index;

    public void Ammo_Switch()
    {
        if (index == 0)
        {
            index = 1;
        }
        else
        {
            index = 0;
        }
    }

    public void Shoot_Projectile()
    {
        Instantiate(Projectiles[index],gameObject.transform.position,transform.rotation);
    }
}
