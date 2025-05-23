using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTipManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Projectiles; // Array of different projectile prefabs
    [SerializeField] private float Reload_Duration = 5f; // Time between consecutive shots
    private int index; // Current projectile index

    private bool Is_Ammo_Loaded = true; // Flag to check if weapon can shoot

    // Switches between available ammo types (projectiles)
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

    // Instantiates a projectile if ammo is loaded, then starts the reload coroutine
    public void Shoot_Projectile()
    {
        if (Is_Ammo_Loaded)
        {
            Instantiate(Projectiles[index], gameObject.transform.position, transform.rotation);
            Is_Ammo_Loaded = false;
            StartCoroutine(Reload_Ammo());
        }
    }

    // Waits for the specified reload duration before allowing the next shot
    IEnumerator Reload_Ammo()
    {
        yield return new WaitForSeconds(Reload_Duration);
        Is_Ammo_Loaded = true;
    }
}
