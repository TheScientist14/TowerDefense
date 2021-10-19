using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BankBehaviour : MonoBehaviour
{
    public GameObject thief;
    public GameObject capo;

    private float spawnRateThief; // thief per second
    private float spawnRateCapo; // Capo per second
    public WaveScriptableObject[] waves;

    //testing
    private int EnemyMax;
    private int NbEnemyAll;
    private int NbEnemyThief;
    private int NbEnemyCapo;

    private object[] parametersThief;
    private object[] parametersCapo;

    // Start is called before the first frame update
    void Start()
    {
        parametersThief = new object[3];
        parametersCapo = new object[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.IsGameStarted() && GameManagement.IsWaveReady())
        {
            GameManagement.setEnemyLeft(waves[GameManagement.GetCurrentLvl()].nbEmenyTief + waves[GameManagement.GetCurrentLvl()].nbEmenyCapo);
            if (waves[GameManagement.GetCurrentLvl()].nbEmenyTief > 0) // if to check if in the wave we have this type of enemy
            {
                Debug.Log("SpawnEnemy Thief");
                parametersThief[0] = thief;
                parametersThief[1] = waves[GameManagement.GetCurrentLvl()].spawnRateThief;
                parametersThief[2] = waves[GameManagement.GetCurrentLvl()].nbEmenyTief;
                StartCoroutine("spawn", parametersThief);
            }
            if (waves[GameManagement.GetCurrentLvl()].nbEmenyCapo > 0)
            {
                Debug.Log("SpawnEnemy Capo");
                parametersCapo[0] = capo;
                parametersCapo[1] = waves[GameManagement.GetCurrentLvl()].spawnRateCapo;
                parametersCapo[2] = waves[GameManagement.GetCurrentLvl()].nbEmenyCapo;
                StartCoroutine("spawn", parametersCapo);
            }
            GameManagement.StopGame();
            GameManagement.StopWave();
        }
    }

    IEnumerator spawn(object[] parameters)
    {
        for (int i = 0; i < (int)parameters[2]; i++)
        {
            Instantiate((GameObject)parameters[0], transform.position + transform.forward*6, Quaternion.identity);
            Debug.Log("Spawn : " + (GameObject)parameters[0]);            
            yield return new WaitForSeconds(1/(float)parameters[1]);
        }
    }
}
