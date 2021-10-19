using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmptyCaseBehaviour : MonoBehaviour
{
    public GameObject uiStats;
    private GUI_StatsBehaviour uiStatsBehaviour;

    public Material takenMaterial;
    public Material freeMaterial;
    private new Renderer renderer;

    private GameObject currentTurret;
    private TurretBehaviour currentTurretBehaviour;

    private void Awake()
    {
        uiStatsBehaviour = uiStats.GetComponent<GUI_StatsBehaviour>();
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        uiStatsBehaviour.Hide();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Selection.instance.IsTurretSelected())
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
    }

    public void SellCurrentTurret()
    {
        if (currentTurret != null)
        {
            GameManagement.instance.AddMoney(currentTurretBehaviour.GetSellPrice());
            uiStatsBehaviour.Hide();
            Destroy(currentTurret);
            TextManagement.instance.UpdateMoneyText();
            renderer.material = freeMaterial;
        }
    }

    private void BuySelectedTurret()
    {
        TurretBehaviour selectedTurretBehaviour = Selection.instance.GetSelectedTurretBehaviour();
        if (GameManagement.instance.GetMoney() >= selectedTurretBehaviour.GetPrice())
        {
            currentTurret = Instantiate(Selection.instance.GetSelectedTurret(), transform.position, Quaternion.identity, GameManagement.instance.turretsContainer.transform);
            currentTurretBehaviour = currentTurret.GetComponent<TurretBehaviour>();
            GameManagement.instance.RemoveMoney(selectedTurretBehaviour.GetPrice());
            TextManagement.instance.UpdateMoneyText();
            uiStatsBehaviour.UpdateData();
            renderer.material = takenMaterial;
        }
    }

    public void UpgradeCurrentTurret()
    {
        if (!currentTurretBehaviour.IsFullyUpgraded())
        {
            int price = currentTurretBehaviour.GetUpgradePrice();
            if (GameManagement.instance.GetMoney() >= price)
            {
                currentTurretBehaviour.UpgradeTurret();
                GameManagement.instance.RemoveMoney(price);
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
}
