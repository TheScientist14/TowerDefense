using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private GameObject selected;
    private TurretBehaviour selectedTurretBehaviour;
    private bool isTurret = false;
    private Camera cam;
    private Ray ray;
    private RaycastHit hit;
    private GameObject airStrikeLocation;
    private bool isAirStriking = false;
    private float airStrikeTime = 0;
    private float airStrikeCoolDown = 0;
    private float airStrikeCount = 3;
    private GameObject beingAirStriked;

    public static Selection instance;

    void Start()
    {
        // Singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        cam = Camera.main;
        airStrikeLocation = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        airStrikeLocation.transform.localScale = new Vector3(10, 1, 10);
        airStrikeLocation.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selected = null;
            selectedTurretBehaviour = null;
            isTurret = false;
        }
        // reducing cooldown
        if(airStrikeCoolDown > 0f)
        {
            airStrikeCoolDown -= Time.deltaTime;
        }
        else
        {
            // displaying air strike
            if (selected != null && !isTurret && !isAirStriking)
            {
                ray = cam.ScreenPointToRay(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
                if(Physics.Raycast(ray, out hit)){
                    airStrikeLocation.SetActive(true);
                    airStrikeLocation.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        isAirStriking = true;
                        airStrikeCoolDown = 10f;
                        beingAirStriked = selected;
                        airStrikeLocation.SetActive(false);
                        airStrikeCount = 3;
                    }
                }
                else
                {
                    airStrikeLocation.SetActive(false);
                }
            }
            else
            {
                airStrikeLocation.SetActive(false);
            }
        }
        if (isAirStriking) {
            /*
             *  managing air strike animation
             *  2 air bomb per second
             */
            if (airStrikeTime > 0)
            {
                airStrikeTime -= Time.deltaTime;
            }
            else
            {
                if (airStrikeCount > 0)
                {
                    airStrikeCount--;
                    Vector3 horizontalOffSet = Random.insideUnitCircle * 5;
                    Vector3 verticalOffSet = 40 * Vector3.up;
                    Instantiate(beingAirStriked, hit.point + verticalOffSet + horizontalOffSet, Quaternion.identity);
                    airStrikeTime = 0.5f;
                }
                else {
                    isAirStriking = false;
                }
            }
        }
    }


    public void Select(GameObject obj)
    {
        selected = obj;
        selectedTurretBehaviour = selected.GetComponent<TurretBehaviour>();
        isTurret = (selectedTurretBehaviour != null);
    }

    public GameObject GetSelectedTurret()
    {
        return selected;
    }

    // this script is stored on the hard disk, it is not bound to an running instance
    public TurretBehaviour GetSelectedTurretBehaviour()
    {
        return selectedTurretBehaviour;
    }

    public bool IsTurretSelected()
    {
        return (selected != null) && isTurret;
    }
}
