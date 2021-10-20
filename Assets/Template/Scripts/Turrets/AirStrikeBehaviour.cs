using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrikeBehaviour : MonoBehaviour
{
    public GameObject explosionFX;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.down * 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 7.5f);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Enemy")){
                    colliders[i].gameObject.GetComponent<EnemyBehaviour>().GetDamage(5);
                }
            }
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
