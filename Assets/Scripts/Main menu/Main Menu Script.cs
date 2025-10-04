using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Color Text_Colour_Unhover;
    [SerializeField] private Color Text_Colour_Hover;

    [SerializeField] private GameObject Main_Menu_Screen;
    [SerializeField] private GameObject Select_SpaceShip;
    [SerializeField] private GameObject Econmony_Screen;
    [SerializeField] private GameObject Quit_Screen;


    [SerializeField] private Transform Transition_Screen;
    private Vector3 Transition_Screen_Reset_Position = new Vector3(-1920, 0, 0);

    private WaitForSeconds WaitForSeconds_1 = new WaitForSeconds(1);


    private void Start()
    {
        Main_Menu_Screen.SetActive(true);
        Select_SpaceShip.SetActive(false);
        Transition_Screen.gameObject.SetActive(false);
        //Econmony_Screen.SetActive(false);
    }


    #region Play Button

    [Header("Play Button")]
    [SerializeField] private Transform Play_Button_Object;
    [SerializeField] private TextMeshProUGUI Play_Button_Text;
    [SerializeField] private GameObject Play_Button_BackGround;

    private Vector2 Wrap_Position = new Vector2(900, 900);

    public void On_Hover_Play_Button()
    {
        On_Hover_Button(Play_Button_Object, Play_Button_Text, Play_Button_BackGround,500,0);
    }

    public void On_Unhover_Play_Button()
    {
        On_Unhover_Button(Play_Button_Object, Play_Button_Text, Play_Button_BackGround);
    }

    public void On_Click_Play_Button()
    {
        StartCoroutine(Play_Button_Transition());
    }

    private IEnumerator Play_Button_Transition()
    {
        Transition_Screen.gameObject.SetActive(true);
        Transition_Screen.DOLocalMoveX(0f, 1f);

        Mouse.current.WarpCursorPosition(Wrap_Position);

        yield return WaitForSeconds_1;


        Select_SpaceShip.SetActive(true);
        Main_Menu_Screen.SetActive(false);

        On_Unhover_Play_Button();

        Transition_Screen.localPosition = Transition_Screen_Reset_Position;

    }

    #endregion

    #region Economy Button

    [Header("Economy Button")]

    [SerializeField] private Transform Economy_Button_Object;
    [SerializeField] private TextMeshProUGUI Economy_Button_Text;
    [SerializeField] private GameObject Economy_Button_BackGround;


    public void On_Hover_Economy_Button()
    {
        On_Hover_Button(Economy_Button_Object, Economy_Button_Text, Economy_Button_BackGround,500,100);
    }

    public void On_Unhover_Economy_Button()
    {
        On_Unhover_Button(Economy_Button_Object, Economy_Button_Text, Economy_Button_BackGround);
    }

    #endregion

    #region Quit Button

    [Header("Quit Button")]

    [SerializeField] private Transform Quit_Button_Object;
    [SerializeField] private TextMeshProUGUI Quit_Button_Text;
    [SerializeField] private GameObject Quit_Button_BackGround;


    public void On_Hover_Quit_Button()
    {
        On_Hover_Button(Quit_Button_Object, Quit_Button_Text, Quit_Button_BackGround,500,200);
    }

    public void On_Unhover_Quit_Button()
    {
        On_Unhover_Button(Quit_Button_Object, Quit_Button_Text, Quit_Button_BackGround);
    }

    #endregion

    #region Settings Button

    [Header("Settings Button")]

    [SerializeField] private Transform Settings_Button_Object;
    [SerializeField] private Image Settings_Button_Image;
    [SerializeField] private GameObject Settings_Button_BackGround;

    [SerializeField] private Vector3 Rotate_Unhover;
    [SerializeField] private Vector3 Rotate_Hover;

    public void On_Hover_Settings_Button()
    {
        Settings_Button_Object.DOScale(1.5f, 0.25f);
        Settings_Button_Image.transform.DOLocalRotate(Rotate_Hover,0.25f);
        Settings_Button_Image.color = Text_Colour_Hover;
        Settings_Button_BackGround.SetActive(true);

    }

    public void On_Unhover_Settings_Button()
    {
        Settings_Button_Object.DOScale(1.0f, 0.25f);
        Settings_Button_Image.transform.DOLocalRotate(Rotate_Unhover, 0.25f);
        Settings_Button_Image.color = Text_Colour_Unhover;
        Settings_Button_BackGround.SetActive(false);
    }

    #endregion


    #region Basic button Functions 

    private void On_Hover_Button(Transform Button_Object,TextMeshProUGUI Button_text,GameObject Button_Background,float LocalX,float LocalY)
    {
        Button_Object.transform.DOScale(1.5f, 0.25f);
        Button_Object.transform.DOLocalMoveX(LocalX, 0.25f);
        Button_Object.transform.DOLocalMoveY(LocalY, 0.25f);

        Button_text.color = Text_Colour_Hover;
        Button_Background.SetActive(true);
    }

    private void On_Unhover_Button(Transform Button_Object, TextMeshProUGUI Button_text, GameObject Button_Background)
    {
        Button_Object.transform.DOScale(1.0f, 0.25f);
        Button_Object.transform.DOLocalMoveX(0.0f, 0.25f);
        Button_Object.transform.DOLocalMoveY(0.0f, 0.25f);


        Button_text.color = Text_Colour_Unhover;
        Button_Background.SetActive(false);
    }
    #endregion
}
