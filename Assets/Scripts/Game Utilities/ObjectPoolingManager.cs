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

    public GameObject Instantiate_Explosion()
    {
        if (Explosion_VFX_Pool.Count > 0)
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
}
