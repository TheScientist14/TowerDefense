using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private static GameObject selectedTurret;
    private static TurretBehaviour selectedTurretPrice;

    public static void SelectTurret(GameObject turret)
    {
        selectedTurret = turret;
        selectedTurretPrice = selectedTurret.GetComponent<TurretBehaviour>();
    }

    public static GameObject GetSelectedTurret()
    {
        return selectedTurret;
    }

    public static TurretBehaviour GetSelectedTurretBehaviour()
    {
        return selectedTurretPrice;
    }
}
