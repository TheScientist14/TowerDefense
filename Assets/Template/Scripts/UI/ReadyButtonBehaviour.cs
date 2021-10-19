using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(GameManagement.instance.StartGame);
        gameObject.GetComponent<Button>().onClick.AddListener(GameManagement.instance.StartWave);
    }
}
