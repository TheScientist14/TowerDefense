using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public GameObject bulletsContainer;
    public GameObject turretsContainer;
    public GameObject bank;

    public static GameManagement instance;

    private static int PlayerHealth = 100;
    private static int Money = 50;
    private static bool GameStarted = false;
    private static int EnemyLeft;
    private static int lvl;

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
        lvl = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWaveFinished() || !IsWin())
        {
            if (lvl < 4)
            {
                lvl++;
                GetComponent<BankBehaviour>().NextWave();
            }
            else
            {
                Debug.Log("Fin de la partie, Vous avez gagné !!");
            }
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

    public static void StartGame()
    {
        GameStarted = true;
    }

    public static bool IsGameStarted()
    {
        return GameStarted;
    }

    public static void EnemyDie()
    {
        EnemyLeft--;
    }

    public static void setEnemyLeft(int enemy)
    {
        EnemyLeft = enemy;
    }

    public static bool IsWaveFinished()
    {
        if (EnemyLeft == 0)
        {
            return IsWin();
        }
        return false;
    }

    public static int GetCurrentLvl()
    {
        return lvl;
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

