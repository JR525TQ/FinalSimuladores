using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class HeroManager : MonoBehaviour
{
    [Header("Heroes")]
    public GameObject knight;
    public GameObject wizard;
    public UnitManager unitManager;

    [Header("VFXConfig")]
    public VisualEffect effectWizzarAbility2;

    [Header("HUDConfig")]
    public GameObject HUDKnight;
    public GameObject HUDWizard;


    // Start is called before the first frame update
    void Start()
    {
        effectWizzarAbility2.Stop();
        knight.GetComponent<Abilities>().enabled = true;
        wizard.GetComponent<AbilitiesWizard>().enabled = false;
        HUDWizard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            knight.GetComponent<Abilities>().enabled = true;
            wizard.GetComponent<AbilitiesWizard>().enabled = false;
            unitManager.DeselectUnits();
            unitManager.SelectUnit(knight.GetComponent<Unit>(), false);
            HUDWizard.SetActive(false);
            HUDKnight.SetActive(true);            
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            knight.GetComponent<Abilities>().enabled = false;
            wizard.GetComponent<AbilitiesWizard>().enabled = true;
            unitManager.DeselectUnits();
            unitManager.SelectUnit(wizard.GetComponent<Unit>(), false);
            HUDKnight.SetActive(false);
            HUDWizard.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SkeletonUnit[] units = FindObjectsOfType<SkeletonUnit>();

            if(units.Length > 0)
            {
                unitManager.DeselectUnits();
                foreach(var skeletonUnit in units)
                {
                    Unit unit = skeletonUnit.GetComponent<Unit>();
                    if(unit != null)
                    {
                        unitManager.SelectUnit(unit, true);
                    }
                }
            }
        }

    }
}
