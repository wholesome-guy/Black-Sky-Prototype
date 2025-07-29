using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Instantiater : MonoBehaviour
{
    public GameObject cube;

    private void Start()
    {
        for (int c = 0; c< 100; c++)
        {
            Instantiate(cube, new Vector3(-50, 0, 100*c), Quaternion.identity);
            Instantiate(cube, new Vector3(50, 0, 100 * c), Quaternion.identity);

        }
    }

  
}
