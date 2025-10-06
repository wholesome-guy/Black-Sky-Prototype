using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EconomyScreenManager : MonoBehaviour
{

    [SerializeField] private Color Lighter;
    [SerializeField] private Color Darker;
    [SerializeField] private GameObject Economy_Screen;
    [SerializeField] private GameObject Main_Menu_Screen;

    private void Start()
    {
        Take_Loan_Function(100000f);
        Money_Counter_Text.text = MoneyNotation.Money_Notate_Function(PlayerPrefs.GetFloat(Player_Money_Key));
        Pay_Back_Loan_Function();
        Shop_Price_Setter();

    }
    private void Update()
    {
        Pay_Back_Loan_Function();
    }

    #region Money Counter

    [Header("Money")]

    [SerializeField] private TextMeshProUGUI Money_Counter_Text;
    [SerializeField] private Image Money_Counter_BackGround;
    [SerializeField] private Transform Money_Counter_Object;

    [SerializeField] private Vector3 Money_Scale_Up_Position;
    [SerializeField] private Vector3 Money_Scale_Down_Position;

    [SerializeField] private Color Positive_Money_Gain;
    [SerializeField] private Color Negative_Money_Gain;
    private Color Money_Color;
    private string Player_Money_Key = "Current_Player_Money";

    private float Total_Money;
    private float Money_Plus_Change;

    private void Current_Money_System(float Change_Amount)
    {
        Total_Money = PlayerPrefs.GetFloat(Player_Money_Key);
        Money_Plus_Change = Total_Money + Change_Amount;
        if(Change_Amount > 0)
        {
            Money_Color = Positive_Money_Gain;
        }
        else
        {
            Money_Color =Negative_Money_Gain;
        }

        StartCoroutine(Money_Lerp(5f,Money_Color));
    }

    private IEnumerator Money_Lerp(float Duration,Color Color)
    {
        float Time_Elapsed = 0;

        Money_Counter_Text.color = Color;
        Money_Counter_BackGround.color = Lighter;
        Money_Counter_Object.DOScale(1.2f, 0.25f);
        Money_Counter_Object.DOLocalMove(Money_Scale_Up_Position, 0.25f);

        while (Time_Elapsed < Duration)
        {
            Time_Elapsed += Time.deltaTime;

            float value = Mathf.Lerp(Total_Money, Money_Plus_Change, Time_Elapsed / Duration);
            Money_Counter_Text.text = Mathf.RoundToInt(value).ToString();
            yield return null;
        }
        Total_Money = Money_Plus_Change;
        

        PlayerPrefs.SetFloat(Player_Money_Key, Total_Money);

        Money_Counter_Text.text = MoneyNotation.Money_Notate_Function(Total_Money);

        Money_Counter_Text.color = Lighter;
        Money_Counter_BackGround.color = Darker;
        Money_Counter_Object.DOScale(1.0f, 0.25f);
        Money_Counter_Object.DOLocalMove(Money_Scale_Down_Position, 0.25f);

        


    }

    #endregion

    #region Take Loan


    #region BackEnd

    [Header("Take Loan")]
    [SerializeField] private float Lower_Loan_Limit;
    [SerializeField] private float Upper_Loan_Limit;
    [SerializeField] private float Change_Amount;
    [SerializeField] private TextMeshProUGUI Take_Loan_Amount_Text;
    private float Take_Loan_Amount = 0;


    private void Take_Loan_Function(float Change)
    {
        Take_Loan_Amount += Change;
        Take_Loan_Amount =Mathf.Clamp(Take_Loan_Amount,Lower_Loan_Limit,Upper_Loan_Limit);
        Take_Loan_Amount_Text.text = MoneyNotation.Money_Notate_Function(Take_Loan_Amount);
    }


    #endregion

    #region FrontEnd

    [Header("Take Loan Button")]
    [SerializeField] private Transform Take_Loan_Button_Object;
    [SerializeField] private TextMeshProUGUI Take_Loan_Button_Text;
    [SerializeField] private Image Take_Loan_Button_BackGround;

    private string Taken_Loan_Key = "Taken_Loan_Amount";
    private float Total_Loan = 0;

    public void On_Hover_Take_Loan_Button()
    {
        On_Hover_Button(Take_Loan_Button_Object, Take_Loan_Button_Text, Take_Loan_Button_BackGround, 783.3f, -187.6f);
    }

    public void On_Unhover_Take_Loan_Button()
    {
        On_Unhover_Button(Take_Loan_Button_Object, Take_Loan_Button_Text, Take_Loan_Button_BackGround, 491f, -187.6f);
    }

    public void On_Click_Take_Loan_Button()
    {
        Current_Money_System(+Take_Loan_Amount);
        Take_Loan_Button_Text.color = Positive_Money_Gain;
        Total_Loan = PlayerPrefs.GetFloat(Taken_Loan_Key) + Take_Loan_Amount;

        PlayerPrefs.SetFloat(Taken_Loan_Key, Total_Loan);

    }

    [Header("Plus Button")]
    [SerializeField] private Transform Plus_Button_Object;
    [SerializeField] private Image Plus_Button_Image;
    [SerializeField] private GameObject Plus_Button_BackGround;

    public void On_Hover_Plus_Button()
    {
        Plus_Button_BackGround.SetActive(true);
        Plus_Button_Object.DOScale(1.5f, 0.25f);
        Plus_Button_Object.DOLocalMoveX(460f, 0.25f);
        Plus_Button_Image.color = Darker;

    }

    public void On_Unhover_Plus_Button()
    {
        Plus_Button_BackGround.SetActive(false);
        Plus_Button_Object.DOScale(1.0f, 0.25f);
        Plus_Button_Object.DOLocalMoveX(500f, 0.25f);
        Plus_Button_Image.color = Lighter;
    }

    public void On_Click_Plus_Button()
    {
        
        Take_Loan_Function(+Change_Amount);
    }

    [Header("Minus Button")]
    [SerializeField] private Transform Minus_Button_Object;
    [SerializeField] private Image Minus_Button_Image;
    [SerializeField] private GameObject Minus_Button_BackGround;

    public void On_Hover_Minus_Button()
    {
        Minus_Button_BackGround.SetActive(true);
        Minus_Button_Object.DOScale(1.5f, 0.25f);
        Minus_Button_Object.DOLocalMoveX(480, 0.25f);
        Minus_Button_Image.color = Darker;

    }

    public void On_Unhover_Minus_Button()
    {
        Minus_Button_BackGround.SetActive(false);
        Minus_Button_Object.DOScale(1.0f, 0.25f);
        Minus_Button_Object.DOLocalMoveX(400, 0.25f);
        Minus_Button_Image.color = Lighter;
    }

    public void On_Click_Minus_Button()
    {
        Take_Loan_Function(-Change_Amount);
    }
    #endregion

    #endregion

    #region Pay Loan 

    #region BackEnd

    [SerializeField] private TextMeshProUGUI Pay_Back_Loan;

    private void Pay_Back_Loan_Function()
    {
        Pay_Back_Loan.text = MoneyNotation.Money_Notate_Function(PlayerPrefs.GetFloat(Taken_Loan_Key));
    }



    #endregion

    #region Frontend

    [Header("Pay Loan Button")]
    [SerializeField] private Transform Pay_Loan_Button_Object;
    [SerializeField] private TextMeshProUGUI Pay_Loan_Button_Text;
    [SerializeField] private Image Pay_Loan_Button_BackGround;

    public void On_Hover_Pay_Loan_Button()
    {
        On_Hover_Button(Pay_Loan_Button_Object, Pay_Loan_Button_Text, Pay_Loan_Button_BackGround, 284.64f, -371.53f);
    }

    public void On_Unhover_Pay_Loan_Button()
    {
        On_Unhover_Button(Pay_Loan_Button_Object, Pay_Loan_Button_Text, Pay_Loan_Button_BackGround, 0, -371.53f);
    }

    public void On_Click_Pay_Loan_Button()
    {
        if(PlayerPrefs.GetFloat(Player_Money_Key) < PlayerPrefs.GetFloat(Taken_Loan_Key))
        {
            Pay_Loan_Button_Text.color = Negative_Money_Gain;
            return;
        }
        Current_Money_System(-PlayerPrefs.GetFloat(Taken_Loan_Key));
        Pay_Loan_Button_Text.color = Positive_Money_Gain;

        PlayerPrefs.SetFloat(Taken_Loan_Key, 0);

        Pay_Back_Loan_Function();
    }
    #endregion

    #endregion

    #region Upgrades

    [Header("Upgrades Button")]
    [SerializeField] private Transform Upgrades_Button_Object;
    [SerializeField] private TextMeshProUGUI Upgrades_Button_Text;
    [SerializeField] private Image Upgrades_Button_BackGround;

    public void On_Hover_Upgrades_Button()
    {
        On_Hover_Button(Upgrades_Button_Object, Upgrades_Button_Text, Upgrades_Button_BackGround, 1244.34f, -299.175f);
    }

    public void On_Unhover_Upgrades_Button()
    {
        On_Unhover_Button(Upgrades_Button_Object, Upgrades_Button_Text, Upgrades_Button_BackGround, 942f, -299.175f);
    }


    #endregion

    #region Cross Button

    [Header("Cross Button")]

    [SerializeField] private Transform Cross_Button_Object;
    [SerializeField] private TextMeshProUGUI Cross_Button_Image;
    [SerializeField] private GameObject Cross_Button_BackGround;

    private Vector3 Scale_Up_Position = new Vector3(-1871, -141, 0);
    private Vector3 Scale_Down_Position = new Vector3(-1574.9f, 82f, 0);


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
        Economy_Screen.SetActive(false);
        Main_Menu_Screen.SetActive(true);

        On_Unhover_Cross_Button();
    }
    #endregion

    #region Shop
    [SerializeField] private SpaceShipSelectScreen SpaceShip_Select_Screen;

    private void Shop_Price_Setter()
    {
        SpaceShip_Price_Setter(SpaceShip_0_String, SpaceShip_0_Price_Text, SpaceShip_0_Price);
        SpaceShip_Price_Setter(SpaceShip_1_String, SpaceShip_1_Price_Text, SpaceShip_1_Price);
        SpaceShip_Price_Setter(SpaceShip_2_String, SpaceShip_2_Price_Text, SpaceShip_2_Price);

    }


    [Header("SpaceShip 0")]
    [SerializeField] private Transform SpaceShip_0_Transform;
    [SerializeField] private Image SpaceShip_0_BackGround;
    [SerializeField] private Image SpaceShip_0_Line;
    [SerializeField] private Image SpaceShip_0_Silhouette;
    [SerializeField] private TextMeshProUGUI SpaceShip_0_Name;
    [SerializeField] private TextMeshProUGUI SpaceShip_0_Price_Text;
    [SerializeField] private float SpaceShip_0_Price = 10000;

    private string SpaceShip_0_String = "SpaceShip_0"; 

    public void On_Hover_SpaceShip_0_Shop()
    {
        On_Hover_SpaceShip_Shop(SpaceShip_0_Transform, SpaceShip_0_BackGround, SpaceShip_0_Line, SpaceShip_0_Silhouette, SpaceShip_0_Name, SpaceShip_0_Price_Text, SpaceShip_0_String);
    }
    public void On_Unhover_SpaceShip_0_Shop()
    {
        On_Unhover_SpaceShip_Shop(SpaceShip_0_Transform, SpaceShip_0_BackGround, SpaceShip_0_Line, SpaceShip_0_Silhouette, SpaceShip_0_Name, SpaceShip_0_Price_Text, SpaceShip_0_String);
    }

    public void On_Click_SpaceShip_0_Shop()
    {
        On_Click_SpaceShip_Shop(SpaceShip_0_String, SpaceShip_0_Price,SpaceShip_0_Price_Text);
    }

    [Header("SpaceShip 1")]
    [SerializeField] private Transform SpaceShip_1_Transform;
    [SerializeField] private Image SpaceShip_1_BackGround;
    [SerializeField] private Image SpaceShip_1_Line;
    [SerializeField] private Image SpaceShip_1_Silhouette;
    [SerializeField] private TextMeshProUGUI SpaceShip_1_Name;
    [SerializeField] private TextMeshProUGUI SpaceShip_1_Price_Text;
    [SerializeField] private float SpaceShip_1_Price = 20000;

    private string SpaceShip_1_String = "SpaceShip_1";

    public void On_Hover_SpaceShip_1_Shop()
    {
        On_Hover_SpaceShip_Shop(SpaceShip_1_Transform, SpaceShip_1_BackGround, SpaceShip_1_Line, SpaceShip_1_Silhouette, SpaceShip_1_Name, SpaceShip_1_Price_Text, SpaceShip_1_String);
    }
    public void On_Unhover_SpaceShip_1_Shop()
    {
        On_Unhover_SpaceShip_Shop(SpaceShip_1_Transform, SpaceShip_1_BackGround, SpaceShip_1_Line, SpaceShip_1_Silhouette, SpaceShip_1_Name, SpaceShip_1_Price_Text, SpaceShip_1_String);
    }

    public void On_Click_SpaceShip_1_Shop()
    {
        On_Click_SpaceShip_Shop(SpaceShip_1_String, SpaceShip_1_Price, SpaceShip_1_Price_Text);
    }

    [Header("SpaceShip 2")]
    [SerializeField] private Transform SpaceShip_2_Transform;
    [SerializeField] private Image SpaceShip_2_BackGround;
    [SerializeField] private Image SpaceShip_2_Line;
    [SerializeField] private Image SpaceShip_2_Silhouette;
    [SerializeField] private TextMeshProUGUI SpaceShip_2_Name;
    [SerializeField] private TextMeshProUGUI SpaceShip_2_Price_Text;
    [SerializeField] private float SpaceShip_2_Price = 30000;

    private string SpaceShip_2_String = "SpaceShip_2";

    public void On_Hover_SpaceShip_2_Shop()
    {
        On_Hover_SpaceShip_Shop(SpaceShip_2_Transform, SpaceShip_2_BackGround, SpaceShip_2_Line, SpaceShip_2_Silhouette, SpaceShip_2_Name, SpaceShip_2_Price_Text, SpaceShip_2_String);
    }
    public void On_Unhover_SpaceShip_2_Shop()
    {
        On_Unhover_SpaceShip_Shop(SpaceShip_2_Transform, SpaceShip_2_BackGround, SpaceShip_2_Line, SpaceShip_2_Silhouette, SpaceShip_2_Name, SpaceShip_2_Price_Text, SpaceShip_2_String);
    }

    public void On_Click_SpaceShip_2_Shop()
    {
        On_Click_SpaceShip_Shop(SpaceShip_2_String, SpaceShip_2_Price, SpaceShip_2_Price_Text);
    }

    #endregion;





    #region Basic button Functions 

    private void On_Hover_Button(Transform Button_Object, TextMeshProUGUI Button_text, Image Button_Background, float LocalX, float LocalY)
    {
        Button_Object.transform.DOScale(1.5f, 0.25f);
        Button_Object.transform.DOLocalMoveX(LocalX, 0.25f);
        Button_Object.transform.DOLocalMoveY(LocalY, 0.25f);

        Button_text.color = Lighter;
        Button_Background.color = Darker;
    }

    private void On_Unhover_Button(Transform Button_Object, TextMeshProUGUI Button_text, Image Button_Background, float LocalX, float LocalY)
    {
        Button_Object.transform.DOScale(1.0f, 0.25f);
        Button_Object.transform.DOLocalMoveX(LocalX, 0.25f);
        Button_Object.transform.DOLocalMoveY(LocalY, 0.25f);


        Button_text.color = Darker;
        Button_Background.color = Lighter;
    }


    private void On_Hover_SpaceShip_Shop(Transform SpaceShip_Transform,Image SpaceShip_BackGround,Image SpaceShip_Line,Image SpaceShip_Silhouette,TextMeshProUGUI SpaceShip_Name,TextMeshProUGUI SpaceShip_Price_Text,string SpaceShip_Store_Key)
    {
        SpaceShip_Transform.DOScale(1.2f, 0.25f);

        SpaceShip_BackGround.color = Lighter;
        SpaceShip_Line.color = Lighter;

        SpaceShip_Silhouette.color = Darker;
        SpaceShip_Name.color = Darker;
        switch (PlayerPrefs.GetInt(SpaceShip_Store_Key))
        {
            case 0:
                SpaceShip_Price_Text.color = Darker;
                break;
            case 1:
                SpaceShip_Price_Text.color = Positive_Money_Gain;
                break;

            default:
                SpaceShip_Price_Text.color = Darker;
                break;
        }
    }

    private void On_Unhover_SpaceShip_Shop(Transform SpaceShip_Transform, Image SpaceShip_BackGround, Image SpaceShip_Line, Image SpaceShip_Silhouette, TextMeshProUGUI SpaceShip_Name, TextMeshProUGUI SpaceShip_Price_Text, string SpaceShip_Store_Key)
    {
        SpaceShip_Transform.DOScale(1.0f, 0.25f);

        SpaceShip_BackGround.color = Darker;
        SpaceShip_Line.color = Darker;

        SpaceShip_Silhouette.color = Lighter;
        SpaceShip_Name.color = Lighter;
        switch (PlayerPrefs.GetInt(SpaceShip_Store_Key))
        {
            case 0:
                SpaceShip_Price_Text.color = Lighter;
                break;
            case 1:
                SpaceShip_Price_Text.color = Positive_Money_Gain;
                break;

            default:
                SpaceShip_Price_Text.color = Lighter;
                break;
        }
    }

    private void On_Click_SpaceShip_Shop(string SpaceShip_Store_Key,float SpaceShip_Price,TextMeshProUGUI SpaceShip_Price_Text)
    {
        if(PlayerPrefs.GetInt(SpaceShip_Store_Key) == 1)
        {
            return;
        }
        if (PlayerPrefs.GetFloat(Player_Money_Key) > SpaceShip_Price)
        {
            PlayerPrefs.SetInt(SpaceShip_Store_Key, 1);
            SpaceShip_Price_Text.color = Positive_Money_Gain;
            Current_Money_System(-SpaceShip_Price);
            SpaceShip_Select_Screen.Reset_All_SpaceShip();
        }
        else
        {
            PlayerPrefs.SetInt(SpaceShip_Store_Key, 0);
            SpaceShip_Price_Text.color = Negative_Money_Gain;
        }
    }

    private void SpaceShip_Price_Setter(string SpaceShip_Store_Key,TextMeshProUGUI SpaceShip_Price_Text,float SpaceShip_Price )
    {
        switch (PlayerPrefs.GetInt(SpaceShip_Store_Key))
        {
            case 0:
                SpaceShip_Price_Text.color = Lighter;
                break;
            case 1:
                SpaceShip_Price_Text.color = Positive_Money_Gain;
                break;

            default:
                SpaceShip_Price_Text.color = Lighter;
                break;
        }

        SpaceShip_Price_Text.text = MoneyNotation.Money_Notate_Function(SpaceShip_Price);

    }
    #endregion

}
