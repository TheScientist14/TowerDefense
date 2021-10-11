using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public float speed;
    private int accelerationTimer = 1;
    private int damageValue = 1;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(accelerationTimer > 0)
        {
            rb.AddForce(transform.forward * speed * 50f, ForceMode.Acceleration);
            // 50f to get the same speed value as in the variable
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
    public void SetBulletSpeed(int speed)
    {
        this.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehaviour>().GetDamage(damageValue);
            Destroy(gameObject);
        }
    }
}
