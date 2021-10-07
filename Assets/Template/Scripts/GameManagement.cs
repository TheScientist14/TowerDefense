using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{

    private int PlayerHealth;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( PlayerHealth <= 0)
        {
            Debug.Log("Game Over !!");
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
}

