using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Abilities : MonoBehaviour
{

    public VisualEffect effectHeal;

    [Header("Ability1")]
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown = false;
    public KeyCode ability1;
    public Transform target;
    public GameObject ability1Skill;

    Vector3 position;

    //ability1 inputs
    public Canvas ability1Canvas;
    public Image skillShot1;
    public Transform player;

    [Header("Ability2")]
    public Image abilityImage2;
    public float cooldown2 = 5;
    bool isCooldown2 = false;
    public KeyCode ability2;
    public GameObject ability2Skill;

    //ability2 inputs
    public Canvas ability2Canvas;
    public Image skillShot2;

    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        effectHeal.Stop();
        skillShot1.GetComponent<Image>().enabled = false;
        skillShot2.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ability1
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        ability1Canvas.transform.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);
        ability2Canvas.transform.rotation = Quaternion.Lerp(transRot, ability2Canvas.transform.rotation, 0f);

    }

    void Ability1()
    {
        if(Input.GetKeyDown(ability1) && isCooldown == false)
        {
            skillShot2.GetComponent<Image>().enabled = false;
            skillShot1.GetComponent<Image>().enabled = true;
        }

        if(skillShot1.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            isCooldown = true;
            abilityImage1.fillAmount = 1;
            GameObject vfx = Instantiate(ability1Skill, transform.position, ability1Canvas.transform.rotation);
            //vfx.GetComponent<ControlSkill1Knight>().target = target;
            Destroy(vfx, 4.0f);
        }

        if(isCooldown)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            skillShot1.GetComponent<Image>().enabled = false;
            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    void Ability2()
    {
        if (Input.GetKeyDown(ability2) && isCooldown2 == false)
        {
            skillShot1.GetComponent<Image>().enabled = false;
            skillShot2.GetComponent<Image>().enabled = true;
        }

        if (skillShot2.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
            GameObject vfx = Instantiate(ability2Skill, transform.position, ability2Canvas.transform.rotation);
            Destroy(vfx, 2.0f);
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            skillShot2.GetComponent<Image>().enabled = false;
            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
}
