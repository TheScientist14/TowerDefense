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
    public WaveScriptableObject wave;

    //testing
    private int EnemyMax;
    private int NbEnemy;
    private int NbEnemyAll;
    private int NbEnemyThief;
    private int NbEnemyCapo;
    private float time;
    private bool isWaveReady;

    private float timer;
    private object[] parametersThief;
    private object[] parametersCapo;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        timer = 0;
        
        NbEnemy = 0;
        parametersThief = new object[3];
        parametersCapo = new object[3];
        isWaveReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.IsGameStarted() && isWaveReady)
        {
            GameManagement.setEnemyLeft(wave.nbEmenyTief + wave.nbEmenyCapo);
            if (wave.nbEmenyTief > 0) // if to check if in the wave we have this type of enemy
            {
                Debug.Log("SpawnEnemy Thief");
                parametersThief[0] = thief;
                parametersThief[1] = wave.spawnRateThief;
                parametersThief[2] = wave.nbEmenyTief;
                StartCoroutine("spawn", parametersThief);
            }
            if (wave.nbEmenyCapo > 0)
            {
                Debug.Log("SpawnEnemy Capo");
                parametersCapo[0] = capo;
                parametersCapo[1] = wave.spawnRateCapo;
                parametersCapo[2] = wave.nbEmenyCapo;
                StartCoroutine("spawn", parametersCapo);
            }

            isWaveReady = false;
        }
    }

    IEnumerator spawn(object[] parameters)
    {
        for (int i = 0; i < (int)parameters[2]; i++)
        {
            Instantiate((GameObject)parameters[0], transform.position + new Vector3(0, 0, 6), Quaternion.identity);
            Debug.Log("Spawn : " + (GameObject)parameters[0]);            
            yield return new WaitForSeconds(1/(float)parameters[1]);
        }
    }

    public void NextWave()
    {
        //wave = (WaveScriptableObject) File.Open("lvl1wave" + GameManagement.GetCurrentLvl() + "", FileMode.Open);
    }
}
