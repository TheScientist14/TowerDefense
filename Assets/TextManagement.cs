using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManagement : MonoBehaviour
{
    public TextMeshProUGUI TextMesh;
    
    private void Awake()
    {
        UpdateLifeText();
        UpdateMoneyText();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLifeText()
    {
        TextMesh.SetText("Life : " + GameManagement.GetPlayerHealth());
    }

    public void UpdateMoneyText()
    {
        TextMesh.SetText("Money : " + GameManagement.GetMoney());
    }
}
