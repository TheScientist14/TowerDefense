using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentButtonBehaviour : MonoBehaviour
{
    public GameObject agent;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SelectTurret);
    }

    public void SelectTurret()
    {
        Selection.SelectTurret(agent);
    }
}
