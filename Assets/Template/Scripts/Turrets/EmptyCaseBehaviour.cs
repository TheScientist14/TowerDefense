using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCaseBehaviour : MonoBehaviour
{
    public GameObject uiStats;

    private GameObject currentTurret;
    private int currentTurretPrice;
    private bool overing = false;

    private static Vector2 uiShift = new Vector2(10, 10);

    // Start is called before the first frame update
    void Start()
    {
        //uiStats.SetActive(false);
        Camera cam = Camera.main;
        Vector3 screenPoint = cam.WorldToScreenPoint(gameObject.transform.position);
        uiStats.transform.position = new Vector2(screenPoint.x, screenPoint.y) + uiShift;
    }

    // Update is called once per frame
    void Update()
    {
        if (overing)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject selectedTurret = Selection.GetSelectedTurret();
                int price = Selection.GetSelectedTurretPrice();
                if (selectedTurret != null)
                {
                    // TODO : give back money if turret is replacing the previous one
                    if (currentTurret == null || currentTurret.GetType() != selectedTurret.GetType())
                    {
                        if (GameManagement.GetMoney() >= price)
                        {
                            currentTurret = Instantiate(selectedTurret, transform.position, Quaternion.identity, GameManagement.instance.turretsContainer.transform);
                            currentTurretPrice = price;
                            GameManagement.RemoveMoney(price);
                            TextManagement.instance.UpdateMoneyText();
                        }
                    }
                }
            }
            else if (Input.GetButtonDown("Fire2"))
            {

            }
            else if (Input.GetButtonDown("Fire3"))
            {
                if (currentTurret != null)
                {
                    GameManagement.AddMoney(currentTurretPrice);
                    Destroy(currentTurret);
                    TextManagement.instance.UpdateMoneyText();
                    currentTurretPrice = 0;
                }
            }
        }
    }

    private void OnMouseOver()
    {
        overing = true;
    }

    private void OnMouseExit()
    {
        overing = false;
    }
}
