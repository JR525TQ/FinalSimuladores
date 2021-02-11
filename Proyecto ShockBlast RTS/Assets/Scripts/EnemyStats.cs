using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public UnitStats enemyStats;

    float health;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyStats.health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
