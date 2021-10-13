using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private GameObject selectedTurret;
    private TurretBehaviour selectedTurretBehaviour;

    public static Selection instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectedTurret = null;
            selectedTurretBehaviour = null;
        }
    }

    public void SelectTurret(GameObject turret)
    {
        selectedTurret = turret;
        selectedTurretBehaviour = selectedTurret.GetComponent<TurretBehaviour>();
    }

    public GameObject GetSelectedTurret()
    {
        return selectedTurret;
    }

    // this script is stored on the hard disk, it is not bound to an running instance
    public TurretBehaviour GetSelectedTurretBehaviour()
    {
        return selectedTurretBehaviour;
    }
}
