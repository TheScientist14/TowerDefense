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

    public static GameManagement instance;

    private static UnityEvent startWaveEvent;
    private static UnityEvent stopWaveEvent;
    private static UnityEvent endGameEvent;
    public static UnityEvent moneyAmountChangedEvent;
    // public static UnityEvent lifeChangedEvent;
    private static int PlayerHealth;
    private static int Money;
    private static bool GameStarted;
    private static bool WaveReady;
    private static int EnemyLeft;
    private static int lvl; // level of the wave in the current level
    private static int waveNb; // level of the wave in the current level

    private void Awake()
    {
        startWaveEvent = new UnityEvent();
        stopWaveEvent = new UnityEvent();
        endGameEvent = new UnityEvent();
        moneyAmountChangedEvent = new UnityEvent();
    }

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
        
        SetEventListener();
        stopWaveEvent.Invoke();
        PlayerHealth = 10;
        Money = 50;
        GameStarted = false;
        lvl = 1;
        waveNb = 0;
        EnemyLeft = -1;
    }

    // Update is called once per frame
    void Update()
    {
        IsWaveFinished();
        IsLoose();
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
        moneyAmountChangedEvent.Invoke();
    }

    public static void RemoveMoney(int remove)
    {
        Money -= remove;
        moneyAmountChangedEvent.Invoke();
    }

    public static int GetMoney()
    {
        return Money;
    }

    public static void SetMoney(int moneyToSet)
    {
        Money = moneyToSet;
        moneyAmountChangedEvent.Invoke();
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
        return waveNb;
    }
    
    public static bool IsWin()
    {
        if (PlayerHealth <= 0 && waveNb != 4)
        {
            Debug.Log("Game Over !!");
            return false;
        }
        return true;
    }

    public void IsLoose()
    {
        if (PlayerHealth <=0)
        {
            endGameEvent.Invoke();
        }
    }

    private static void NextWave()
    {
        if (waveNb < 4)
        {
            waveNb++;
            TextManagement.instance.UpdateWaveText();
        }
        else if (IsWin() && lvl != 2)
        {
            waveNb = 0;
            SetMoney(60);
            TextManagement.instance.UpdateMoneyText();
            TextManagement.instance.UpdateWaveText();
            LoadNextScene();
            lvl++;
        }
        else
        {
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
        if (buttonReady != null)
        {
            buttonReady.SetActive(true);
        }
    }
    
    public void DeactiveReadyButton()
    {
        if (buttonReady != null)
        {
            buttonReady.SetActive(false);
        }
    }

}

