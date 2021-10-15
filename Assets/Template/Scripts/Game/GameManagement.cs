using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public GameObject bulletsContainer;
    public GameObject turretsContainer;

    public static GameManagement instance;

    private static int PlayerHealth;
    private static int Money;
    private static bool GameStarted;
    private static bool WaveReady;
    private static int EnemyLeft;
    private static int lvl; // level of the wave in the current level

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
        lvl = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if ((IsWaveFinished() || !IsWin()) && IsGameStarted())
        {
            if (lvl < 4)
            {
                Debug.Log("New Wave !! " + lvl);
                lvl++;
                TextManagement.instance.UpdateWaveText();
                Debug.Log("New Wave !! " + lvl);
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
    
    public static void StartWave()
    {
        WaveReady = true;
    }
    
    public static void StopGame()
    {
        GameStarted = false;
    }
    
    public static void StopWave()
    {
        WaveReady = false;
    }

    public static bool IsGameStarted()
    {
        return GameStarted;
    }
    public static bool IsWaveReady()
    {
        return WaveReady;
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
        if (EnemyLeft == 0 && IsGameStarted())
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
        if (PlayerHealth <= 0 && lvl != 4)
        {
            Debug.Log("Game Over !!");
            return false;
        }
        return true;
    }

    /*
     * Scene managing
     */
    public static void LoadNextScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(activeSceneIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            LoadMenuScene();
        }
        else
        {
            Debug.Log("cc");
            SceneManager.LoadScene(activeSceneIndex + 1);
        }
    }

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadSceneLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    // TODO save progression
    public static void Exit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}

