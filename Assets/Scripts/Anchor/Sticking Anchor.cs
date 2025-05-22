using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StickingAnchor : MonoBehaviour
{

    [SerializeField] private GameObject Docking_Zone;
    [SerializeField] private float Docking_Distance;
    [SerializeField] private LineRenderer Line_Renderer;
    [SerializeField] private GameObject Rope;

    [SerializeField] private GameObject Closest_Ship_Anchor;
    private GameObject Left_Ship_Anchor;
    private GameObject Right_Ship_Anchor;

    private static bool Is_Docking_Zone_Instantiated = false;

    private void OnEnable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed += Docking_Zone_Instantiate;
      //  DockingZoneCollisionManager.On_Player_Docked += Ship_Anchor;
    }

    private void OnDisable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed -= Docking_Zone_Instantiate;
        //DockingZoneCollisionManager.On_Player_Docked -= Ship_Anchor;

    }
    private void Awake()
    {
        Left_Ship_Anchor = Anchor_Singleton.instance.Left_Ship_Anchor;
        Right_Ship_Anchor = Anchor_Singleton.instance.Right_Ship_Anchor;
    }

    private void Update()
    {
        Ship_Anchor();
    }
    private void LateUpdate()
    {
        Line_Renderer_Sequence();
    }

    private void Docking_Zone_Instantiate(Vector3 Direction)
    {
        if(!Is_Docking_Zone_Instantiated)
        {
            Direction = Direction.normalized;

            Vector3 Docking_Direction = new Vector3(Direction.x, 0, Direction.z) * Docking_Distance;

            Instantiate(Docking_Zone, Docking_Direction, Quaternion.identity);

           Is_Docking_Zone_Instantiated = true;
        }
    }

    private void Ship_Anchor()
    {

            float Left_Ship_Anchor_Distance = Vector3.Distance(transform.position, Left_Ship_Anchor.transform.position);
            float Right_Ship_Anchor_Distance = Vector3.Distance(transform.position, Right_Ship_Anchor.transform.position);

            if (Left_Ship_Anchor_Distance > Right_Ship_Anchor_Distance)
            {
                Closest_Ship_Anchor = Right_Ship_Anchor;

            }
            else
            {
                Closest_Ship_Anchor = Left_Ship_Anchor;
            }

    }

    private void Line_Renderer_Sequence()
    {
        Line_Renderer.positionCount = 2;

        Line_Renderer.SetPosition(0,transform.position);
        Line_Renderer.SetPosition(1, Closest_Ship_Anchor.transform.position);
    }

    

    
  
}
