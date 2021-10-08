using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    private static int PlayerHealth = 100;
    private static int Money = 50;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 100;
        Money = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if ( PlayerHealth <= 0)
        {
            Debug.Log("Game Over !!");
        }
    }

    public static void GetDamage(int damage)
    {
        PlayerHealth -= damage;
    }

    public static int GetPlayerHealth()
    {
        return PlayerHealth;
    }

    public static void AddMoney(int gain)
    {
        Money += gain;
    }

    public static void RemoveMoney(int remove)
    {
        Money -= remove;
    }

    public static int GetMoney()
    {
        return Money;
    }

    public static void SetMoney(int moneyToSet)
    {
        Money = moneyToSet;
    }
}

