using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Selection : MonoBehaviour
{
    private GameObject selected;
    private TurretBehaviour selectedTurretBehaviour;
    private bool isTurret = false;
    private Camera cam;
    private Ray ray;
    private RaycastHit hit;
    private bool isAirStriking = false;
    private float airStrikeTime = 0;
    private float airStrikeCoolDown = 0;
    private float airStrikeCount = 3;
    private GameObject beingAirStriked;

    public GameObject airStrikeLocationModel;
    private GameObject airStrikeLocation;
    public Slider cooldown;

    public static Selection instance;

    private void Awake()
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
    }

    void Start()
    {
        cam = Camera.main;
        //airStrikeLocation = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //airStrikeLocation.transform.localScale = new Vector3(10, 1, 10);
        airStrikeLocation = Instantiate(airStrikeLocationModel);
        airStrikeLocation.layer = LayerMask.NameToLayer("Ignore Raycast");
        
        airStrikeLocation.SetActive(false);
        airStrikeCoolDown = 25f;
        cooldown.maxValue = airStrikeCoolDown;
        cooldown.value = airStrikeCoolDown;
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
            cooldown.value -= Time.deltaTime;
        }
        else
        {
            // displaying air strike
            if (selected != null && !isTurret && !isAirStriking)
            {
                ray = cam.ScreenPointToRay(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
                if(Physics.Raycast(ray, out hit) && !EventSystem.current.IsPointerOverGameObject()){
                    airStrikeLocation.SetActive(true);
                    airStrikeLocation.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        isAirStriking = true;
                        airStrikeCoolDown = 25f;
                        cooldown.maxValue = airStrikeCoolDown;
                        cooldown.value = airStrikeCoolDown;
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
                    Vector2 horizontalOffSet = Random.insideUnitCircle * 5;
                    Vector3 offSet = new Vector3(horizontalOffSet.x, 80, horizontalOffSet.y);
                    Instantiate(beingAirStriked, hit.point + offSet, Quaternion.identity);
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

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
