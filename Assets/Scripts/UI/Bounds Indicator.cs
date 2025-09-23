using System.Collections;
using System.Collections.Generic;
using System.Text; // for StringBuilder
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoundsIndicator : MonoBehaviour
{
    private Transform Player;
    [SerializeField] private RectTransform Bounds_Circle;
    [SerializeField] private Image Bounds_Circle_Image;

    [SerializeField] private float X_Z_Limits;
    [SerializeField] private float Y_Limit;

    private float Positive_Y_Constant;
    private float Negative_Y_Constant;
    private float Y_Constant;

    private float X_Z_Constant;

    private float Positive_Scale_Constant;
    private float Negative_Scale_Constant;
    private float Scale_Constant;

    [SerializeField] private TextMeshProUGUI Coordinates_Text;

    // Cached vectors to avoid per-frame allocations
    private Vector3 shipLocationOnCube;
    private Vector3 boundsCircleScale;

    // StringBuilder for coordinates text
    private StringBuilder coordBuilder = new StringBuilder(20);

    // Optional: limit how often coordinates update
    private float coordUpdateTimer;
    [SerializeField] private float coordUpdateRate = 0.2f;

    private void Start()
    {
        Positive_Y_Constant = 108f / Y_Limit;
        Negative_Y_Constant = 34f / Y_Limit;

        X_Z_Constant = 56f / X_Z_Limits;

        Positive_Scale_Constant = 0.5f / X_Z_Limits;
        Negative_Scale_Constant = 0.2f / X_Z_Limits;

        Player = PlayerSingleton.instance.Player_Transform;

        // Initialize cached vectors
        shipLocationOnCube = Vector3.zero;
        boundsCircleScale = Vector3.one;
    }

    void Update()
    {
        Direction_Constant();
        Bounds_Position();
        Bounds_Scale();

        coordUpdateTimer += Time.deltaTime;
        if (coordUpdateTimer >= coordUpdateRate)
        {
            coordUpdateTimer = 0f;
            Coordinates_Text_Updater();
        }
    }

    private void Direction_Constant()
    {
        Y_Constant = (Player.position.y > 0) ? Positive_Y_Constant : Negative_Y_Constant;
        Scale_Constant = (Player.position.x > 0 || Player.position.z < 0) ? Positive_Scale_Constant : Negative_Scale_Constant;
    }

    private void Bounds_Position()
    {
        shipLocationOnCube.x = (Player.position.x + Player.position.z) * X_Z_Constant;
        shipLocationOnCube.y = Player.position.y * Y_Constant;
        shipLocationOnCube.z = 0f;

        Bounds_Circle.anchoredPosition = shipLocationOnCube;
    }

    private void Bounds_Scale()
    {
        float scale = (Player.position.x - Player.position.z) * Scale_Constant;
        scale = Mathf.Clamp(scale, 0.2f, 0.5f);

        boundsCircleScale.x = scale;
        boundsCircleScale.y = scale;
        boundsCircleScale.z = 1f;

        Bounds_Circle.localScale = boundsCircleScale;
    }

    public void Coordinates_Text_Idle()
    {
        Coordinates_Text.enabled = false;
    }

    public void Coordinates_Text_Hover()
    {
        Coordinates_Text.enabled = true;
    }

    private void Coordinates_Text_Updater()
    {
        int X = Mathf.RoundToInt(Player.position.x);
        int Y = Mathf.RoundToInt(Player.position.y);
        int Z = Mathf.RoundToInt(Player.position.z);

        coordBuilder.Length = 0; // clear without reallocating
        coordBuilder.Append(X);
        coordBuilder.Append(',');
        coordBuilder.Append(Y);
        coordBuilder.Append(',');
        coordBuilder.Append(Z);

        Coordinates_Text.text = coordBuilder.ToString();
    }
}