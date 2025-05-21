using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Spaceship_Health_System : MonoBehaviour
{
    private float Max_Health;
    private float Current_Health;
    private float Ratio_Of_Current_To_Max_Health;

    private Coroutine Health_Fill_Bar_Lerp;
    private float Percentage_Health;
    
    [SerializeField] private GameObject[] Shields_GameObjects;
    [SerializeField] private SpaceShipValues Space_Ship_Values;
    [SerializeField] private Image Health_Fill_Bar;
    [SerializeField] private TextMeshProUGUI Health_Amount_Text;

    public UnityEvent Death;

    private void OnEnable()
    {
        Collision_Manager_SpaceShip.Take_Damage += Shield_Checker;
    }
    private void OnDisable()
    {
        Collision_Manager_SpaceShip.Take_Damage -= Shield_Checker;

    }
    private void Start()
    {
        Max_Health = Space_Ship_Values.Max_Health;
        Current_Health = Max_Health;
        Ratio_Of_Current_To_Max_Health = 1f;
        Health_Fill_Bar.fillAmount = 1;
        Health_Amount_Text.text = "100%";
    }
    


    private void Shield_Checker(float Damage_Amount)
    {
        if (!Is_Any_Shield_Active())
        {
            Reduce_Health(Damage_Amount);
        }
        else
        {
            Shield_Breaker();
        }
    }

    private bool Is_Any_Shield_Active()
    {
        for (int i = 0; i < Shields_GameObjects.Length; i++)
        {
            if (Shields_GameObjects[i].activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }




    private void Reduce_Health(float Damage)
    {
        Current_Health -= Damage;
        Current_Health = Mathf.Clamp(Current_Health, 0, Max_Health);
        Ratio_Of_Current_To_Max_Health = Current_Health / Max_Health;
        Percentage_Health = Mathf.RoundToInt(Ratio_Of_Current_To_Max_Health * 100);

        if(Current_Health == 0f )
        {
            Death.Invoke();
        }
        Update_Health_UI();
        
    }

    private void Update_Health_UI()
    {
        if(Health_Fill_Bar_Lerp != null)
        {
            StopCoroutine(Health_Fill_Bar_Lerp);
        }
        Health_Fill_Bar_Lerp = StartCoroutine(Health_UI_Lerp(Health_Fill_Bar.fillAmount, Ratio_Of_Current_To_Max_Health,0.5f));
        Health_Amount_Text.text = Percentage_Health + "%";
    }

    private void Shield_Breaker()
    {
        for (int i = 0; i < Shields_GameObjects.Length; i++)
        {
            if (Shields_GameObjects[i].activeInHierarchy)
            {
                Shields_GameObjects[i].SetActive(false);
                StartCoroutine(Regenerate_Shield(i));
                break;
            }
        }
    }
    private IEnumerator Regenerate_Shield(int i)
    {
        yield return new WaitForSeconds(10f);
        Shields_GameObjects[i].SetActive(true);
    }
    private IEnumerator Health_UI_Lerp(float start, float target, float duration)
    {
        float time = 0;
       
        while (time < duration)
        {
            time += Time.deltaTime;
          Health_Fill_Bar.fillAmount = Mathf.Lerp(start,target,time/duration);
            yield return null;
        }
        Health_Fill_Bar.fillAmount = target;
        
    }

}
