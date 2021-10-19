using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public Button restartButton;
    public Button exitButton;
    public GameObject textWin;
    public GameObject textLoose;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManagement.instance.IsWin())
        {
            textLoose.SetActive(false);
        }
        else
        {
            textWin.SetActive(false);
        }
        restartButton.onClick.AddListener(GameManagement.instance.LoadMenuScene);
        exitButton.onClick.AddListener(GameManagement.instance.Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
