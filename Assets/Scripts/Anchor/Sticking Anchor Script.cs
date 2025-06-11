using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickingAnchorScript : MonoBehaviour
{
    [SerializeField] private GameObject Docking_Zone;
    [SerializeField] private float Docking_Distance;
    private static bool Is_Docking_Zone_Instantiated = false;

    private SpringJoint Asteroid_SpringJoint;

    public GameObject Ship_Anchor;

    private Rigidbody Player_Rigidbody;

    public bool Is_Right_Anchor_Taken;

    private void OnEnable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed += Docking_Zone_Instantiate;
        DockingZoneCollisionManager.On_Player_Docked += Instantiate_Rope;
    }

    private void OnDisable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed -= Docking_Zone_Instantiate;
        DockingZoneCollisionManager.On_Player_Docked -= Instantiate_Rope;

    }

    private void Start()
    {

        Asteroid_SpringJoint = gameObject.transform.parent.GetComponent<SpringJoint>();
        
        Anchor_Selector();

    }
    private void Docking_Zone_Instantiate(Vector3 Direction)
    {
        if (!Is_Docking_Zone_Instantiated)
        {
            Direction = Direction.normalized;

            Vector3 Docking_Direction = new Vector3(Direction.x, 0, Direction.z) * Docking_Distance;

            Instantiate(Docking_Zone, Docking_Direction, Quaternion.identity);

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
    }

    

}
