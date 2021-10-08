using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ThiefBehaviour : MonoBehaviour

{
    public GameObject helicopter;
    private NavMeshAgent agent;
    private int health;
    private int healthMax;
    private int damage;
    public Slider healthBar;
    public Canvas canva;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        damage = 1;
        health = 5;
        healthMax = health;
        helicopter = GameObject.Find("Helicopter");
        agent = GetComponent<NavMeshAgent>();
        agent.destination = helicopter.transform.position;
        healthBar.maxValue = health;
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        /*Vector3 direction = transform.position - Camera.main.transform.position; // calcule la direction
        Quaternion lookRotation = Quaternion.LookRotation(direction); // calcule la rotation
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 50).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);*/
        //Quaternion rotation = Quaternion.LookRotation(camera.transform.position);
        //healthBar.transform.rotation = Quaternion.Euler(camera.transform.rotation.x, camera.transform.rotation.y, camera.transform.rotation.z);
        canva.transform.LookAt(camera.transform);
        if ( health <= 0)
        {
            GameManagement.AddMoney(5);
            TextManagement.instance.UpdateMoneyText();
            Destroy(gameObject);
        }
    }

    public void GetDamage(int damage)
    {
        health -= damage;

        healthBar.value = health;
    }

    public int PointLose()
    {
        return damage;
    }
}
