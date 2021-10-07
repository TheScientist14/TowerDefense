using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentButtonBehaviour : MonoBehaviour
{
    public GameObject agent;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SelectTurret);
        cam = Camera.main;
    }

    void SelectTurret()
    {
        Debug.Log("Hello");
        GameObject turret = Instantiate(agent, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y)), Quaternion.identity);
    }
}
