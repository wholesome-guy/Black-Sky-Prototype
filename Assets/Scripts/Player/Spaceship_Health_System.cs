using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Spaceship_Health_System : MonoBehaviour
{
    // Health values
    private float Max_Health;
    private float Current_Health;
    private float Ratio_Of_Current_To_Max_Health;

    // Coroutine reference for UI lerp
    private Coroutine Health_Fill_Bar_Lerp;
    private float Percentage_Health;

    [Header("Shield and Health References")]

    [SerializeField] private GameObject[] Shields_GameObjects;      // Array of shield objects attached to the ship

    [SerializeField] private float Shield_Regeneration_Time = 10f;  

    [SerializeField] private SpaceShipValues Space_Ship_Values;     // ScriptableObject or class holding the max health value

    [SerializeField] private Image Health_Fill_Bar;                 // UI image for health fill bar

    [SerializeField] private TextMeshProUGUI Health_Amount_Text;    // Text displaying current health percentage

    public UnityEvent Death;                                        // Event triggered on ship death

    // Subscribe to the damage event
    private void OnEnable()
    {
        Collision_Manager_SpaceShip.Take_Damage += Shield_Checker;
    } 
    
    // Unsubscribe to avoid memory leaks

    private void OnDisable()
    {
        Collision_Manager_SpaceShip.Take_Damage -= Shield_Checker;

    }
      // Initialize health values and UI on start
    private void Start()
    {
        Max_Health = Space_Ship_Values.Max_Health;
        Current_Health = Max_Health;
        Ratio_Of_Current_To_Max_Health = 1f;
        Health_Fill_Bar.fillAmount = 1;
        Health_Amount_Text.text = "100%";
    }



    // Called when the ship takes damage
    private void Shield_Checker(float Damage_Amount)
    {
        // If no shields are active, apply damage
        if (!Is_Any_Shield_Active())
        {
            Reduce_Health(Damage_Amount);
        }
        else
        {
            // Otherwise, break the first active shield
            Shield_Breaker();
        }
    }

    // Checks if any shield is currently active
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

    // Reduces health based on incoming damage
    private void Reduce_Health(float Damage)
    {
        Current_Health -= Damage;
        Current_Health = Mathf.Clamp(Current_Health, 0, Max_Health); // Prevents health from going below 0
        Ratio_Of_Current_To_Max_Health = Current_Health / Max_Health;
        Percentage_Health = Mathf.RoundToInt(Ratio_Of_Current_To_Max_Health * 100);

        // Trigger death event if health reaches 0
        if (Current_Health == 0f)
        {
            Death.Invoke();
        }

        // Update UI after taking damage
        Update_Health_UI();
    }

    // Updates the health bar and percentage text in the UI
    private void Update_Health_UI()
    {
        // Stop previous coroutine if one is running
        if (Health_Fill_Bar_Lerp != null)
        {
            StopCoroutine(Health_Fill_Bar_Lerp);
        }

        // Start a new coroutine to smoothly transition health bar
        Health_Fill_Bar_Lerp = StartCoroutine(Health_UI_Lerp(Health_Fill_Bar.fillAmount, Ratio_Of_Current_To_Max_Health, 0.5f));
        Health_Amount_Text.text = Percentage_Health + "%";
    }

    // Deactivates the first active shield and starts its regeneration
    private void Shield_Breaker()
    {
        for (int i = 0; i < Shields_GameObjects.Length; i++)
        {
            if (Shields_GameObjects[i].activeInHierarchy)
            {
                Shields_GameObjects[i].SetActive(false);
                StartCoroutine(Regenerate_Shield(i));
                break; // Break only one shield at a time
            }
        }
    }

    // Coroutine to regenerate a shield after a delay
    private IEnumerator Regenerate_Shield(int i)
    {
        yield return new WaitForSeconds(Shield_Regeneration_Time);
        Shields_GameObjects[i].SetActive(true);
    }

    // Coroutine to smoothly interpolate the health bar fill amount
    private IEnumerator Health_UI_Lerp(float start, float target, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            Health_Fill_Bar.fillAmount = Mathf.Lerp(start, target, time / duration);
            yield return null;
        }

        Health_Fill_Bar.fillAmount = target;
    }
}
