using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Instantiater : MonoBehaviour
{
    public GameObject cube;

    private void Start()
    {
        for (int i = 0; i< 100; i++)
        {
            Instantiate(cube, new Vector3(0,0,100*i), Quaternion.identity);
            Instantiate(cube, new Vector3(100 * i, 0, 0), Quaternion.identity);
            Instantiate(cube, new Vector3(-100 * i, 0, 0), Quaternion.identity);

        }
    }

    public void randomfunction()
    {
        Debug.Log("Fired");
    }

}
