using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        GameObject Enemy = other.gameObject;
        if (Enemy.CompareTag("Enemy"))
        {
            GameManagement.GetDamage(Enemy.GetComponent<EnemyBehaviour>().PointLose());
            TextManagement.instance.UpdateLifeText();
            Destroy(Enemy);
        }
    }
}
