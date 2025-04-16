using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_Rate_Locker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    
}
