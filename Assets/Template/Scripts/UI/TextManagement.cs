using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManagement : MonoBehaviour
{
    public static TextManagement instance;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI WaveText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLifeText();
        UpdateMoneyText();
        UpdateWaveText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLifeText()
    {
        LifeText.SetText("Life : " + GameManagement.GetPlayerHealth());
    }

    public void UpdateMoneyText()
    {
        MoneyText.SetText("Money : " + GameManagement.GetMoney());
    }

    public void UpdateWaveText()
    {
        WaveText.SetText("Wave : " + (GameManagement.GetCurrentLvl() + 1));
    }
}
