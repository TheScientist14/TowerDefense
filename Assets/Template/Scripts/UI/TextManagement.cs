using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextManagement : MonoBehaviour
{
    public static TextManagement instance;
    public Slider PlayerHealth;
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
        PlayerHealth.maxValue = GameManagement.instance.GetPlayerHealthMax();
        PlayerHealth.value = GameManagement.instance.GetPlayerHealth();
    }

    public void UpdateMoneyText()
    {
        MoneyText.SetText("Money : " + GameManagement.instance.GetMoney());
    }

    public void UpdateWaveText()
    {
        WaveText.SetText("Wave : " + (GameManagement.instance.GetCurrentLvl() + 1));
    }
}
