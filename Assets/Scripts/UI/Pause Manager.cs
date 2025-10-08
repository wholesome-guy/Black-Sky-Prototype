using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    [SerializeField] private GameObject Pause_Menu;
    private bool Pause_Switch = false;
    [SerializeField] private Color Darker;
    [SerializeField] private Color Lighter;


    private PlayerSingleton Player_Singleton;
    private Keyboard_Input_Manager Keyboard_Input_Manager_;

    private void Start()
    {
        Player_Singleton = PlayerSingleton.instance;
        Keyboard_Input_Manager_ = Keyboard_Input_Manager.instance;
        Pause_Menu.SetActive(false);
    }
    

    public void Pause_Switch_Function()
    {
        Pause_Switch = !Pause_Switch;

        if (Pause_Switch)
        {
            On_Pause();
        }
        else
        {
            Off_Pause();
        }
    }

    private void On_Pause()
    {
        Keyboard_Input_Manager_.On_Pause();
        Pause_Menu.SetActive(true);
        Time.timeScale = 0.0f;
        Player_Singleton.Is_Spaceship_Able_To_Shoot = false;
        Player_Singleton.Is_Spaceship_At_Rest = true;
    }
    private void Off_Pause()
    {
        Keyboard_Input_Manager_.Off_Pause();
        Pause_Menu.SetActive(false);
        Time.timeScale = 1.0f;
        Player_Singleton.Is_Spaceship_Able_To_Shoot = true;
        Player_Singleton.Is_Spaceship_At_Rest = false;
    }

    #region Settings

    [Header("Settings")]
    [SerializeField] private Transform Settings_Transform;
    [SerializeField] private GameObject Settings_Background;
    [SerializeField] private TextMeshProUGUI Settings_Text;


    public void On_Hover_Settings_Button()
    {
        On_Hover_Button(Settings_Transform, Settings_Text, Settings_Background);
    }

    public void On_Unhover_Settings_Button()
    {
        On_Unhover_Button(Settings_Transform, Settings_Text, Settings_Background);
    }
    public void On_Click_Settings_Button()
    {
        // Settings
    }

    #endregion

    #region Quit

    [Header("Quit")]
    [SerializeField] private Transform Quit_Transform;
    [SerializeField] private GameObject Quit_Background;
    [SerializeField] private TextMeshProUGUI Quit_Text;


    public void On_Hover_Quit_Button()
    {
        On_Hover_Button(Quit_Transform, Quit_Text, Quit_Background);
    }

    public void On_Unhover_Quit_Button()
    {
        On_Unhover_Button(Quit_Transform, Quit_Text, Quit_Background);
    }
    public async void On_Click_Quit_Button()
    {
        Off_Pause();
        UIVisualEffectsManager.Transition_Screen_Event.Invoke(-1920, 0, 3f);
        await Task.Delay(4000);
        SceneManager.LoadScene(0);
    }

    #endregion


    #region Basic button Functions 

    private void On_Hover_Button(Transform Button_Object, TextMeshProUGUI Button_text, GameObject Button_Background)
    {
        Button_Object.transform.DOScale(1.5f, 0.25f).SetUpdate(true); ;

        Button_text.color = Darker;
        Button_Background.SetActive(true);
    }

    private void On_Unhover_Button(Transform Button_Object, TextMeshProUGUI Button_text, GameObject Button_Background)
    {
        Button_Object.transform.DOScale(1.0f, 0.25f).SetUpdate(true);

        Button_text.color = Lighter;
        Button_Background.SetActive(false);
    }
    #endregion

}
