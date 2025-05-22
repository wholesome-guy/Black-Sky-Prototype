using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickingAnchorScript : MonoBehaviour
{
    [SerializeField] private GameObject Docking_Zone;
    [SerializeField] private float Docking_Distance;
    [SerializeField] private LineRenderer Line_Renderer;
    [SerializeField] private GameObject Rope;
    private static bool Is_Docking_Zone_Instantiated = false;

    private GameObject Ship_Anchor;

    public bool Is_Right_Anchor_Taken;

    private void OnEnable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed += Docking_Zone_Instantiate;
        DockingZoneCollisionManager.On_Player_Docked += Start_Line_Renderer;
    }

    private void OnDisable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed -= Docking_Zone_Instantiate;
        DockingZoneCollisionManager.On_Player_Docked -= Start_Line_Renderer;

    }
    
    

    private void LateUpdate()
    {
        Line_Renderer_Sequence();
    }


    private void Start()
    {
        Line_Renderer.enabled = false;
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
    private void Start_Line_Renderer()
    {
        Line_Renderer.enabled = true;
    }

    private void Anchor_Selector()
    {
        if (!Is_Right_Anchor_Taken)
        {
            Ship_Anchor = Anchor_Singleton.instance.Left_Ship_Anchor;
        }
        else
        {
            Ship_Anchor = Anchor_Singleton.instance.Right_Ship_Anchor;
        }
    }

    private void Line_Renderer_Sequence()
    {
        Line_Renderer.positionCount = 2;

        Line_Renderer.SetPosition(0, transform.position);
        Line_Renderer.SetPosition(1, Ship_Anchor.transform.position);

    }

}
