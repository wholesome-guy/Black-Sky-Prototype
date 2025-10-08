using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageVFX : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] SpaceShip_0;
    [SerializeField] private MeshRenderer[] SpaceShip_1;
    [SerializeField] private MeshRenderer[] SpaceShip_2;

    [SerializeField] private Material Shield_Material;
    [SerializeField] private Material Damage_Material;
    [SerializeField] private Material Shield_Rengeration_Material;

    [SerializeField] private Material[] Spaceship_0_Material;
    [SerializeField] private Material[] Spaceship_1_Material;
    [SerializeField] private Material[] Spaceship_2_Material;



    [SerializeField] private float Duration;

    private WaitForSeconds WaitForSeconds_Duration;

    public static Action Shield_Break_Event;
    public static Action Damage_Break_Event;
    public static Action Shield_Regeneration_Event;
    public static Action<Vector3> Debris_Event;

    private ObjectPoolingManager Object_Pooling_Manager;
    private GameObject Damage_Particle;


    private void Start()
    {
        WaitForSeconds_Duration = new WaitForSeconds(Duration);
        Object_Pooling_Manager = ObjectPoolingManager.Instance;
    }
    private void OnEnable()
    {
        Shield_Break_Event += Shield_Break;
        Damage_Break_Event += Damage_Break;
        Shield_Regeneration_Event += Shield_Rengeneration;
        Collision_Manager_SpaceShip.Debris_VFX += Debris_VFX;
        Debris_Event += Debris_VFX;
    }
    private void OnDisable()
    {
        Shield_Break_Event -= Shield_Break;
        Damage_Break_Event -= Damage_Break;
        Shield_Regeneration_Event -= Shield_Rengeneration;
        Collision_Manager_SpaceShip.Debris_VFX -= Debris_VFX;
        Debris_Event -= Debris_VFX;

    }


    private void Shield_Break()
    {
        StartCoroutine(Material_Change(SpaceShip_0, Spaceship_0_Material, Shield_Material, Duration));
        StartCoroutine(Material_Change(SpaceShip_1, Spaceship_1_Material,Shield_Material,Duration));
        StartCoroutine(Material_Change(SpaceShip_2, Spaceship_2_Material, Shield_Material, Duration));

    }

    private void Damage_Break()
    {
        StartCoroutine(Material_Change(SpaceShip_0, Spaceship_0_Material, Damage_Material, Duration));
        StartCoroutine(Material_Change(SpaceShip_1, Spaceship_1_Material, Damage_Material, Duration));
        StartCoroutine(Material_Change(SpaceShip_2, Spaceship_2_Material, Damage_Material, Duration));

    }

    private void Shield_Rengeneration()
    {
        StartCoroutine(Material_Change(SpaceShip_0, Spaceship_0_Material, Shield_Rengeration_Material, Duration));
        StartCoroutine(Material_Change(SpaceShip_1, Spaceship_1_Material, Shield_Rengeration_Material, Duration));
        StartCoroutine(Material_Change(SpaceShip_2, Spaceship_2_Material, Shield_Rengeration_Material, Duration));
    }

    private void Debris_VFX(Vector3 Point)
    {
        Damage_Particle = Object_Pooling_Manager.Instantiate_Hit_Particle();
        Damage_Particle.transform.SetLocalPositionAndRotation(Point,Quaternion.identity);

        Object_Pooling_Manager.Destroy_Hit_Particle(3f, Damage_Particle);
    }

    private IEnumerator Material_Change(MeshRenderer[] SpaceShip, Material[] Original, Material Change, float Duration)
    {
        for (int i = 0; i < SpaceShip.Length; i++)
        {
            SpaceShip[i].material = Original[i];
            SpaceShip[i].material = Change;
        }

        yield return WaitForSeconds_Duration;

        for (int i = 0; i < SpaceShip.Length; i++)
        {
            SpaceShip[i].material = Original[i];
        }
    }
}
