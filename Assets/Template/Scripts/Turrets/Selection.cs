using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private static GameObject selectedTurret;
    private static TurretBehaviour selectedTurretBehaviour;

    public static void SelectTurret(GameObject turret)
    {
        selectedTurret = turret;
        selectedTurretBehaviour = selectedTurret.GetComponent<TurretBehaviour>();
    }

    public static GameObject GetSelectedTurret()
    {
        return selectedTurret;
    }

    // this script is stored on the hard disk, it is not bound to an running instance
    public static TurretBehaviour GetSelectedTurretBehaviour()
    {
        return selectedTurretBehaviour;
    }
}
