using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AsteroidInformationManager : MonoBehaviour
{
    [SerializeField] private GameObject Asteroid_Information_Gameobject;
    [SerializeField] private Transform Asteroid_Information_Transform;
    [SerializeField] private CanvasGroup Canvas_Group_Asteroid_Information;

    [SerializeField] private TextMeshProUGUI Mass_Value_Text;

    [SerializeField] private TextMeshProUGUI Sell_Value_Text;

    [SerializeField] private TextMeshProUGUI Composition1_Text;

    [SerializeField] private TextMeshProUGUI Composition2_Text;

    [SerializeField] private TextMeshProUGUI Composition3_Text;


    public static Action<float, float,
        string, float,
        string, float,
        string, float> Asteroid_Information_Event;

    private void Start()
    {
        Canvas_Group_Asteroid_Information.alpha = 0f;
        Asteroid_Information_Gameobject.SetActive(false);
    }
    private void OnEnable()
    {
        Asteroid_Information_Event += Asteroid_Information_Function;
    }
    private void OnDisable()
    {
        Asteroid_Information_Event -= Asteroid_Information_Function;

    }

    private void Asteroid_Information_Function
        (float Mass,float Sell_Value,
        string Composition1,float Composition1_Percentage,
        string Composition2, float Composition2_Percentage,
        string Composition3, float Composition3_Percentage)
    {
        Asteroid_Information_Gameobject.SetActive(true);
        Asteroid_Information_Transform.localScale = Vector3.zero;
        Asteroid_Information_Transform.DOScale(1f, 0.6f);
        UIVisualEffectsManager.UI_Fader_Event.Invoke(Canvas_Group_Asteroid_Information, 0, 1, 0.5f);

        Mass_Value_Text.text = Mass.ToString() + " " + "Kgs";

        Sell_Value_Text.text = Sell_Value.ToString() + " " + "$";

        Composition1_Text.text = Composition1 + " " + Composition1_Percentage.ToString() + "%";
        Composition2_Text.text = Composition2 + " " + Composition2_Percentage.ToString() + "%";
        Composition3_Text.text = Composition3 + " " + Composition3_Percentage.ToString() + "%";

       
        StartCoroutine(Asteroid_Information_Shut_Down());

    }

    private IEnumerator Asteroid_Information_Shut_Down()
    {
        yield return new WaitForSeconds(10f);

        UIVisualEffectsManager.UI_Fader_Event.Invoke(Canvas_Group_Asteroid_Information, 0, 1, 0.5f);
        Asteroid_Information_Transform.DOScale(0f, 1f);

        yield return new WaitForSeconds(2f);

        Asteroid_Information_Gameobject.SetActive(false);
    }




}
