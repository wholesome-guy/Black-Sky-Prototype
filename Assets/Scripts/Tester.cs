using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private int set = 10;
    private void Start()
    {
        Test_Function(set);
    }

    private void Test_Function(int Sample)
    {
        for (int i = 0; i <= Sample; i++)
        {
            Debug.Log("Break Velocity:"+ AsteroidData.Joint_Break_Velocity_Player(i * 1000)/245f + " Asteroid_Mass" + ":" + i * 1000); ;

        }
    }
}
