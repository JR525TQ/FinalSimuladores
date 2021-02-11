using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlockingScript : MonoBehaviour
{

    Unit[] unidades;
    NavMeshAgent agent;

    public float space = 2;

    // Start is called before the first frame update
    void Start()
    {
        unidades = FindObjectsOfType<Unit>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(unidades.Length > 0)
        {
            foreach (Unit unit in unidades)
            {
                if(unit != null)
                {
                    float distance = Vector3.Distance(unit.transform.position, this.transform.position);
                    if (distance <= space)
                    {
                        agent.isStopped = true;
                    }
                    agent.isStopped = false;
                }
            }
        }
    }
}
