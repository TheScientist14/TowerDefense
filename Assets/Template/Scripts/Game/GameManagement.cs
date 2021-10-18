using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManagement : MonoBehaviour
{
    public GameObject bulletsContainer;
    public GameObject turretsContainer;
    public GameObject buttonReady;
    public GameObject endGameCanva;

    public static GameManagement instance;

    private static UnityEvent startWaveEvent;
    private static UnityEvent stopWaveEvent;
    private static UnityEvent endGameEvent;
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

        startWaveEvent = new UnityEvent();
        stopWaveEvent = new UnityEvent();
        endGameEvent = new UnityEvent();
        
        SetEventListener();
        stopWaveEvent.Invoke();
        PlayerHealth = 100;
        Money = 50;
        GameStarted = false;
        lvl = 0;
        EnemyLeft = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Enemies restants : " + EnemyLeft);
        IsWaveFinished();
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
        startWaveEvent.Invoke();
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
        if (EnemyLeft == 0)
        {
            Debug.Log("Enemy left == 0 wave finished");
            stopWaveEvent.Invoke();
            setEnemyLeft(-1);
            NextWave();
            return true;
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

    private static void NextWave()
    {
        if (lvl < 4)
        {
            lvl++;
            TextManagement.instance.UpdateWaveText();
        }
        else
        {
            Debug.Log("###################### END GAME ######################");
            endGameEvent.Invoke();
        }
    }

    public void SetEventListener()
    {
        startWaveEvent.AddListener(DeactiveReadyButton);
        stopWaveEvent.AddListener(ActiveReadyButton);
        endGameEvent.AddListener(LoadSceneEndGame);
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
    
    public void LoadSceneEndGame()
    {
        SceneManager.LoadScene("EndGame");
    } 

    // TODO save progression
    public static void Exit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ActiveReadyButton()
    {
        buttonReady.SetActive(true);
    }
    
    public void DeactiveReadyButton()
    {
        buttonReady.SetActive(false);
    }

}

