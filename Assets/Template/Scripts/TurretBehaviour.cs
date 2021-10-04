using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public GameObject bullet;

    private float fireRate; // bullet per second
    private float damageValue; // of the bullets fired
    private float range; // of detection of enemies

    //testing
    private Vector3 rotationSpeed = new Vector3(0, 45, 0);
    private float time = 0;
    private GameObject bulletFired;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 2f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotationSpeed * time);
        if(timer <= 0)
        {
            bulletFired = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            Destroy(bulletFired, 5f);
            timer = 1 / fireRate;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
