using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingSystemStickingAnchor : MonoBehaviour
{
    [SerializeField] private float Docking_Distance;
    private GameObject Current_Docking_Zone;

    [SerializeField] private MoneyValues Money_Values;
    private static bool Is_Docking_Zone_Instantiated = false;

    private PlayerSingleton Player_Singleton;
    private ObjectPoolingManager Object_Pooling_Manager;

    private void Awake()
    {
        Object_Pooling_Manager = ObjectPoolingManager.Instance;

    }
    private void OnEnable()
    {
        Player_Singleton = PlayerSingleton.instance;
        AnchorProjectileMovement.Sticking_Anchor_Deployed += Docking_Zone_Instantiate;
        DockingZoneCollisionManager.On_Player_Undocked += UnDocked;
        Keyboard_Input_Manager.De_Tether += Docking_Zone_Re_Instantiate;
        DeselectProjectile.Deselect += Delete_Current_Docking_Zone_On_Deselect;
    }
    private void OnDisable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed -= Docking_Zone_Instantiate;
        DockingZoneCollisionManager.On_Player_Undocked += UnDocked;
        Keyboard_Input_Manager.De_Tether -= Docking_Zone_Re_Instantiate;
        DeselectProjectile.Deselect -= Delete_Current_Docking_Zone_On_Deselect;
    }

    private void Docking_Zone_Instantiate(Vector3 Direction)
    {
        if (!Is_Docking_Zone_Instantiated)
        {
            Direction = Direction.normalized;

            Vector3 Docking_Direction = new Vector3(Direction.x, Direction.y, Direction.z) * Docking_Distance;

            Current_Docking_Zone = Object_Pooling_Manager.Instantiate_Docking_Zone();
            Current_Docking_Zone.transform.SetLocalPositionAndRotation(Docking_Direction,Quaternion.identity);

            Is_Docking_Zone_Instantiated = true;

            MoneyManager.Money_Change_Event.Invoke(Money_Values.Docking_Station_Making_Cost);


        }
    }
    private void Docking_Zone_Re_Instantiate()
    {
        if(Player_Singleton.Is_Spaceship_At_Selling_Zone)
        {
            return;
        }
        Vector3 Player_Position = PlayerSingleton.instance.Player_Transform.position;
        Vector3 Random_Position = new Vector3(UnityEngine.Random.Range(-1, +1) * Docking_Distance + Player_Position.x, Player_Position.y, UnityEngine.Random.Range(-1, +1) * Docking_Distance + Player_Position.z);

        if (!Is_Docking_Zone_Instantiated)
        {
            Current_Docking_Zone = Object_Pooling_Manager.Instantiate_Docking_Zone();
            Current_Docking_Zone.transform.SetLocalPositionAndRotation(Random_Position, Quaternion.identity);


            Is_Docking_Zone_Instantiated = true;

            MoneyManager.Money_Change_Event.Invoke(Money_Values.Docking_Station_Making_Cost);

        }
    }

    private void Delete_Current_Docking_Zone_On_Deselect()
    {
        if(Current_Docking_Zone != null)
        {
            DockingZonePointerManager.Pointer_Event(false, Current_Docking_Zone.gameObject.transform);
            Object_Pooling_Manager.Destroy_Docking_Zone(1f, Current_Docking_Zone);
            Is_Docking_Zone_Instantiated = false;

        }
    }
    private void UnDocked()
    {
        Is_Docking_Zone_Instantiated = false;
    }
}
