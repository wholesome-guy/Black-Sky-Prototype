using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{

    [SerializeField] private float Beginning_Debt = 0;

    [SerializeField] private TextMeshProUGUI Money_Amount;
    [SerializeField] private TextMeshProUGUI Change_Amount_Text;
    [SerializeField] private CanvasGroup Canvas_Group_Change_Text;

    private float Current_Money;
    private float Changed_Money;

    public static Action<float> Money_Change_Event;

    public static bool Money_Spent = false;

    private WaitForSeconds WaitForSeconds_10 = new WaitForSeconds(10f);
    private WaitForSeconds WaitForSeconds_1 = new WaitForSeconds(1f);

    private void OnEnable()
    {
        Money_Change_Event += Change_Amount;
    }

    private void OnDisable()
    {
        Money_Change_Event -= Change_Amount;
    }
    // Start is called before the first frame update
    void Start()
    {
        Current_Money = Beginning_Debt;
        Money_Amount.text = Current_Money.ToString();
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

        Change_Amount_Text.text = Change.ToString();
        Current_Money += Change;
        Money_Amount.text = Current_Money.ToString();

    }

    private IEnumerator Money_Lerp(float Start,float End,float Duration, TextMeshProUGUI Text)
    {
        float Time_Elapsed = 0;
        while (Time_Elapsed < Duration)
        {
            Time_Elapsed += Time.deltaTime;

            float value = Mathf.Lerp(Start, End, Time_Elapsed/Duration);
            Text.text = Mathf.RoundToInt(value).ToString();
            yield return null;
        }
        Start = End;
        Text.text = Start.ToString();
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Canvas_Group_Change_Text, 1, 0, 0.5f);

        yield return WaitForSeconds_1;
        Change_Amount_Text.gameObject.SetActive(false);
        Change_Amount_Text.transform.localPosition = Vector3.zero;
        Canvas_Group_Change_Text.alpha = 0;

        yield return WaitForSeconds_10;
        Money_Spent = false;
    }
}
