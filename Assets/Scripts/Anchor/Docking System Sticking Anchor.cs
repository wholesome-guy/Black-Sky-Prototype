using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingSystemStickingAnchor : MonoBehaviour
{
    [SerializeField] private GameObject Docking_Zone;
    [SerializeField] private float Docking_Distance;

    private static bool Is_Docking_Zone_Instantiated = false;

    private void OnEnable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed += Docking_Zone_Instantiate;
        Keyboard_Input_Manager.De_Tether += Docking_Zone_Re_Instantiate;
        DockingZoneCollisionManager.On_Player_Undocked += UnDocked;
    }
    private void OnDisable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed -= Docking_Zone_Instantiate;
        Keyboard_Input_Manager.De_Tether -= Docking_Zone_Re_Instantiate;
        DockingZoneCollisionManager.On_Player_Undocked += UnDocked;
    }

    private void Docking_Zone_Instantiate(Vector3 Direction)
    {
        if (!Is_Docking_Zone_Instantiated)
        {
            Direction = Direction.normalized;

            Vector3 Docking_Direction = new Vector3(Direction.x, Direction.y, Direction.z) * Docking_Distance;

            Instantiate(Docking_Zone, Docking_Direction, Quaternion.identity);

            Is_Docking_Zone_Instantiated = true;

        }
    }
    private void Docking_Zone_Re_Instantiate()
    {
        Vector3 Player_Position = PlayerSingleton.instance.Player_Transform.position;
        Vector3 Random_Position = new Vector3(Random.Range(-1, +1) * Docking_Distance + Player_Position.x, Player_Position.y, Random.Range(-1, +1) * Docking_Distance + Player_Position.z);

        if (!Is_Docking_Zone_Instantiated)
        {
            Instantiate(Docking_Zone, Random_Position, Quaternion.identity);

            Is_Docking_Zone_Instantiated = true;
        }
    }
    private void UnDocked()
    {
        Is_Docking_Zone_Instantiated = false;
    }
}
