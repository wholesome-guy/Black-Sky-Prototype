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

    [SerializeField] private MoneyValues Money_Values;

    private WaitForSeconds WaitForSeconds_Reload_Time;
    private void OnEnable()
    {
        ProjectileWheelManager.Projectile_Select_Event += Projectile_Select;
        Mouse_Input_Manager.Shoot_Action += Shoot_Projectile;
    }
    private void OnDisable()
    {
        ProjectileWheelManager.Projectile_Select_Event -= Projectile_Select;
        Mouse_Input_Manager.Shoot_Action -= Shoot_Projectile;

    }
    // Switches between available ammo types (projectiles)
    private void Start()
    {
        Projectiles = Projectiles_Scriptable_Object.Projectile_Gameobjects;
        WaitForSeconds_Reload_Time = new WaitForSeconds(Reload_Duration);
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
            switch (Index_Projectile)
            {
                case 0: 
                    MoneyManager.Money_Change_Event.Invoke(Money_Values.Cannon_Projectile_Cost);
                    MoneyManager.Money_Spent = false;
                    break;
                case 1:
                    MoneyManager.Money_Change_Event.Invoke(Money_Values.Anchor_Projectile_Cost);
                    MoneyManager.Money_Spent = false;

                    break;
                case 2:
                    MoneyManager.Money_Change_Event.Invoke(Money_Values.Destroy_Projectile_Cost);
                    MoneyManager.Money_Spent = false;

                    break;
                case 3:
                    MoneyManager.Money_Change_Event.Invoke(Money_Values.Data_Projectile_Cost);
                    MoneyManager.Money_Spent = false;

                    break;
                case 4:
                    MoneyManager.Money_Change_Event.Invoke(Money_Values.Mist_Projectile_Cost);
                    MoneyManager.Money_Spent = false;

                    break;
                case 5:
                    MoneyManager.Money_Change_Event.Invoke(Money_Values.BlackHole_Projectile_Cost);
                    MoneyManager.Money_Spent = false;

                    break;


            }

            
            
                Instantiate(Projectiles[Index_Projectile], transform.position, transform.rotation);
                Muzzle_Flash.Play();
                Is_Ammo_Loaded = false;
                TimerManager.Cannon_Reload_Event?.Invoke(Reload_Duration);
                StartCoroutine(Reload_Ammo());
            
        }
    }

    // Waits for the specified reload duration before allowing the next shot
    IEnumerator Reload_Ammo()
    {
        yield return WaitForSeconds_Reload_Time;
        Is_Ammo_Loaded = true;
    }
}
