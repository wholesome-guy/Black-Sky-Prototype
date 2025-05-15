using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Throttle_Handling_Lever : MonoBehaviour
{
    [SerializeField] private SpaceShip_Movement_Controller SpaceShip_Movement_Controller;
    [SerializeField] private Slider Throttle_Lever;
    [SerializeField] private Slider Handling_Lever;

    public UnityEvent Low_Throttle;
    public UnityEvent Moderate_Throttle;
    public UnityEvent High_Throttle;

    public UnityEvent Low_Handling;
    public UnityEvent Moderate_Handling;
    public UnityEvent High_Handling;

    // Start is called before the first frame update
    void Start()
    {
        Low_Throttle.AddListener(SpaceShip_Movement_Controller.Low_Throttle);
        Low_Handling.AddListener(SpaceShip_Movement_Controller.Low_Handling);
        Moderate_Throttle.AddListener(SpaceShip_Movement_Controller.Moderate_Throttle);
        Moderate_Handling.AddListener(SpaceShip_Movement_Controller.Moderate_Handling);
        High_Throttle.AddListener(SpaceShip_Movement_Controller.High_Throttle);
        High_Handling.AddListener(SpaceShip_Movement_Controller.High_Handling);
        Preset_Throttle_Hnadling();
    }

    private void Preset_Throttle_Hnadling()
    {
        Low_Throttle.Invoke();
        Low_Handling.Invoke();
        Throttle_Lever.value = 0;
        Handling_Lever.value = 0;
    }
  

    public void Throttle_Function()
    {
        switch (Throttle_Lever.value)
        {
            case 0:

                Low_Throttle.Invoke();
                
                break;
            case 1:

                Moderate_Throttle.Invoke();
                
                break;
            case 2:

                High_Throttle.Invoke();
                
                break;

        }
    }
    public void Handling_Function()
    {
        switch (Handling_Lever.value)
        {
            case 0:

                Low_Handling.Invoke();
                
                break;
            case 1:

                Moderate_Handling.Invoke();
                
                break;
            case 2:

                High_Handling.Invoke();
                
                break;

        }
    }

}
