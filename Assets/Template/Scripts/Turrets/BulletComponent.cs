using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    private float speed;
    private int accelerationTimer = 1;
    private int damageValue = 1;
    private int canGoThrough = 0;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(accelerationTimer > 0)
        {
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
            accelerationTimer--;
        }
    }

    public void SetDamageValue(int value)
    {
        damageValue = value;
    }

    public int GetDamageValue()
    {
        return damageValue;
    }

    // speed in meter per second
    public void SetBulletSpeed(float speed)
    {
        this.speed = speed;
    }

    public void CanGoThrough(int nb)
    {
        canGoThrough = nb;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehaviour>().GetDamage(damageValue);
            if (canGoThrough <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                canGoThrough--;
            }
        }
    }
}
