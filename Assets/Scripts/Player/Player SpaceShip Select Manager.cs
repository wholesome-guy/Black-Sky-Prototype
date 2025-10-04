using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpaceShipSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Spaceship_Meshes = new GameObject[3];

    [SerializeField] private SpaceShipSelectValue SpaceShip_Select_Value;

    private int SpaceShip_Index;

    private PlayerSingleton Player_Singleton;

    private void Awake()
    {
        Player_Singleton = PlayerSingleton.instance;

        SpaceShip_Index = SpaceShip_Select_Value.SpaceShip_Index;

        Player_Singleton.Space_Ship_Index = SpaceShip_Index;
        Player_Singleton.SpaceShip_Select_Function();

        for (int i = 0; i < 3; i++)
        {
            if(i != SpaceShip_Index)
            {
                Spaceship_Meshes[i].SetActive(false);
            }
            else
            {
                Spaceship_Meshes[i].SetActive(true);

            }
        }
    }
}
