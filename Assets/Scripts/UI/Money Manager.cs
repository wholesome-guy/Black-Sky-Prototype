using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI Money_Amount;
    [SerializeField] private TextMeshProUGUI Change_Amount_Text;
    [SerializeField] private CanvasGroup Canvas_Group_Change_Text;

    private float Current_Money;
    private float Changed_Money;
    private string Player_Money_Key = "Current_Player_Money";


    public static Action<float> Money_Change_Event;
    public static Action Money_Saved;

    public static bool Money_Spent = false;

    private WaitForSeconds WaitForSeconds_10 = new WaitForSeconds(10f);
    private WaitForSeconds WaitForSeconds_1 = new WaitForSeconds(1f);

    private void OnEnable()
    {
        Money_Change_Event += Change_Amount;
        Money_Saved += Save_Money_Value;
    }

    private void OnDisable()
    {
        Money_Change_Event -= Change_Amount;
        Money_Saved -= Save_Money_Value;

    }
    // Start is called before the first frame update
    void Start()
    {
        Current_Money = PlayerPrefs.GetFloat(Player_Money_Key);
        Money_Amount.text = MoneyNotation.Money_Notate_Function(Current_Money);
        Change_Amount_Text.gameObject.SetActive(false);
        Canvas_Group_Change_Text.alpha = 0;
    }

    private void Change_Amount(float Change)
    {
        if(Money_Spent)
        {
            return;
        }
        if (Change > 0)
        {
            Change_Amount_Text.color = Color.green;
        }
        else
        { 
            Change_Amount_Text.color = Color.red;
        }

        Money_Spent = true; 
        Change_Amount_Text.gameObject.SetActive(true);
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Canvas_Group_Change_Text,0,1,2f);
        Change_Amount_Text.transform.DOLocalMoveY(100f, 3f);

        Changed_Money = Current_Money + Change;
        StartCoroutine(Money_Lerp(Current_Money,Changed_Money,2.5f,Money_Amount));

        Change_Amount_Text.text = MoneyNotation.Money_Notate_Function(Change);
        Current_Money += Change;
        if(Change < 0)
        {
            Save_Money_Value();
        }
        Money_Amount.text = MoneyNotation.Money_Notate_Function(Current_Money);



    }

    private IEnumerator Money_Lerp(float Start,float End,float Duration, TextMeshProUGUI Text)
    {
        float Time_Elapsed = 0;
        while (Time_Elapsed < Duration)
        {
            Time_Elapsed += Time.deltaTime;

            float value = Mathf.Lerp(Start, End, Time_Elapsed/Duration);
            Text.SetText("{0}", Mathf.RoundToInt(value));
            yield return null;
        }
        Text.text = MoneyNotation.Money_Notate_Function(End);
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Canvas_Group_Change_Text, 1, 0, 0.5f);

        yield return WaitForSeconds_1;
        Change_Amount_Text.gameObject.SetActive(false);
        Change_Amount_Text.transform.localPosition = Vector3.zero;
        Canvas_Group_Change_Text.alpha = 0;

        yield return WaitForSeconds_10;
        Money_Spent = false;
    }

    private void Save_Money_Value()
    {
        PlayerPrefs.SetFloat(Player_Money_Key, Current_Money);
    }
}
