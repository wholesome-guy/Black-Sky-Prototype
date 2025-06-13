using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class StickingAnchorScript : MonoBehaviour
{
    [SerializeField] private GameObject Docking_Zone;
    [SerializeField] private float Docking_Distance;
    
    public static bool Is_Docking_Zone_Instantiated = false;
    
    private SpringJoint Asteroid_SpringJoint;
    private AsteroidScript Asteroid_Script;

    public GameObject Ship_Anchor;
    private Rigidbody Player_Rigidbody;

    public bool Is_Right_Anchor_Taken;

    public static Action<float> Asteroid_Mass_Transfer;

    private void OnEnable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed += Docking_Zone_Instantiate;

        DockingZoneCollisionManager.On_Player_Docked += Instantiate_Rope;

        Keyboard_Input_Manager.De_Tether += De_Tether_Function;
    }

    private void OnDisable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed -= Docking_Zone_Instantiate;

        DockingZoneCollisionManager.On_Player_Docked -= Instantiate_Rope;

        Keyboard_Input_Manager.De_Tether -= De_Tether_Function;

    }

    private void Start()
    {

        Asteroid_SpringJoint = gameObject.transform.parent.GetComponent<SpringJoint>();
        Asteroid_Script = gameObject.transform.parent.GetComponent<AsteroidScript>();
        Anchor_Selector();
        
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
    

    private void Anchor_Selector()
    {
        if (!Is_Right_Anchor_Taken)
        {
            Ship_Anchor = PlayerSingleton.instance.Left_Ship_Anchor;
        }
        else
        {
            Ship_Anchor = PlayerSingleton.instance.Right_Ship_Anchor;
        }
    }

    private void Instantiate_Rope()
    {
        Player_Rigidbody = PlayerSingleton.instance.Player_Rigidbody;
        Asteroid_SpringJoint.connectedBody = Player_Rigidbody;
        Asteroid_Tethered();
    }

    private void Asteroid_Tethered()
    {
        Asteroid_Script.Is_Asteroid_Tethered = true;
        Asteroid_Mass_Transfer.Invoke(Asteroid_Script.Asteroid_Mass);
    }

    private void De_Tether_Function()
    {
        Asteroid_Script.Is_Asteroid_Tethered = false;

        Asteroid_SpringJoint.connectedBody = null;

        Docking_Zone_Re_Instantiate();
    }

}
