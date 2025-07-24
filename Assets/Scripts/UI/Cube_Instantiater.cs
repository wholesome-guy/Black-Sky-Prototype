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
            for(int r = 0; r < 100; r++)
            {
                Instantiate(cube, new Vector3(100*c, 0, 100*r), Quaternion.identity);
            }         
        }
    }

  
}
