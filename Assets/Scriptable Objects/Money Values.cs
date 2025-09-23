using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Money Values", menuName = "ScriptableObjects/Money Values")]
public class MoneyValues : ScriptableObject
{
    public int Refuel_Station_Cost;
    public int Docking_Station_Cost;
    public int Docking_Station_Making_Cost;
    public int Cannon_Projectile_Cost;
    public int Anchor_Projectile_Cost;
    public int Destroy_Projectile_Cost;
    public int Data_Projectile_Cost;
    public int Mist_Projectile_Cost;
    public int BlackHole_Projectile_Cost;

}
