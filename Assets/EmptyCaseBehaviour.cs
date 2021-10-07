using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCaseBehaviour : MonoBehaviour
{
    private GameObject currentTurret;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (SelectionBehaviour.selectedTurret != null)
        {
            //if (currentTurret.class == SelectionBehaviour.selectedTurret.class)
            {
              //  currentTurret = Instantiate(SelectionBehaviour.selectedTurret, transform.position, Quaternion.identity);
            }
        }
    }
}
