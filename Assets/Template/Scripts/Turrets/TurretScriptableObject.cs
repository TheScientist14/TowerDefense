using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Turret")]
public class TurretScriptableObject : ScriptableObject
{

    public string turretName;
    public float fireRate; // bullet per second
    public int damageValue; // of the bullets fired
    public float bulletSpeed; // speed of the bullet
    public float range; // of detection of enemies
    public int price;
}
