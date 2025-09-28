using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{

    #region Singleton Pattern
    public static ObjectPoolingManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    #region Explosion
    [SerializeField] private GameObject Explosion_VFX;
    private Queue<GameObject> Explosion_VFX_Pool =new Queue<GameObject>();
    private int Explosion_Pool_Count;

    public GameObject Instantiate_Explosion()
    {
        Explosion_Pool_Count = Explosion_VFX_Pool.Count;
        if (Explosion_Pool_Count > 0)
        {
           GameObject Object = Explosion_VFX_Pool.Dequeue();
           Object.SetActive(true);
           return Object;
        }
           return Instantiate(Explosion_VFX); 
    }

    public void Destroy_Explosion(float Wait_Duration,GameObject Object)
    {
       StartCoroutine(Delay_Destroy_Explosion(Wait_Duration,Object));
    }

    private IEnumerator Delay_Destroy_Explosion(float Wait_Duration,GameObject Object)
    {
        yield return new WaitForSeconds(Wait_Duration);

        Object.SetActive(false);
        Explosion_VFX_Pool.Enqueue(Object);
    }
    #endregion

    #region Hit Particle
    [SerializeField] private GameObject Hit_Particle_VFX;
    private Queue<GameObject> Hit_Particle_VFX_Pool = new Queue<GameObject>();
    private int Hit_Particle_Pool_Count;
    public GameObject Instantiate_Hit_Particle()
    {
        Hit_Particle_Pool_Count = Hit_Particle_VFX_Pool.Count;
        if (Hit_Particle_Pool_Count > 0)
        {
            GameObject Object = Hit_Particle_VFX_Pool.Dequeue();
            Object.SetActive(true);
            return Object;
        }
        return Instantiate(Hit_Particle_VFX);
    }

    public void Destroy_Hit_Particle(float Wait_Duration, GameObject Object)
    {
        StartCoroutine(Delay_Destroy_Hit_Particle(Wait_Duration, Object));
    }

    private IEnumerator Delay_Destroy_Hit_Particle(float Wait_Duration, GameObject Object)
    {
        yield return new WaitForSeconds(Wait_Duration);

        Object.SetActive(false);
        Hit_Particle_VFX_Pool.Enqueue(Object);
    }
    #endregion

    #region Docking Zone
    [SerializeField] private GameObject Docking_Zone;
    private Queue<GameObject> Docking_Zone_Pool = new Queue<GameObject>();
    private int Docking_Zone_Pool_Count;
    public GameObject Instantiate_Docking_Zone()
    {
        Docking_Zone_Pool_Count = Docking_Zone_Pool.Count;
        if (Docking_Zone_Pool_Count > 0)
        {
            GameObject Object = Docking_Zone_Pool.Dequeue();
            Object.SetActive(true);
            return Object;
        }
        return Instantiate(Docking_Zone);
    }

    public void Destroy_Docking_Zone(float Wait_Duration, GameObject Object)
    {
        StartCoroutine(Delay_Destroy_Docking_Zone(Wait_Duration, Object));
    }

    private IEnumerator Delay_Destroy_Docking_Zone(float Wait_Duration, GameObject Object)
    {
        yield return new WaitForSeconds(Wait_Duration);

        Object.SetActive(false);
        Docking_Zone_Pool.Enqueue(Object);
    }
    #endregion


}
