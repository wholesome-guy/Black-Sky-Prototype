using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VerletRopeScript : MonoBehaviour
{
    [SerializeField] private StickingAnchorScript StickingAnchorScript;

    [SerializeField] private LineRenderer Line_Renderer;
    private GameObject Ship_Anchor;

    //Verlet Rope 
    private struct Rope_Segment
    {
        public Vector3 Current_Position;
        public Vector3 Previous_Position;

        public Rope_Segment(Vector3 Position)
        {
            this.Current_Position = Position;
            this.Previous_Position = Position;
        }

    }

    private Rope_Segment[] Rope_Segments_Array;
    [SerializeField] private int Number_Of_Rope_Segment = 50;
    [SerializeField] private float Length_Of_Rope_Segment = 0.25f;
    [SerializeField] private Vector3 Tension_Vector = new Vector3(0, -2, 0);
    [SerializeField] private int Number_Of_Constrain_Run = 50;




    private void OnEnable()
    {
        DockingZoneCollisionManager.On_Player_Docked += Start_Line_Renderer;
    }

    private void OnDisable()
    {
        DockingZoneCollisionManager.On_Player_Docked -= Start_Line_Renderer;
    }
    private void LateUpdate()
    {
       // Line_Renderer_Sequence();

        Draw_Verlet_Rope();
    }
    private void Start()
    {
        Line_Renderer.enabled = false;
        Ship_Anchor = StickingAnchorScript.Ship_Anchor;

        Verlet_Rope_Setup();
    }

    private void FixedUpdate()
    {
        Simulate_Verlet_Rope();
    }
    private void Verlet_Rope_Setup()
    {
        Vector3 Start_Position = gameObject.transform.position;

        Rope_Segments_Array = new Rope_Segment[Number_Of_Rope_Segment];

        for (int i = 0; i < Number_Of_Rope_Segment; i++)
        {
            this.Rope_Segments_Array[i] = new Rope_Segment(Start_Position);
            Start_Position.y -= Length_Of_Rope_Segment;
        }

    }

    private void Draw_Verlet_Rope()
    {
        Vector3[] Segment_Positions = new Vector3[Number_Of_Rope_Segment];

        Line_Renderer.positionCount = Number_Of_Rope_Segment;

        for (int i = 0;i < Number_Of_Rope_Segment;i++)
        {
            Segment_Positions[i] = Rope_Segments_Array[i].Current_Position;
        }
        Line_Renderer.SetPositions(Segment_Positions);
    }

    private void Simulate_Verlet_Rope()
    {
        for (int i = 0; i < Number_Of_Rope_Segment; i++)
        {
            Rope_Segment rope_Segment = Rope_Segments_Array[i];

            Vector3 Difference_Between_Current_And_Previous_Position = rope_Segment.Current_Position - rope_Segment.Previous_Position;

            rope_Segment.Current_Position = rope_Segment.Previous_Position;

            rope_Segment.Current_Position += Difference_Between_Current_And_Previous_Position;

            rope_Segment.Current_Position += Tension_Vector * Time.fixedDeltaTime;

            this.Rope_Segments_Array[i] = rope_Segment;

        }

        for (int i = 0; i< Number_Of_Constrain_Run; i++)
        {
            Constrains_Verlet_Rope();
        }
    }

    private void Constrains_Verlet_Rope()
    {
        Rope_Segment Asteroid_Anchor = Rope_Segments_Array[0];
        Asteroid_Anchor.Current_Position = gameObject.transform.position;
        this.Rope_Segments_Array[0] = Asteroid_Anchor;

        Rope_Segment Ship_Anchor_Local = Rope_Segments_Array[Number_Of_Rope_Segment - 1];
        Ship_Anchor_Local.Current_Position = Ship_Anchor.transform.position;
        this.Rope_Segments_Array[Number_Of_Rope_Segment - 1] = Ship_Anchor_Local;

        for (int i = 0;i < Number_Of_Rope_Segment - 1; i++)
        {
            Rope_Segment Nth_Rope_Segment = this.Rope_Segments_Array[i];
            Rope_Segment Nth_Plus_1_Rope_Segment = this.Rope_Segments_Array[i + 1];

            Vector3 Distance_Vector_Between_Segments = Nth_Rope_Segment.Current_Position - Nth_Plus_1_Rope_Segment.Current_Position;

            float Distance = Distance_Vector_Between_Segments.magnitude;

            float Difference = Distance - Length_Of_Rope_Segment;

            Vector3 Change_Vector = Distance_Vector_Between_Segments.normalized * Difference;

            if(i != 0)
            {
                Nth_Rope_Segment.Current_Position -= Change_Vector * 0.5f;
                Nth_Plus_1_Rope_Segment.Current_Position += Change_Vector * 0.5f;
            }
            else
            {
                Nth_Plus_1_Rope_Segment.Current_Position += Change_Vector;
            }

            this.Rope_Segments_Array[i] = Nth_Rope_Segment;
            this.Rope_Segments_Array[i + 1] = Nth_Plus_1_Rope_Segment;

        }
    }

    private void Start_Line_Renderer()
    {
        Line_Renderer.enabled = true;
    }
    private void Line_Renderer_Sequence()
    {
        Line_Renderer.positionCount = 2;

        Line_Renderer.SetPosition(0, transform.position);
        Line_Renderer.SetPosition(1, Ship_Anchor.transform.position);

    }
}
