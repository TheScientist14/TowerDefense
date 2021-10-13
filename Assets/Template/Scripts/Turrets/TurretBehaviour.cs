using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretBehaviour : MonoBehaviour
{
    public string turretName;
    public GameObject canon;
    public GameObject bullet;
    public TurretScriptableObject[] turretStat;
    public int level = 0;

    private BulletComponent bulletBehaviour;
    private float timer;

    private GameObject target; // target to fire on
    private NavMeshAgent targetNavMeshAgent;
    private CapsuleCollider trigger;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        trigger = GetComponent<CapsuleCollider>();
        trigger.radius = turretStat[level].range;
        bulletBehaviour = bullet.GetComponent<BulletComponent>();
    }

    // TODO improve bullet spawn

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            double dist = (canon.transform.position - target.transform.position).magnitude;
            double bulletSpeed = bulletBehaviour.speed;
            double timeBullet = dist / bulletSpeed;
            Vector3 targetPos = target.transform.position + ((float)timeBullet * targetNavMeshAgent.speed) * target.transform.forward;
            transform.LookAt(targetPos);
            Vector3 rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, rot.y, 0);
            canon.transform.rotation = Quaternion.Euler(rot.x, 0, rot.z);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Firing", false);
        }
        if(timer <= 0f && target != null)
        {
            Instantiate(bullet, canon.transform.position, canon.transform.rotation, GameManagement.instance.bulletsContainer.transform);
            gameObject.GetComponent<Animator>().SetBool("Firing", true);
            timer = 1f / turretStat[level].fireRate;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void UpgradeTurret()
    {
        level++;
        if(level >= turretStat.Length)
        {
            level = turretStat.Length-1;
        }
        else
        {
            this.trigger.radius = turretStat[level].range;
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

    // Trivial getters
    public int GetDamage()
    {
        return turretStat[level].damageValue;
    }

    public float GetRange()
    {
        return turretStat[level].range;
    }

    public float GetFireRate()
    {
        return turretStat[level].fireRate;
    }

    public float GetBulletSpeed()
    {
        return turretStat[level].bulletSpeed;
    }

    public int GetPrice()
    {
        return turretStat[level].price;
    }

    public int GetLevel()
    {
        return level+1;
    }

    public string GetName()
    {
        return turretName;
    }

    public bool IsFullyUpgraded()
    {
        return (level >= turretStat.Length - 1);
    }

    public int GetUpgradePrice()
    {
        if(level < turretStat.Length - 1)
        {
            return turretStat[level + 1].price;
        }
        else
        {
            return 0;
        }
    }

    public int GetSellPrice()
    {
        int price = 0;
        for(int i = 0; i <= level; i++)
        {
            price += turretStat[i].price;
        }
        return price;
    }
}
