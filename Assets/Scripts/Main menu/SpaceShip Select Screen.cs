using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceShipSelectScreen : MonoBehaviour
{

    [SerializeField] private Color Lighter;
    [SerializeField] private Color Darker;
    [SerializeField] private Color Redder;
    [SerializeField] private Color Greener;

    [SerializeField] private GameObject Main_Menu_Screen;
    [SerializeField] private GameObject Select_SpaceShip;
    [SerializeField] private Transform Transition_Screen;
    private Vector3 Transition_Screen_Reset_Position = new Vector3(-1920, 0, 0);


    [SerializeField] private Image BackGround;

    [SerializeField] private SpaceShipSelectValue SpaceShip_Select_Value;

    private WaitForSeconds WaitForSeconds_1 = new WaitForSeconds(1);

    private void Start()
    {
        Reset_All_SpaceShip();

        Transition_Screen.localPosition = Transition_Screen_Reset_Position;
    }

    public void Reset_All_SpaceShip()
    {
        On_Unhover_SpaceShip_0();
        On_Unhover_SpaceShip_1();
        On_Unhover_SpaceShip_2();
    }


    #region SpaceShip 0
    [Header("SpaceShip 0")]

    [SerializeField] private Transform SpaceShip_0_Object;
    [SerializeField] private Image[] Ligter_On_Hover_SpaceShip_0_Images = new Image[7];
    [SerializeField] private Image SpaceShip_0_Silhouette;
    [SerializeField] private TextMeshProUGUI[] Ligter_On_Hover_SpaceShip_0_Texts = new TextMeshProUGUI[4];

    private string SpaceShip_0_String = "SpaceShip_0";

    public void On_Hover_SpaceShip_0()
    {
        On_Hover_Spaceship(SpaceShip_0_Object, Ligter_On_Hover_SpaceShip_0_Images, SpaceShip_0_Silhouette, Ligter_On_Hover_SpaceShip_0_Texts, SpaceShip_0_String);
    }
    public void On_Unhover_SpaceShip_0()
    {
        On_Unhover_Spaceship(SpaceShip_0_Object, Ligter_On_Hover_SpaceShip_0_Images, SpaceShip_0_Silhouette, Ligter_On_Hover_SpaceShip_0_Texts, SpaceShip_0_String);
    }

    public void On_Click_SpaceShip_0()
    {
        if (PlayerPrefs.GetInt(SpaceShip_0_String) == 0)
        {
            return;
        }
        StartCoroutine(SpaceShip_0_Button_Transition());
    }

    private IEnumerator SpaceShip_0_Button_Transition()
    {
        
        Transition_Screen.gameObject.SetActive(true);
        Transition_Screen.DOLocalMoveX(0f, 1f);

        SpaceShip_Select_Value.SpaceShip_Index = 0;


        yield return WaitForSeconds_1;

        SceneManager.LoadScene(1);

    }

    #endregion

    #region SpaceShip 1
    [Header("SpaceShip 1")]

    [SerializeField] private Transform SpaceShip_1_Object;
    [SerializeField] private Image[] Ligter_On_Hover_SpaceShip_1_Images = new Image[7];
    [SerializeField] private Image SpaceShip_1_Silhouette;
    [SerializeField] private TextMeshProUGUI[] Ligter_On_Hover_SpaceShip_1_Texts = new TextMeshProUGUI[4];
    private string SpaceShip_1_String = "SpaceShip_1";

    public void On_Hover_SpaceShip_1()
    {
        On_Hover_Spaceship(SpaceShip_1_Object, Ligter_On_Hover_SpaceShip_1_Images, SpaceShip_1_Silhouette, Ligter_On_Hover_SpaceShip_1_Texts, SpaceShip_1_String);
    }
    public void On_Unhover_SpaceShip_1()
    {
        On_Unhover_Spaceship(SpaceShip_1_Object, Ligter_On_Hover_SpaceShip_1_Images, SpaceShip_1_Silhouette, Ligter_On_Hover_SpaceShip_1_Texts, SpaceShip_1_String);
    }

    public void On_Click_SpaceShip_1()
    {
        if (PlayerPrefs.GetInt(SpaceShip_1_String) == 0)
        {
            return;
        }
        StartCoroutine(SpaceShip_1_Button_Transition());
    }

    private IEnumerator SpaceShip_1_Button_Transition()
    {
        Transition_Screen.gameObject.SetActive(true);
        Transition_Screen.DOLocalMoveX(0f, 1f);

        SpaceShip_Select_Value.SpaceShip_Index = 1;


        yield return WaitForSeconds_1;

        SceneManager.LoadScene(1);
    }

    #endregion

    #region SpaceShip 2
    [Header("SpaceShip 2")]

    [SerializeField] private Transform SpaceShip_2_Object;
    [SerializeField] private Image[] Ligter_On_Hover_SpaceShip_2_Images = new Image[7];
    [SerializeField] private Image SpaceShip_2_Silhouette;
    [SerializeField] private TextMeshProUGUI[] Ligter_On_Hover_SpaceShip_2_Texts = new TextMeshProUGUI[4];

    private string SpaceShip_2_String = "SpaceShip_2";

    public void On_Hover_SpaceShip_2()
    {
        On_Hover_Spaceship(SpaceShip_2_Object, Ligter_On_Hover_SpaceShip_2_Images, SpaceShip_2_Silhouette, Ligter_On_Hover_SpaceShip_2_Texts, SpaceShip_2_String);
    }
    public void On_Unhover_SpaceShip_2()
    {
        On_Unhover_Spaceship(SpaceShip_2_Object, Ligter_On_Hover_SpaceShip_2_Images, SpaceShip_2_Silhouette, Ligter_On_Hover_SpaceShip_2_Texts,SpaceShip_2_String);
    }

    public void On_Click_SpaceShip_2()
    {
        if (PlayerPrefs.GetInt(SpaceShip_2_String) == 0)
        {
            return;
        }
        StartCoroutine(SpaceShip_2_Button_Transition());
    }

    private IEnumerator SpaceShip_2_Button_Transition()
    {
        Transition_Screen.gameObject.SetActive(true);
        Transition_Screen.DOLocalMoveX(0f, 1f);

        SpaceShip_Select_Value.SpaceShip_Index = 2;


        yield return WaitForSeconds_1;

        SceneManager.LoadScene(1);

    }

    #endregion


    #region Cross Button
    [Header("Cross Button")]

    [SerializeField] private Transform Cross_Button_Object;
    [SerializeField] private TextMeshProUGUI Cross_Button_Image;
    [SerializeField] private GameObject Cross_Button_BackGround;

    private Vector3 Scale_Up_Position = new Vector3(-200, -180, 0);
    private Vector3 Scale_Down_Position = new Vector3(200, 65, 0);


    public void On_Hover_Cross_Button()
    {
        Cross_Button_Object.DOScale(1.5f, 0.25f);
        Cross_Button_Object.DOLocalMove(Scale_Up_Position, 0.25f);
        Cross_Button_Image.color = Lighter;
        Cross_Button_BackGround.SetActive(true);

    }

    public void On_Unhover_Cross_Button()
    {
        Cross_Button_Object.DOScale(1.0f, 0.25f);
        Cross_Button_Object.DOLocalMove(Scale_Down_Position, 0.25f);
        Cross_Button_Image.color = Darker;
        Cross_Button_BackGround.SetActive(false);
    }

    public void On_Click_Cross_Button()
    {
        Select_SpaceShip.SetActive(false);
        Main_Menu_Screen.SetActive(true);

        On_Unhover_Cross_Button();
    }

#endregion


    #region Select SpaceShip
    private void On_Hover_Spaceship(Transform Object_SpaceShip, Image[] Lighter_On_Hover,Image SpaceShip_Silhouette, TextMeshProUGUI[] SpaceShip_Texts,string SpaceShip_Store_Key)
    {
        Object_SpaceShip.DOScale(1f, 0.25f);
        
        for (int i = 0; i < 7; i++)
        {
            Lighter_On_Hover[i].color = Lighter;
        }
        for (int i = 0; i < 4; i++)
        {
            SpaceShip_Texts[i].color = Lighter;
        }

        switch (PlayerPrefs.GetInt(SpaceShip_Store_Key))
        {
            case 0: SpaceShip_Silhouette.color = Redder; 
                break;
            case 1: SpaceShip_Silhouette.color = Greener;
                break;
        }

        BackGround.color = Darker;
    }
    private void On_Unhover_Spaceship(Transform Object_SpaceShip, Image[] Lighter_On_Hover, Image SpaceShip_Silhouette, TextMeshProUGUI[] SpaceShip_Texts, string SpaceShip_Store_Key)
    {
        Object_SpaceShip.DOScale(0.85f, 0.25f);

        for (int i = 0; i < 7; i++)
        {
            Lighter_On_Hover[i].color = Darker;
        }
        for (int i = 0; i < 4; i++)
        {
            SpaceShip_Texts[i].color = Darker;
        }

        switch (PlayerPrefs.GetInt(SpaceShip_Store_Key))
        {
            case 0:
                SpaceShip_Silhouette.color = Redder;
                break;
            case 1:
                SpaceShip_Silhouette.color = Greener;
                break;
        }

        BackGround.color = Lighter;
    }
    #endregion

}
