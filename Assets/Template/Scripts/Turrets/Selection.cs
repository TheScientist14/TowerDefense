using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private static GameObject selectedTurret;
    private static int selectedTurretPrice;

    public static void SelectTurret(GameObject turret)
    {
        selectedTurret = turret;
        selectedTurretPrice = selectedTurret.GetComponent<TurretBehaviour>().GetPrice();
    }

    public static GameObject GetSelectedTurret()
    {
        return selectedTurret;
    }

    public static int GetSelectedTurretPrice()
    {
        return selectedTurretPrice;
    }
}
