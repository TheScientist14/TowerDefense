using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCaseBehaviour : MonoBehaviour
{
    private GameObject currentTurret;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject selectedTurret = Selection.GetSelectedTurret();
            int price = Selection.GetSelectedTurretPrice();
            if (selectedTurret != null)
            {
                if (currentTurret == null || currentTurret.GetType() != selectedTurret.GetType())
                {
                    if(GameManagement.GetMoney() >= price)
                    {
                        currentTurret = Instantiate(selectedTurret, transform.position, Quaternion.identity, GameManagement.instance.turretsContainer.transform);
                        GameManagement.RemoveMoney(price);
                        TextManagement.instance.UpdateMoneyText();
                    }
                }
            }
        }
    }
}
