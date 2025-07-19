using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidData
{
    public static float Asteroid_MotionValue(float Asteroid_Mass)
    {
        return Asteroid_Mass * 2;
    }
    public static float Asteroid_Tether_PullForce(float Asteroid_Mass)
    {
        return Asteroid_Mass / 2;
    }

    public static float Minimum_Distance_Player_Asteroid = 50f;
    public static float Maximum_Distance_Player_Asteroid = 300f;

    public static float Minimum_Tether_Break_Player_Velocity = 150f;
    public static float Maximum_Tether_Break_Player_Velocity = 240f;

    public static float Break_Velocity_Curve_Exponent = 0.3f;

    // max speed 245 times constant = 1000 Mega newtons. Constant = 1000/245
    public static float Fake_Tension_Velocity_Constant = 4.082f;
    public static float Joint_Break_Velocity_Player(float Asteroid_Mass)
    {
        // Formula used: Velocity_Constant = 820.51 * ln(Mass) - 3879.5
        // The values were found using desmos, follow this link https://www.desmos.com/calculator/gfdzqbwux7
        float Safe_Mass = Mathf.Max(1f, Asteroid_Mass);
        float Velocity_Constant =Mathf.Clamp((820.51f * Mathf.Log(Safe_Mass)) - (3879.5f), 0 , 6000);
        return Mathf.Clamp(Velocity_Constant / Mathf.Pow(Asteroid_Mass, Break_Velocity_Curve_Exponent), Minimum_Tether_Break_Player_Velocity, Maximum_Tether_Break_Player_Velocity);

    }

    public static float Maximum_Kinetic_Energy_Constant = 100f;

    public static float Kinetic_Energy(float Asteroid_Mass, Rigidbody Asteroid_Rigidbody,Rigidbody Player_Rigidbody)
    {
        float Unclamped_Kinetic_Energy = (0.5f) * (Asteroid_Mass) * Mathf.Pow(Asteroid_Rigidbody.velocity.magnitude - Player_Rigidbody.velocity.magnitude, 2);

        float Maximum_Kinetic_Energy = Asteroid_Mass * Maximum_Kinetic_Energy_Constant;

        return Mathf.Clamp(Unclamped_Kinetic_Energy, 0, Maximum_Kinetic_Energy);
    }

    public static float Joint_Break_Delay_Duration = 2.0f;
    public static float Warning_Velocity_Difference_Constant = 30f;

    public static string Right_Sticking_Anchor = "Sticking Anchor Prefab Right (Master)(Clone)";
    public static string Left_Sticking_Anchor = "Sticking Anchor Prefab Left(Clone)";


    public static string Joint_Break = "Anchor's Broken Captain";
    public static string Approaching_Max_Tether_Velocity = "Slow Down Captain, Tether's Snapping";
}
