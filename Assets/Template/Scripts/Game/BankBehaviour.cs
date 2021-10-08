using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankBehaviour : MonoBehaviour
{
    public GameObject thief;

    private float spawnRate; // thief per second

    //testing
    private int EnemyMax = 20;
    private int NbEnemy = 0;
    private float time = 0;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 1;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.IsGameStarted())
        {
            time += Time.deltaTime;
            if (timer <= 0 && NbEnemy < EnemyMax)
            {
                spawn();
                timer = 1 / spawnRate;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        Debug.Log("Nb Enemy spawn : " + NbEnemy);
    }

    private void spawn()
    {
        Instantiate(thief, transform.position + new Vector3(0, 0, 6), Quaternion.identity);
        NbEnemy++;
    }
}
