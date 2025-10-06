using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MoneyNotation : MonoBehaviour
{
    private static float Thousand = 1000;
    private static float Million = 1000000;
    private static float Billion = 1000000000;
    private static float Trillion = 1000000000000;

    public static string Money_Notate_Function(float Amount)
    {
        if(Amount < Thousand)
        {
            return Amount.ToString("F1");
        }
        else if(Amount < Million)
        {
            string K_String = ((Amount / Thousand).ToString("F1")) + "K";
            return K_String;
        }
        else if (Amount < Billion)
        {
            string M_String = ((Amount / Million).ToString("F1")) + "M";
            return M_String;
        }
        else if (Amount < Trillion)
        {
            string B_String = ((Amount / Billion).ToString("F1")) + "B";
            return B_String;
        }
        else if (Amount > Trillion)
        {
            string B_String = ((Amount / Trillion).ToString("F1")) + "T";
            return B_String;
        }

        return "TooBig";
    }
}
