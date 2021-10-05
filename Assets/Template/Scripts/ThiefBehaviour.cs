using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefBehaviour : MonoBehaviour

{
    public GameObject helicopter;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        helicopter = GameObject.Find("Helicopter");
        agent = GetComponent<NavMeshAgent>();
        agent.destination = helicopter.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
