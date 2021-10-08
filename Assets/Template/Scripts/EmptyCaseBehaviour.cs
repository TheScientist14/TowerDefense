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
        Debug.Log("Clicked");
        if (SelectionBehaviour.selectedTurret != null)
        {
            Debug.Log("Placing selected turret");
            if (currentTurret == null || currentTurret.GetType() == SelectionBehaviour.selectedTurret.GetType())
            {
                Debug.Log("Placed");
                currentTurret = Instantiate(SelectionBehaviour.selectedTurret, transform.position, Quaternion.identity);
            }
        }
    }
}
