using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoanManager : MonoBehaviour
{

    public static LoanManager instance;
    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }




    [SerializeField] private float Interest_Rate = 0.20f;
    private float Loan_Amount;
    private string Taken_Loan_key = "Taken_Loan_Amount";

    private void Update()
    {
        Loan_Interest();
    }

    private void Loan_Interest()
    {
        Loan_Amount = PlayerPrefs.GetFloat(Taken_Loan_key);

        if (Loan_Amount == 0)
        {
            return;
        }
        float Interest_Per_Second = Interest_Rate / 3600;

        Loan_Amount += Loan_Amount * Interest_Per_Second * Time.deltaTime;

        PlayerPrefs.SetFloat(Taken_Loan_key, Loan_Amount);
    }
}
