using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.VFX;

public class ThrusterManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer Left_Thruster;
    [SerializeField] private MeshRenderer Right_Thruster;
    [SerializeField] private VisualEffect Particles_VFX;

    private Material Left_Thruster_Material;
    private Material Right_Thruster_Material;
    [SerializeField] private Material Flame_Trail;

    private float Thrust_Value;
    [SerializeField] private float Low_Throttle_Velocity = 59f;
    [SerializeField] private float Moderate_Throttle_Velocity= 127f;
    [SerializeField] private float High_Throttle_Velocity = 245f;

    private float Expected_Thrust_Value;
    [SerializeField] private float Expected_Thrust_Value_Low = 0.75f;
    [SerializeField] private float Expected_Thrust_Value_Moderate = 0.85f;
    [SerializeField] private float Expected_Thrust_Value_High = 0.9f;

     private float Velocity_To_Thurst_Constant;
     private float Low_Velocity_To_Thurst_Constant;
     private float Moderate_Velocity_To_Thurst_Constant;
     private float High_Velocity_To_Thurst_Constant;

    [ColorUsage(true, true)]
    [SerializeField] private Color[] Color1 = new Color[3];
    [ColorUsage(true, true)]
    [SerializeField] private Color[] Color2 = new Color[3];
    [ColorUsage(true, true)]
    [SerializeField] private Color[] Color3 = new Color[3];

    private float Particle_Rate;

    public bool Particle_Switch = false;
    private void Start()
    {
        Low_Velocity_To_Thurst_Constant = Expected_Thrust_Value_Low / Low_Throttle_Velocity;
        Moderate_Velocity_To_Thurst_Constant = Expected_Thrust_Value_Moderate / Moderate_Throttle_Velocity;
        High_Velocity_To_Thurst_Constant = Expected_Thrust_Value_High / High_Throttle_Velocity;

        Left_Thruster_Material = Left_Thruster.material;
        Right_Thruster_Material = Right_Thruster.material;
        Low_Throttle();
    }

    private void Update()
    {
        Thrust_Value = Mathf.Clamp(PlayerSingleton.instance.Player_Rigidbody.velocity.magnitude*Velocity_To_Thurst_Constant,0,Expected_Thrust_Value);
        
        Left_Thruster_Material.SetFloat("_ThrustPower", Thrust_Value);
        Right_Thruster_Material.SetFloat("_ThrustPower", Thrust_Value);

        bool isMoving = PlayerSingleton.instance.Player_Rigidbody.velocity.sqrMagnitude > 0.01f; // use sqrMagnitude for performance

        if (isMoving && !Particle_Switch)
        {
            Particles_Play();
            Particle_Switch = true;
        }
        else if (!isMoving && Particle_Switch)
        {
            Particles_Stop();
            Particle_Switch = false;
        }


    }

    private void Particles_Play()
    {
        Particles_VFX.Play();
    }
    private void Particles_Stop()
    {
        Particles_VFX.Stop();
    }
    public void Low_Throttle()
    {
        Velocity_To_Thurst_Constant = Low_Velocity_To_Thurst_Constant;
        Expected_Thrust_Value = Expected_Thrust_Value_Low;
        Particle_Rate = 5;

        Left_Thruster_Material.SetColor("_Colour_1", Color1[0]);
        Left_Thruster_Material.SetColor("_Colour_2", Color2[0]);

        Right_Thruster_Material.SetColor("_Colour_1", Color1[0]);
        Right_Thruster_Material.SetColor("_Colour_2", Color2[0]);

        Flame_Trail.SetColor("_Colour_1", Color1[0]);
        Flame_Trail.SetColor("_Colour_2", Color2[0]);


        Particles_VFX.SetFloat("Rate", Particle_Rate);
        Particles_VFX.SetVector4("Colour", Color3[0]);

    }
    public void Moderate_Throttle()
    {
        Velocity_To_Thurst_Constant = Moderate_Velocity_To_Thurst_Constant;
        Expected_Thrust_Value = Expected_Thrust_Value_Moderate;
        Particle_Rate = 10;

        Left_Thruster_Material.SetColor("_Colour_1", Color1[1]);
        Left_Thruster_Material.SetColor("_Colour_2", Color2[1]);

        Right_Thruster_Material.SetColor("_Colour_1", Color1[1]);
        Right_Thruster_Material.SetColor("_Colour_2", Color2[1]);

        Flame_Trail.SetColor("_Colour_1", Color1[1]);
        Flame_Trail.SetColor("_Colour_2", Color2[1]);

        Particles_VFX.SetFloat("Rate", Particle_Rate);
        Particles_VFX.SetVector4("Colour", Color3[1]);


    }
    public void High_Throttle()
    {
        Velocity_To_Thurst_Constant = High_Velocity_To_Thurst_Constant;
        Expected_Thrust_Value = Expected_Thrust_Value_High;
        Particle_Rate = 15;

        Left_Thruster_Material.SetColor("_Colour_1", Color1[2]);
        Left_Thruster_Material.SetColor("_Colour_2", Color2[2]);

        Right_Thruster_Material.SetColor("_Colour_1", Color1[2]);
        Right_Thruster_Material.SetColor("_Colour_2", Color2[2]);

        Flame_Trail.SetColor("_Colour_1", Color1[2]);
        Flame_Trail.SetColor("_Colour_2", Color2[2]);

        Particles_VFX.SetFloat("Rate", Particle_Rate);
        Particles_VFX.SetVector4("Colour", Color3[2]);


    }

}
