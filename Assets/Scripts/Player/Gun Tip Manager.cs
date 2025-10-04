using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GunTipManager : MonoBehaviour
{
    [SerializeField] private VisualEffect Muzzle_Flash;
    [SerializeField] private float Reload_Duration = 5f; // Time between consecutive shots
    private int Index_Projectile; // Current projectile index

    private bool Is_Ammo_Loaded = true; // Flag to check if weapon can shoot
    [SerializeField] private bool Is_Right;

    [SerializeField] private MoneyValues Money_Values;

    private ObjectPoolingManager Object_Pooling_Manager;

    private GameObject Cannon_Ball;
    private GameObject Anchor_Projectile;
    private GameObject Deselect_Projectile;
    private GameObject Data_Projectile;

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
        Object_Pooling_Manager = ObjectPoolingManager.Instance;
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

                    Spend_Money(Money_Values.Cannon_Projectile_Cost);

                    Cannon_Ball = Object_Pooling_Manager.Instantiate_Cannon_Ball();
                    Cannon_Ball.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

                    break;
                case 1:
                    Spend_Money(Money_Values.Anchor_Projectile_Cost);
                    

                    if (Is_Right)
                    {
                        Anchor_Projectile = Object_Pooling_Manager.Instantiate_Anchor_Projectile_Right();
                        Anchor_Projectile.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
                    }
                    else
                    {
                        Anchor_Projectile = Object_Pooling_Manager.Instantiate_Anchor_Projectile_Left();
                        Anchor_Projectile.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
                    }

                    break;
                case 2:
                    Spend_Money(Money_Values.Destroy_Projectile_Cost);
                    

                    Deselect_Projectile = Object_Pooling_Manager.Instantiate_Deselect_Projectile();
                    Deselect_Projectile.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

                    break;
                case 3:
                    Spend_Money(Money_Values.Data_Projectile_Cost);
                    

                    Data_Projectile = Object_Pooling_Manager.Instantiate_Data_Projectile();
                    Data_Projectile.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

                    break;
                case 4:
                    Spend_Money(Money_Values.Mist_Projectile_Cost);
                    

                    break;
                case 5:
                    Spend_Money(Money_Values.BlackHole_Projectile_Cost);
                    

                    break;


            }

            
            
               // Instantiate(Projectiles[Index_Projectile], transform.position, transform.rotation);
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

    private void Spend_Money(int Cost)
    {
        MoneyManager.Money_Change_Event.Invoke(Cost);
        MoneyManager.Money_Spent = false;
    }
}
