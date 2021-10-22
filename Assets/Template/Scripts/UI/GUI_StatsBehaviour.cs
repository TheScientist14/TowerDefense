using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_StatsBehaviour : MonoBehaviour
{
    // tracking the opened menu so that we close it before opening another one
    private static GUI_StatsBehaviour opened;

    public GameObject parentCase;
    public TextMeshProUGUI turretName;
    public TextMeshProUGUI damageStat;
    public TextMeshProUGUI rangeStat;
    public TextMeshProUGUI bullSpeedStat;
    public TextMeshProUGUI fireRateStat;
    public TextMeshProUGUI upgradePrice;
    public GameObject upgradeButton;

    private EmptyCaseBehaviour parentCaseBehaviour;
    private Image upgradeButtonImage;
    private Color upgradeButtonImageColor;
    private Color noUpgradeButtonImageColor = Color.grey;

    private static Vector2 shift = new Vector2(150, 0);
    private Vector3 screenPoint;
    private Camera cam;

    private void Awake()
    {
        parentCaseBehaviour = parentCase.GetComponent<EmptyCaseBehaviour>();
        upgradeButtonImage = upgradeButton.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        upgradeButtonImageColor = Color.green;
        cam = Camera.main;
        Hide();
        GameManagement.instance.moneyAmountChangedEvent.AddListener(OnMoneyChange);
    }

    // Update is called once per frame
    void Update()
    {
        screenPoint = cam.WorldToScreenPoint(parentCase.transform.position);
        if(screenPoint.x < cam.pixelWidth / 2)
        {
            transform.position = new Vector2(screenPoint.x, screenPoint.y) + shift;
        }
        else
        {
            transform.position = new Vector2(screenPoint.x, screenPoint.y) - shift;
        }
    }

    public void UpdateData()
    {
        TurretBehaviour turret = parentCaseBehaviour.GetCurrentTurret().GetComponent<TurretBehaviour>();
        turretName.text = turret.GetName() + " (" + turret.GetLevel().ToString() + ")";
        damageStat.text = turret.GetDamage().ToString();
        rangeStat.text = turret.GetRange().ToString();
        bullSpeedStat.text = turret.GetBulletSpeed().ToString();
        fireRateStat.text = turret.GetFireRate().ToString();
        if (!turret.IsFullyUpgraded())
        {
            upgradePrice.text = turret.GetUpgradePrice().ToString();
        }
        else
        {
            upgradePrice.text = "\u221E";
        }
        OnMoneyChange();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        if (opened == this)
        {
            opened = null;
        }
    }

    public void OnMoneyChange()
    {
        if(parentCaseBehaviour.GetCurrentTurret() != null)
        {
            if(GameManagement.instance.GetMoney() >= parentCaseBehaviour.GetCurrentTurretBehaviour().GetUpgradePrice())
            {
                upgradeButtonImage.color = upgradeButtonImageColor;
            }
            else
            {
                upgradeButtonImage.color = noUpgradeButtonImageColor;
            }
        }
    }

    private void OnEnable()
    {
        if(parentCaseBehaviour.GetCurrentTurret() != null)
        {
            if(opened != null)
            {
                opened.gameObject.SetActive(false);
            }
            opened = this;
        }
    }
}
