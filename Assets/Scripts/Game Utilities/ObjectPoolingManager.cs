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

    #region Cannon Ball
    [SerializeField] private GameObject Cannon_Ball;
    private Queue<GameObject> Cannon_Ball_Pool = new Queue<GameObject>();
    private int Cannon_Ball_Pool_Count;
    public GameObject Instantiate_Cannon_Ball()
    {
        Cannon_Ball_Pool_Count = Cannon_Ball_Pool.Count;
        if (Cannon_Ball_Pool_Count > 0)
        {
            GameObject Object = Cannon_Ball_Pool.Dequeue();
            Object.SetActive(true);
            return Object;
        }
        return Instantiate(Cannon_Ball);
    }

    public void Destroy_Cannon_Ball(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        StartCoroutine(Delay_Destroy_Cannon_Ball(Wait_Duration, Object, Trail_Renderer));
    }

    private IEnumerator Delay_Destroy_Cannon_Ball(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        yield return new WaitForSeconds(Wait_Duration);
       
        Trail_Renderer.Clear();
        Object.SetActive(false);
        Cannon_Ball_Pool.Enqueue(Object);
    }
    #endregion

    #region Anchor Projectile Left
    [SerializeField] private GameObject Anchor_Projectile_Left;
    private Queue<GameObject> Anchor_Projectile_Left_Pool = new Queue<GameObject>();
    private int Anchor_Projectile_Left_Pool_Count;
    public GameObject Instantiate_Anchor_Projectile_Left()
    {
        Anchor_Projectile_Left_Pool_Count = Anchor_Projectile_Left_Pool.Count;
        if (Anchor_Projectile_Left_Pool_Count > 0)
        {
            GameObject Object = Anchor_Projectile_Left_Pool.Dequeue();
            Object.SetActive(true);
            return Object;
        }
        return Instantiate(Anchor_Projectile_Left);
    }

    public void Destroy_Anchor_Projectile_Left(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        StartCoroutine(Delay_Destroy_Anchor_Projectile_Left(Wait_Duration, Object, Trail_Renderer));
    }

    private IEnumerator Delay_Destroy_Anchor_Projectile_Left(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        yield return new WaitForSeconds(Wait_Duration);

        Trail_Renderer.Clear();
        Object.SetActive(false);
        Anchor_Projectile_Left_Pool.Enqueue(Object);
    }
    #endregion

    #region Anchor Projectile Right
    [SerializeField] private GameObject Anchor_Projectile_Right;
    private Queue<GameObject> Anchor_Projectile_Right_Pool = new Queue<GameObject>();
    private int Anchor_Projectile_Right_Pool_Count;
    public GameObject Instantiate_Anchor_Projectile_Right()
    {
        Anchor_Projectile_Right_Pool_Count = Anchor_Projectile_Right_Pool.Count;
        if (Anchor_Projectile_Right_Pool_Count > 0)
        {
            GameObject Object = Anchor_Projectile_Right_Pool.Dequeue();
            Object.SetActive(true);
            return Object;
        }
        return Instantiate(Anchor_Projectile_Right);
    }

    public void Destroy_Anchor_Projectile_Right(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        StartCoroutine(Delay_Destroy_Anchor_Projectile_Right(Wait_Duration, Object, Trail_Renderer));
    }

    private IEnumerator Delay_Destroy_Anchor_Projectile_Right(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        yield return new WaitForSeconds(Wait_Duration);

        Trail_Renderer.Clear();
        Object.SetActive(false);
        Anchor_Projectile_Right_Pool.Enqueue(Object);
    }
    #endregion

    #region Deselect Projectile
    [SerializeField] private GameObject Deselect_Projectile;
    private Queue<GameObject> Deselect_Projectile_Pool = new Queue<GameObject>();
    private int Deselect_Projectile_Pool_Count;
    public GameObject Instantiate_Deselect_Projectile()
    {
        Deselect_Projectile_Pool_Count = Deselect_Projectile_Pool.Count;
        if (Deselect_Projectile_Pool_Count > 0)
        {
            GameObject Object = Deselect_Projectile_Pool.Dequeue();
            Object.SetActive(true);
            return Object;
        }
        return Instantiate(Deselect_Projectile);
    }

    public void Destroy_Deselect_Projectile(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        StartCoroutine(Delay_Destroy_Deselect_Projectile(Wait_Duration, Object, Trail_Renderer));
    }

    private IEnumerator Delay_Destroy_Deselect_Projectile(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        yield return new WaitForSeconds(Wait_Duration);

        Trail_Renderer.Clear();
        Object.SetActive(false);
        Deselect_Projectile_Pool.Enqueue(Object);
    }
    #endregion

    #region Data Projectile
    [SerializeField] private GameObject Data_Projectile;
    private Queue<GameObject> Data_Projectile_Pool = new Queue<GameObject>();
    private int Data_Projectile_Pool_Count;
    public GameObject Instantiate_Data_Projectile()
    {
        Data_Projectile_Pool_Count = Data_Projectile_Pool.Count;
        if (Data_Projectile_Pool_Count > 0)
        {
            GameObject Object = Data_Projectile_Pool.Dequeue();
            Object.SetActive(true);
            return Object;
        }
        return Instantiate(Data_Projectile);
    }

    public void Destroy_Data_Projectile(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        StartCoroutine(Delay_Destroy_Data_Projectile(Wait_Duration, Object, Trail_Renderer));
    }

    private IEnumerator Delay_Destroy_Data_Projectile(float Wait_Duration, GameObject Object, TrailRenderer Trail_Renderer)
    {
        yield return new WaitForSeconds(Wait_Duration);

        Trail_Renderer.Clear();
        Object.SetActive(false);
        Data_Projectile_Pool.Enqueue(Object);
    }
    #endregion

}
