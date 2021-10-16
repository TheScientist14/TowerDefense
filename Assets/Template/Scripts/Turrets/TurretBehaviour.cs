using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretBehaviour : MonoBehaviour
{
    public string turretName;
    public GameObject weapon;
    public GameObject canon;
    public GameObject bullet;
    public TurretScriptableObject[] turretStat;
    public int level;
    public AnimationClip firingAnimation;

    private float timer;

    private GameObject target; // target to fire on
    private NavMeshAgent targetNavMeshAgent;
    private CapsuleCollider trigger;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        trigger = gameObject.GetComponent<CapsuleCollider>();
        animator = gameObject.GetComponent<Animator>();
        level = 0;
        trigger.radius = GetRange();
        float ratio = (GetFireRate() * firingAnimation.length);
        animator.SetFloat("SpeedFiring", ratio);
    }

    // TODO improve bullet spawn

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            double dist = (canon.transform.position - target.transform.position).magnitude;
            double bulletSpeed = GetBulletSpeed();
            double timeBullet = dist / bulletSpeed;
            Vector3 targetPos = target.transform.position + ((float)timeBullet * targetNavMeshAgent.speed) * target.transform.forward;
            transform.LookAt(targetPos);
            Vector3 rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, rot.y, 0);
            weapon.transform.rotation = Quaternion.Euler(rot.x, rot.y, 0);
        }
        else
        {
            animator.SetBool("Firing", false);
        }
        if(timer <= 0f && target != null)
        {
            GameObject firedbullet = Instantiate(bullet, canon.transform.position, canon.transform.rotation, GameManagement.instance.bulletsContainer.transform);
            BulletComponent firedBulletBehaviour = firedbullet.GetComponent<BulletComponent>();
            firedBulletBehaviour.SetDamageValue(GetDamage());
            firedBulletBehaviour.SetBulletSpeed(GetBulletSpeed());
            firedBulletBehaviour.CanGoThrough(GetBulletPenetration());
            animator.SetBool("Firing", true);
            timer = 1f / GetFireRate();
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
            trigger.radius = GetRange();
            float ratio = (GetFireRate() * firingAnimation.length);
            animator.SetFloat("SpeedFiring", ratio);
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

    /*
     * Trivial getters
     */
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

    public int GetBulletPenetration()
    {
        return turretStat[level].bulletPenetration;
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
