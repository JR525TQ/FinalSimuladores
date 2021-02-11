using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{

    private Unit unitControl;
    private PlayerUnit[] playerUnits;

    // Start is called before the first frame update
    void Start()
    {
        unitControl = GetComponent<Unit>();
        if (unitControl.currentTarget == null) 
        {
            playerUnits = FindObjectsOfType<PlayerUnit>();
            Transform playerUnit = playerUnits[Random.Range(0, playerUnits.Length)].transform;

            if (Vector3.Distance(transform.position, playerUnit.position) > 3)
            {
                CapturePointManager point = FindObjectOfType<CapturePointManager>();
                unitControl.MoveUnit(point.transform.position);
            }
            else
            {
                unitControl.SetNewTarget(playerUnit);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(unitControl.currentTarget == null) 
        {
            playerUnits = FindObjectsOfType<PlayerUnit>();
            Transform playerUnit = playerUnits[Random.Range(0, playerUnits.Length)].transform;

            if(Vector3.Distance(transform.position, playerUnit.position) > 3)
            {
                CapturePointManager point = FindObjectOfType<CapturePointManager>();
                unitControl.MoveUnit(point.transform.position);
            }
            else
            {
                unitControl.SetNewTarget(playerUnit);
            }
        }
    }
}
