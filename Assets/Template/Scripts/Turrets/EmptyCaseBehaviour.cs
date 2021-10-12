using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCaseBehaviour : MonoBehaviour
{
    public GameObject uiStats;

    private GameObject currentTurret;
    private int currentTurretPrice;

    // Start is called before the first frame update
    void Start()
    {
        uiStats.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnMouseDown()
    {
        GameObject selectedTurret = Selection.GetSelectedTurret();
        int price = Selection.GetSelectedTurretPrice();
        if (selectedTurret != null)
        {
            if (currentTurret == null)
            {
                if (GameManagement.GetMoney() >= price)
                {
                    currentTurret = Instantiate(selectedTurret, transform.position, Quaternion.identity, GameManagement.instance.turretsContainer.transform);
                    currentTurretPrice = price;
                    GameManagement.RemoveMoney(price);
                    TextManagement.instance.UpdateMoneyText();
                }
            }
            else
            {
                uiStats.SetActive(true);
            }
        }
        else
        {
            uiStats.SetActive(true);
        }
    }

    public void SellTurret()
    {
        if (currentTurret != null)
        {
            GameManagement.AddMoney(currentTurretPrice);
            Destroy(currentTurret);
            TextManagement.instance.UpdateMoneyText();
            currentTurretPrice = 0;
        }
    }

    public GameObject GetCurrentTurret()
    {
        return currentTurret;
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
