using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCaseBehaviour : MonoBehaviour
{
    public GameObject uiStats;
    private GUI_StatsBehaviour uiStatsBehaviour;

    private GameObject currentTurret;
    private TurretBehaviour currentTurretBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        uiStatsBehaviour = uiStats.GetComponent<GUI_StatsBehaviour>();
        uiStatsBehaviour.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnMouseDown()
    {
        if (Selection.instance.GetSelectedTurret() != null)
        {
            if (currentTurret == null)
            {
                BuySelectedTurret();
            }
            else
            {
                uiStats.SetActive(true);
            }
        }
        else if(currentTurret != null)
        {
            uiStats.SetActive(true);
        }
    }

    public void SellCurrentTurret()
    {
        if (currentTurret != null)
        {
            GameManagement.AddMoney(currentTurretBehaviour.GetSellPrice());
            uiStatsBehaviour.Hide();
            Destroy(currentTurret);
            TextManagement.instance.UpdateMoneyText();
        }
    }

    private void BuySelectedTurret()
    {
        TurretBehaviour selectedTurretBehaviour = Selection.instance.GetSelectedTurretBehaviour();
        if (GameManagement.GetMoney() >= selectedTurretBehaviour.GetPrice())
        {
            currentTurret = Instantiate(Selection.instance.GetSelectedTurret(), transform.position, Quaternion.identity, GameManagement.instance.turretsContainer.transform);
            currentTurretBehaviour = currentTurret.GetComponent<TurretBehaviour>();
            GameManagement.RemoveMoney(selectedTurretBehaviour.GetPrice());
            TextManagement.instance.UpdateMoneyText();
            uiStatsBehaviour.UpdateData();
        }
    }

    public void UpgradeCurrentTurret()
    {
        if (!currentTurretBehaviour.IsFullyUpgraded())
        {
            int price = currentTurretBehaviour.GetUpgradePrice();
            if (GameManagement.GetMoney() >= price)
            {
                currentTurretBehaviour.UpgradeTurret();
                GameManagement.RemoveMoney(price);
                TextManagement.instance.UpdateMoneyText();
                uiStatsBehaviour.UpdateData();
            }
        }
    }

    public GameObject GetCurrentTurret()
    {
        return currentTurret;
    }

    public TurretBehaviour GetCurrentTurretBehaviour()
    {
        return currentTurretBehaviour;
    }

/*    public void ShowGUI(bool show)
    {
        if (show)
        {
            uiStats.SetActive(true);
        }
    }*/

/*    private void OnMouseOver()
    {
        overing = true;
    }

    private void OnMouseExit()
    {
        overing = false;
    }*/
}
