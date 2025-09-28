using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class VelocityIndicatorManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Velocity_Text;
    private Rigidbody Player_Rigidbody;
    private float Player_Velocity;
    private StringBuilder sb = new StringBuilder();
    void Start()
    {
        Player_Rigidbody = PlayerSingleton.instance.Player_Rigidbody;
    }

    // Update is called once per frame
    void Update()
    {
        Velocity_Indicator();
    }

    private void Velocity_Indicator()
    {
        Player_Velocity = Mathf.RoundToInt(Player_Rigidbody.velocity.magnitude);

        sb.Clear();
        sb.Append(Player_Velocity);
        sb.Append(" m/s");
        Velocity_Text.text = sb.ToString();

    }
}
