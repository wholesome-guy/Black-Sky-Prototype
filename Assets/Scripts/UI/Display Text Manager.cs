using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTextManager : MonoBehaviour
{
    [SerializeField] private GameObject Display_Text_GameObject;
    [SerializeField] private CanvasGroup Display_Text_Canvas_Group;
    [SerializeField] private TextMeshProUGUI Display_Text;

    public static Action<string> Display_Text_Event;

    private void OnEnable()
    {
        Display_Text_Event += Display_Text_Function;
    }
    private void OnDisable()
    {
        Display_Text_Event -= Display_Text_Function;
    }

    private void Start()
    {
        Display_Text_GameObject.SetActive(false);
        Display_Text_Canvas_Group.alpha = 0f;
    }

    private void Display_Text_Function(string Text)
    {
        Display_Text_GameObject.SetActive(true);
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Display_Text_Canvas_Group, 0, 1, 0.5f);
        Display_Text.text = Text;
        StartCoroutine(Display_Text_Hide());
    }

    private IEnumerator Display_Text_Hide()
    {
        yield return new WaitForSeconds(2f);
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Display_Text_Canvas_Group, 1, 0, 0.5f);
        Display_Text_GameObject.SetActive(false);
    }
}
