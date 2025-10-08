using DG.Tweening;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitScreenManager : MonoBehaviour
{
    [SerializeField] private Color Darker;
    [SerializeField] private Color Ligter;

    [SerializeField] private GameObject Exit_Screen;
    [SerializeField] private TextMeshProUGUI Money_Counter_Text;

    private string Player_Money_Key = "Current_Player_Money";

    public static Action Exit_Screen_Event;

    private PlayerSingleton Player_Singleton;

    private void OnEnable()
    {
        Exit_Screen_Event += Exit_Screen_Initiate;
    }
    private void OnDisable()
    {
        Exit_Screen_Event -= Exit_Screen_Initiate;
    }
    void Start()
    {
        Player_Singleton = PlayerSingleton.instance;
        Exit_Screen.SetActive(false);

    }

    private async void Exit_Screen_Initiate()
    {
        UIVisualEffectsManager.Transition_Screen_Event.Invoke(-1920, 0, 3f);

        await Task.Delay(4000);
        Exit_Screen.SetActive(true);
        Money_Counter_Text.text = MoneyNotation.Money_Notate_Function(PlayerPrefs.GetFloat(Player_Money_Key));

        UIVisualEffectsManager.Transition_Screen_Event.Invoke(0, -1920, 3f);
    }


    #region Continue Working 

    [Header("Continue Working")]
    [SerializeField] private Transform Continue_Working_Transform;
    [SerializeField] private GameObject Continue_Working_Background;
    [SerializeField] private TextMeshProUGUI Continue_Working_Text;


    public void On_Hover_Continue_Working_Button()
    {
        On_Hover_Button(Continue_Working_Transform, Continue_Working_Text, Continue_Working_Background);
    }

    public void On_Unhover_Continue_Working_Button()
    {
        On_Unhover_Button(Continue_Working_Transform, Continue_Working_Text, Continue_Working_Background);
    }

    public async void On_Click_Continue_Working_Button()
    {

        UIVisualEffectsManager.Transition_Screen_Event.Invoke(1920, 0, 3f);

        MoneyManager.Money_Saved.Invoke();
        //Account Credted You are faith Full Soul
        await Task.Delay(4000);

        Exit_Screen.SetActive(false);

        UIVisualEffectsManager.Transition_Screen_Event.Invoke(0, -1920, 3f);

        Player_Singleton.Is_Spaceship_At_Rest = false;
        Player_Singleton.Is_Spaceship_Able_To_Shoot = true;

    }


    #endregion

    #region Return To Base

    [Header("Return To Base")]
    [SerializeField] private Transform Return_To_Base_Transform;
    [SerializeField] private GameObject Return_To_Base_Background;
    [SerializeField] private TextMeshProUGUI Return_To_Base_Text;


    public void On_Hover_Return_To_Base_Button()
    {
        On_Hover_Button(Return_To_Base_Transform, Return_To_Base_Text, Return_To_Base_Background);
    }

    public void On_Unhover_Return_To_Base_Button()
    {
        On_Unhover_Button(Return_To_Base_Transform, Return_To_Base_Text, Return_To_Base_Background);
    }
    public async void On_Click_Return_To_Base_Button()
    {

        UIVisualEffectsManager.Transition_Screen_Event.Invoke(1920, 0, 3f);

        MoneyManager.Money_Saved.Invoke();
        //Account Credted You are faith Full Soul

        Exit_Screen.SetActive(false);

        await Task.Delay(4000);
        SceneManager.LoadScene(0);

    }

    #endregion


    #region Basic button Functions 

    private void On_Hover_Button(Transform Button_Object, TextMeshProUGUI Button_text, GameObject Button_Background)
    {
        Button_Object.transform.DOScale(1.5f, 0.25f);

        Button_text.color = Ligter;
        Button_Background.SetActive(true);
    }

    private void On_Unhover_Button(Transform Button_Object, TextMeshProUGUI Button_text, GameObject Button_Background)
    {
        Button_Object.transform.DOScale(1.0f, 0.25f);

        Button_text.color = Darker;
        Button_Background.SetActive(false);
    }
    #endregion

}
