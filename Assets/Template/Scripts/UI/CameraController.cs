using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float speed;
    private Vector3 previousPos = Vector3.negativeInfinity;

    // Start is called before the first frame update
    void Start()
    {
        speed = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        // moving camera with arrow keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            position += (Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position += (Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position += (Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position += (Vector3.right * speed * Time.deltaTime);
        }

        // right click dragging camera
        if (Input.GetMouseButtonDown(1))
        {
            previousPos = Input.mousePosition;
        }
        if (!previousPos.Equals(Vector3.negativeInfinity))
        {
            Vector3 currentPos = Input.mousePosition;
            Vector3 delta = (currentPos - previousPos);
            delta.z = delta.y;
            delta.y = 0;
            position += (delta * 0.05f);
            previousPos = currentPos;
        }
        if (Input.GetMouseButtonUp(1))
        {
            previousPos = Vector3.negativeInfinity;
        }

        // limiting camera area
        if (position.z < -2)
        {
            position.z = -2;
        }
        if (position.z > 58)
        {
            position.z = 58;
        }
        if (position.x < -10)
        {
            position.x = -10;
        }
        if (position.x > 10)
        {
            position.x = 10;
        }
        transform.position = position;
    }
}
