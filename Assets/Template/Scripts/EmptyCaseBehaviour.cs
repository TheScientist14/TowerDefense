using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCaseBehaviour : MonoBehaviour
{
    //public GameObject turretsParent;

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
            if (currentTurret == null || currentTurret.GetType() != SelectionBehaviour.selectedTurret.GetType())
            {
                currentTurret = Instantiate(SelectionBehaviour.selectedTurret, transform.position, Quaternion.identity);
                // lose money
            }
        }
    }
}
