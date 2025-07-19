using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_Rate_Locker : MonoBehaviour
{
    [SerializeField] private int FPS = 60;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = FPS;
    }

    
}
