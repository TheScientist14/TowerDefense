using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonBehaviour : MonoBehaviour
{
    void OnMouseDown()
    {
        Application.Quit();
    }
}
