using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public GameObject bullet;
    public GameObject enemy;

    private float fireRate; // bullet per second
    private float damageValue; // of the bullets fired
    private float range; // of detection of enemies
    private GameObject target; // target to fire on

    private CapsuleCollider trigger;

    //testing
    private GameObject bulletFired;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 2f;
        timer = 0;
        trigger = GetComponent<CapsuleCollider>();
        trigger.radius = range;
    }

    // Update is called once per frame
    void Update()
    {   
        if(timer <= 0 && target != null)
        {
            transform.LookAt(target.transform);
            bulletFired = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            Destroy(bulletFired, 5f);
            timer = 1 / fireRate;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    /*
     * Target the first enemy within the reach of the turret,
     * stop targeting if out of range and target any enemy in range
     */
    void OnTriggerEnter(Collider target)
    {
        if(this.target != null)
        {
            TargetCollider(target);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other == target)
        {
            target = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(target == null)
        {
            TargetCollider(other);
        }
    }

    private void TargetCollider(Collider target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            this.target = target.gameObject;
        }
    }
}
