using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretBehaviour : MonoBehaviour
{
    public GameObject canon;
    public GameObject bullet;
    private BulletComponent bulletBehaviour;

    private float fireRate; // bullet per second
    private int damageValue = 2; // of the bullets fired
    private float bulletSpeed; // speed of the bullet
    private float range = 20; // of detection of enemies
    private int price = 10;
    private GameObject target; // target to fire on
    private NavMeshAgent targetNavMeshAgent;

    private CapsuleCollider trigger;

    //testing
    private GameObject bulletFired;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 2f;
        timer = 0f;
        trigger = GetComponent<CapsuleCollider>();
        trigger.radius = range;
        bulletBehaviour = bullet.GetComponent<BulletComponent>();
        //bulletBehaviour.
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            double dist = (canon.transform.position - target.transform.position).magnitude;
            double bulletSpeed = bulletBehaviour.speed;
            double timeBullet = dist / bulletSpeed;
            transform.LookAt(target.transform.position + ((float)timeBullet*targetNavMeshAgent.speed)*target.transform.forward);
        }
        if(timer <= 0f && target != null)
        {
            bulletFired = Instantiate(bullet, canon.transform.position, transform.rotation, GameManagement.instance.bulletsContainer.transform) as GameObject;
            bulletFired.GetComponent<BulletComponent>().SetDamageValue(damageValue);
            Destroy(bulletFired, 5f);
            timer = 1f / fireRate;
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
        TargetCollider(target);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target = null;
        }
    }

    void OnTriggerStay(Collider other)
    {
        TargetCollider(other);
    }

    private void TargetCollider(Collider target)
    {
        if (this.target == null)
        {
            if (target.gameObject.CompareTag("Enemy"))
            {
                this.target = target.gameObject;
                targetNavMeshAgent = target.GetComponent<NavMeshAgent>();
                
            }
        }
    }

    public int GetPrice()
    {
        return price;
    }
}
