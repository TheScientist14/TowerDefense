using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBahaviour : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(GameManagement.instance.LoadNextScene);   
        exitButton.onClick.AddListener(GameManagement.instance.Exit);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
