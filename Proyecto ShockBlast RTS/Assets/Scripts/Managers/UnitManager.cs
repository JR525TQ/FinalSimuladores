using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitManager : MonoBehaviour
{

    RaycastHit hit;
    List<Unit> selectedUnits = new List<Unit>();
    bool isDragging = false;
    Vector3 mousePosition;


    private void OnGUI()
    {
        if(isDragging)
        {
            var rect = ScreenHelper.GetScreenRect(mousePosition, Input.mousePosition);
            ScreenHelper.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.green);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            mousePosition = Input.mousePosition;

            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(camRay, out hit))
            {
                if(hit.transform.CompareTag("Unit"))
                {
                    SelectUnit(hit.transform.GetComponent<Unit>(), Input.GetKey(KeyCode.LeftShift));
                } 
                else
                {
                    DeselectUnits();
                    isDragging = true;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {

            foreach(var selecteableObject in FindObjectsOfType<PlayerUnit>())
            {
                if(isWithinSelectionBounds(selecteableObject.transform))
                {
                    SelectUnit(selecteableObject.transform.GetComponent<Unit>(), true);
                }
            }

            foreach (var selecteableObject in FindObjectsOfType<SkeletonUnit>())
            {
                if (isWithinSelectionBounds(selecteableObject.transform))
                {
                    SelectUnit(selecteableObject.transform.GetComponent<Unit>(), true);
                }
            }

            isDragging = false;
        }

        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {

            mousePosition = Input.mousePosition;

            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out hit))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    foreach(Unit unit in selectedUnits)
                    {
                        unit.MoveUnit(hit.point);
                    }
                }

                if (hit.transform.CompareTag("EnemyUnit"))
                {
                    foreach (Unit unit in selectedUnits)
                    {
                        unit.SetNewTarget(hit.transform);
                    }
                }
            }
        }

        /*if(Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].gameObject.GetComponent<NavMeshAgent>();
            }
        }*/
    }

    public void DeselectUnits()
    {
        for(int i = 0; i < selectedUnits.Count; i++)
        {
            if(selectedUnits[i] != null)
            {
                selectedUnits[i].SetSelected(false);
            }
        }

        selectedUnits.Clear();
    }

    public void SelectUnit(Unit unit, bool isMulti = false)
    {
        if(!isMulti)
        {
            DeselectUnits();
        }
        selectedUnits.Add(unit);
        unit.SetSelected(true);
    }

    private bool isWithinSelectionBounds(Transform transform)
    {
        if(!isDragging)
        {
            return false;
        }

        var camera = Camera.main;
        var viewportBounds = ScreenHelper.GetViewportBounds(camera, mousePosition, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(transform.position));
    }

}
