using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class AbilitiesWizard : MonoBehaviour
{
    [Header("Ability1")]
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown = false;
    public KeyCode ability1;
    public Unit knight;
    public VisualEffect effect1;

    Vector3 position;


    [Header("Ability2")]
    public Image abilityImage2;
    public float cooldown2 = 5;
    bool isCooldown2 = false;
    public KeyCode ability2;
    public VisualEffect effect2;

    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
    }

    void Ability1()
    {
        if(Input.GetKeyDown(ability1) && isCooldown == false)
        {
            isCooldown = true;
            abilityImage1.fillAmount = 1;
            knight.health += 40;
            GetComponent<Unit>().health -= 10;
            StartCoroutine(HealAlly(effect1));
            GestorDeAudio.instancia.ReproducirSonido("Healing2");
        }

        if(isCooldown)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    IEnumerator HealAlly(VisualEffect effect)
    {
        effect.Play();
        yield return new WaitForSeconds(2);
        effect.Stop();
    }


    void Ability2()
    {
        if (Input.GetKeyDown(ability2) && isCooldown2 == false)
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
            GetComponent<Unit>().health += 40;
            StartCoroutine(HealAlly(effect2));
            HealSkeletonUnits();
            GestorDeAudio.instancia.ReproducirSonido("Healing");
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }


    private void HealSkeletonUnits()
    {
        SkeletonUnit[] skeletonsUnits = FindObjectsOfType<SkeletonUnit>();

        if(skeletonsUnits.Length > 0)
        {
            foreach (var skeleton in skeletonsUnits)
            {
                Unit skeletonUnit = skeleton.GetComponent<Unit>();

                if(skeletonUnit != null)
                {
                    skeletonUnit.health += 30;
                    StartCoroutine(HealAlly(skeleton.healEffect));                   
                }
            }
        }
    }

}
