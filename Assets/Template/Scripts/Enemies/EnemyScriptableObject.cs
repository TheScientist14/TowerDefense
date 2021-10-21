using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{

    public int health;
    public int damage;
    public int speed;
    public int price;
}
