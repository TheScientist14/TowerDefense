using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefBehaviour : MonoBehaviour

{
    public GameObject helicopter;


    // Start is called before the first frame update
    void Start()
    {
        helicopter = GameObject.Find("Helicopter");
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = helicopter.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
