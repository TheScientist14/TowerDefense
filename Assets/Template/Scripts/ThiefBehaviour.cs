using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefBehaviour : MonoBehaviour

{
    public GameObject helicopter;
    private NavMeshAgent agent;
    private int health;
    private int healthMax;
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = 1;
        health = 5;
        healthMax = health;
        helicopter = GameObject.Find("Helicopter");
        agent = GetComponent<NavMeshAgent>();
        agent.destination = helicopter.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ( health <= 0)
        {
            GameManagement.AddMoney(5);
            Destroy(gameObject);
        }
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        Debug.Log("Hit health = " + health);
    }

    public int PointLose()
    {
        return damage;
    }
}
