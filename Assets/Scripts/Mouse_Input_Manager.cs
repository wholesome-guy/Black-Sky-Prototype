using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse_Input_Manager : MonoBehaviour
{
    private Vector2 Mouse_Input;
    public Vector2 Normalised_Mouse_Input; 
    public static Mouse_Input_Manager instance;
    public int Mouse_Sensitivity; 
    // 1 to 100 Mouse_Sensitivity
    private void Awake()
    {
        if(instance != null && instance !=this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        Mouse_Sensitivity = 1;
    }

    void Update()
    {
        Normalised_Mouse_Input_Method();
    }

    void Normalised_Mouse_Input_Method()
    {
        //New Input System way to get Mouse Position
        Mouse_Input = Mouse.current.position.ReadValue();
        //Making sure the Mouse Input stays on Screen
        Mouse_Input.x = Mathf.Clamp(Mouse_Input.x, 0, Screen.width);
        Mouse_Input.y = Mathf.Clamp(Mouse_Input.y, 0, Screen.height);

        Normalised_Mouse_Input = new Vector2((Mouse_Input.x / Screen.width) * 2f - 1f, (Mouse_Input.y / Screen.height) * 2f - 1f);
    }

}
