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

    [SerializeField] private Vector3 Closest_Ship_Anchor;

    private static bool Is_Docking_Zone_Instantiated = false;

    private void OnEnable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed += Tether_Sequence;
        DockingZoneCollisionManager.On_Player_Docked += Ship_Anchor;
    }

    private void OnDisable()
    {
        AnchorProjectileMovement.Sticking_Anchor_Deployed -= Tether_Sequence;
        DockingZoneCollisionManager.On_Player_Docked -= Ship_Anchor;

    }

    private void Tether_Sequence(Vector3 normal)
    {
        Docking_Zone_Instantiate(normal);
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

    private void Ship_Anchor(Vector3 Left_Ship_Anchor, Vector3 Right_Ship_Anchor)
    {
        float Left_Ship_Anchor_Distance = Vector3.Distance(transform.position, Left_Ship_Anchor);
        float Right_Ship_Anchor_Distance = Vector3.Distance(transform.position, Right_Ship_Anchor);

        if (Left_Ship_Anchor_Distance > Right_Ship_Anchor_Distance)
        {
            Closest_Ship_Anchor = Right_Ship_Anchor;

        }
        else
        {
            Closest_Ship_Anchor = Left_Ship_Anchor;
        }

        Line_Renderer_Sequence();
    }

    private void Line_Renderer_Sequence()
    {
        Line_Renderer.positionCount = 2;

        StartCoroutine(Animate_Line_Renderer(gameObject.transform.position,Closest_Ship_Anchor));
    }

    IEnumerator Animate_Line_Renderer(Vector3 Starting_Position, Vector3 End_Position)
    {
        float time = 0f;
        float duration = 1f;

        while(time < duration)
        {
            time += Time.deltaTime;
            
            float t  =time / duration;

            Vector3 Current_Position = Vector3.Lerp(Starting_Position, End_Position, t);

            Line_Renderer.SetPosition(0, Starting_Position);
            Line_Renderer.SetPosition(1, Current_Position);
            yield return null;
        }

        Line_Renderer.SetPosition(0, Starting_Position);
        Line_Renderer.SetPosition(1, End_Position);
    }

    
  
}
