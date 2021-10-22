using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public GameObject bulletsContainer;
    public GameObject turretsContainer;
    public GameObject buttonReady;

    public static GameManagement instance;

    public UnityEvent startWaveEvent;
    private UnityEvent stopWaveEvent;
    private UnityEvent endGameEvent;
    public UnityEvent moneyAmountChangedEvent;
    // public UnityEvent lifeChangedEvent;
    private int PlayerHealth;
    private int PlayerHealthMax;
    private int Money;
    private bool GameStarted;
    private bool WaveReady;
    private int EnemyLeft;
    private int lvl; // level of the wave in the current level
    private int waveNb; // level of the wave in the current level

    private void Awake()
    {
        startWaveEvent = new UnityEvent();
        stopWaveEvent = new UnityEvent();
        endGameEvent = new UnityEvent();
        moneyAmountChangedEvent = new UnityEvent();
        PlayerHealthMax = 6;
		// Singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            instance.bulletsContainer = bulletsContainer;
            instance.turretsContainer = turretsContainer;
            instance.buttonReady = buttonReady;
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetEventListener();
        stopWaveEvent.Invoke();
        PlayerHealth = PlayerHealthMax;
        Money = 50;
        GameStarted = false;
        lvl = 1;
        waveNb = 0;
        EnemyLeft = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStarted)
        {
            IsWaveFinished();
            IsLoose();
        }
    }

    public void GetDamage(int damage)
    {
        PlayerHealth -= damage;
    }

    public int GetPlayerHealth()
    {
        return PlayerHealth;
    }
    
    public int GetPlayerHealthMax()
    {
        return PlayerHealthMax;
    }

    public void AddMoney(int gain)
    {
        Money += gain;
        moneyAmountChangedEvent.Invoke();
    }

    public void RemoveMoney(int remove)
    {
        Money -= remove;
        moneyAmountChangedEvent.Invoke();
    }

    public int GetMoney()
    {
        return Money;
    }

    public void SetMoney(int moneyToSet)
    {
        Money = moneyToSet;
        moneyAmountChangedEvent.Invoke();
    }

    public void StartGame()
    {
        GameStarted = true;
    }
    
    public void StartWave()
    {
        WaveReady = true;
    }
    
    public void StopGame()
    {
        GameStarted = false;
    }
    
    public void StopWave()
    {
        WaveReady = false;
        startWaveEvent.Invoke();
    }

    public bool IsGameStarted()
    {
        return GameStarted;
    }
    public bool IsWaveReady()
    {
        return WaveReady;
    }

    public void EnemyDie()
    {
        EnemyLeft--;
    }

    public void setEnemyLeft(int enemy)
    {
        EnemyLeft = enemy;
    }

    public bool IsWaveFinished()
    {
        if (EnemyLeft == 0)
        {
            stopWaveEvent.Invoke();
            setEnemyLeft(-1);
            NextWave();
            return true;
        }
        return false;
    }

    public int GetCurrentLvl()
    {
        return waveNb;
    }
    
    public bool IsWin()
    {
        if (PlayerHealth <= 0 && waveNb != 4)
        {
            GameStarted = false;
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

    private void NextWave()
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
    public void LoadNextScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(activeSceneIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            LoadMenuScene();
        }
        else
        {
            SceneManager.LoadScene(activeSceneIndex + 1);
        }
    }

    public void LoadMenuScene()
    {
        PlayerHealth = PlayerHealthMax;
        lvl = 1;
        waveNb = 0;
        SetMoney(60);
        SceneManager.LoadScene(0);
    }

    public void LoadSceneLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    
    public void LoadSceneEndGame()
    {
        SceneManager.LoadScene("EndGame");
    } 

    // TODO save progression
    public void Exit()
    {
        Application.Quit();
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

