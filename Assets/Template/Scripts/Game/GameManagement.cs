using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public GameObject bulletsContainer;
    public GameObject turretsContainer;

    public static GameManagement instance;

    private static int PlayerHealth = 100;
    private static int Money = 50;
    private static bool GameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            instance.bulletsContainer = bulletsContainer;
            instance.turretsContainer = turretsContainer;
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        PlayerHealth = 100;
        Money = 50;
        GameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        IsWin();
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

    public static void StartGame()
    {
        GameStarted = true;
    }

    public static bool IsGameStarted()
    {
        return GameStarted;
    }

    public static bool IsWin()
    {
        if (PlayerHealth <= 0)
        {
            Debug.Log("Game Over !!");
            return false;
        }
        return true;
    }
}

