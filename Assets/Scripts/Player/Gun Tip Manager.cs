using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GunTipManager : MonoBehaviour
{
    [SerializeField] private Projectiles Projectiles_Scriptable_Object;
    [SerializeField] private VisualEffect Muzzle_Flash;
    [SerializeField] private GameObject[] Projectiles; // Array of different projectile prefabs
    [SerializeField] private float Reload_Duration = 5f; // Time between consecutive shots
    private int Index_Projectile; // Current projectile index

    private bool Is_Ammo_Loaded = true; // Flag to check if weapon can shoot
    private void OnEnable()
    {
        ProjectileWheelManager.Projectile_Select_Event += Projectile_Select;
    }
    private void OnDisable()
    {
        ProjectileWheelManager.Projectile_Select_Event -= Projectile_Select;
    }
    // Switches between available ammo types (projectiles)
    private void Start()
    {
        Projectiles = Projectiles_Scriptable_Object.Projectile_Gameobjects;
        Muzzle_Flash.Stop();
        Projectile_Select(0);
    }
    public void Projectile_Select(int Index)
    {
        Index_Projectile = Index;
    }

    // Instantiates a projectile if ammo is loaded, then starts the reload coroutine
    public void Shoot_Projectile()
    {
        if (Is_Ammo_Loaded)
        {

            Instantiate(Projectiles[Index_Projectile], gameObject.transform.position, transform.rotation);
            Muzzle_Flash.Play();
            Is_Ammo_Loaded = false;
            TimerManager.Cannon_Reload_Event.Invoke(Reload_Duration);
            StartCoroutine(Reload_Ammo());
        }
    }

    // Waits for the specified reload duration before allowing the next shot
    IEnumerator Reload_Ammo()
    {
        yield return new WaitForSecondsRealtime(Reload_Duration);
        Is_Ammo_Loaded = true;
    }
}
