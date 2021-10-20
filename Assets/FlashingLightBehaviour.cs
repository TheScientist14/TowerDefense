using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLightBehaviour : MonoBehaviour
{
    public GameObject redLight;
    public GameObject blueLight;
    private float flashDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        float initDelay = Random.Range(0, flashDuration);
        if(Random.Range(0, 1) > 0.5f)
        {
            redLight.SetActive(false);
            blueLight.SetActive(true);
        }
        else
        {
            redLight.SetActive(true);
            blueLight.SetActive(false);
        }
        InvokeRepeating("switchLight", initDelay, flashDuration);
    }

    void switchLight()
    {
        redLight.SetActive(!redLight.activeInHierarchy);
        blueLight.SetActive(!blueLight.activeInHierarchy);
    }
}
