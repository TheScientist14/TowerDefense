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
    public GameObject weaponHandleRight;
    public GameObject weaponHandleLeft;
    public float ikWeight = 1f;

    private float timer;

    private GameObject target; // target to fire on
    private NavMeshAgent targetNavMeshAgent;
    private CapsuleCollider trigger;
    private Animator animator;

    private void Awake()
    {
        trigger = gameObject.GetComponent<CapsuleCollider>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
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
            transform.LookAt(target.transform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            double dist = (canon.transform.position - target.transform.position).magnitude;
            double bulletSpeed = GetBulletSpeed();
            double timeBullet = dist / bulletSpeed;
            Vector3 targetPos = target.transform.position + ((float)timeBullet * targetNavMeshAgent.speed) * target.transform.forward;
            weapon.transform.LookAt(targetPos);
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

    void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
        animator.SetIKPosition(AvatarIKGoal.RightHand, weaponHandleRight.transform.position);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);
        animator.SetIKRotation(AvatarIKGoal.RightHand, weaponHandleRight.transform.rotation);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, weaponHandleLeft.transform.position);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, weaponHandleLeft.transform.rotation);
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
            return int.MaxValue;
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
