using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera management
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.z >= -2)
        {
            transform.position += (Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.z <= 58)
        {
            transform.position += (Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x >= -10)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x < 10)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
