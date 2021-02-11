using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneAbility1 : MonoBehaviour
{

    public GameObject compañero;

    public Texture2D ability2d;
    public Texture2D ability2dCD;

    float abilityTimer;

    public float abilityCD;
    public float healPower = 50;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        abilityTimer -= Time.deltaTime;
    }

    private void OnGUI()
    {
        bool abilityKey = Input.GetKeyDown(KeyCode.Q);

        if(abilityTimer <= 0)
        {
            GUI.Label(new Rect(10,10,50,50), ability2d);
            if(abilityKey)
            {
                Ability();
            }
        }
        else
        {
            GUI.Label(new Rect(10, 10, 50, 50), ability2dCD);
        }
    }

    public virtual void Ability()
    {
        Unit ally = compañero.GetComponent<Unit>();

        ally.unitStats.health += healPower;

        Debug.Log("Ahora la vida del compañero es de:" + ally.unitStats.health);

        abilityTimer = abilityCD;
    }
}
