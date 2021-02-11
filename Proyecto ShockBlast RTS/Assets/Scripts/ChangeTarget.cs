using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTarget : MonoBehaviour
{

    private Unit unitControl;
    private EnemyIA enemyUnit;

    // Start is called before the first frame update
    void Start()
    {
        unitControl = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (unitControl.currentTarget == null)
        {
            enemyUnit = FindObjectOfType<EnemyIA>();
            if(enemyUnit != null)
            {
                unitControl.SetNewTarget(enemyUnit.transform);
            }
        }*/
    }
}
