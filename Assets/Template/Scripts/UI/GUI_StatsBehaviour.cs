using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_StatsBehaviour : MonoBehaviour
{
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

    private static Vector2 shift = new Vector2(200, 0);
    private Vector3 screenPoint;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        parentCaseBehaviour = parentCase.GetComponent<EmptyCaseBehaviour>();
        upgradeButtonImage = upgradeButton.GetComponent<Image>();
        upgradeButtonImageColor = upgradeButtonImage.color;
        cam = Camera.main;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        screenPoint = cam.WorldToScreenPoint(parentCase.transform.position);
        transform.position = new Vector2(screenPoint.x, screenPoint.y) + shift;
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
            upgradeButtonImage.color = upgradeButtonImageColor;
        }
        else
        {
            upgradeButtonImage.color = Color.grey;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
