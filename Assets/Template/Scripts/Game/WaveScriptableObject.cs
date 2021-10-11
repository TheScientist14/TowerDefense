using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class WaveScriptableObject : ScriptableObject
{

    public GameObject thief;
    public GameObject capo;
    public int nbEmenyTief;
    public float spawnRateThief;
    public int nbEmenyCapo;
    public float spawnRateCapo;
}
