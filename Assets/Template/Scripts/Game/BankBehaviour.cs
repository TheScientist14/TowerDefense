using System;
using System.Collections;
using System.Collections.Generic;
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
    private object[] parametersCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        timer = 0;
        
        NbEnemy = 0;
        parametersCoroutine = new object[3];
        isWaveReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.IsGameStarted() && isWaveReady)
        {
            if (wave.nbEmenyTief > 0) // if to check if in the wave we have this type of enemy
            {
                spawnEnemy(thief, wave.spawnRateThief, wave.nbEmenyTief);
            }
            if (wave.nbEmenyCapo > 0)
            {
                spawnEnemy(capo, wave.spawnRateCapo, wave.nbEmenyCapo);
            }

            isWaveReady = false;
        }
    }

    private void spawnEnemy(GameObject typeEnemy, float spawnRate, int max)
    {
        if (NbEnemyAll < (wave.nbEmenyTief + wave.nbEmenyCapo))
        {
            parametersCoroutine[0] = typeEnemy;
            parametersCoroutine[1] = spawnRate;
            parametersCoroutine[2] = max;
            
            StartCoroutine("spawn", parametersCoroutine);
        }

    }

    IEnumerator spawn(object[] parameters)
    {
        for (int i = 0; i < (int)parameters[2]; i++)
        {
            Instantiate((GameObject)parameters[0], transform.position + new Vector3(0, 0, 6), Quaternion.identity);
            Debug.Log("Spawn : " + (GameObject)parameters[0]);            
            yield return new WaitForSeconds(1f/(float)parameters[1]);
        }
    }
}
