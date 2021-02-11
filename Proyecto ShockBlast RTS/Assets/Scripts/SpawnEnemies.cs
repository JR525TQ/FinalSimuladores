using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject enemy;
    public GameObject unit;
    public CapturePointManager capturePoint;
    public float tiempoRespawn;

    private GameObject actualSpawn;

    // Start is called before the first frame update
    void Start()
    {
        actualSpawn = enemy;
        StartCoroutine(Spawns());
    }

    void SpawnItemVelocidad()
    {
        GameObject item = Instantiate(actualSpawn) as GameObject;
        item.transform.position = new Vector3(Random.Range(transform.position.x, transform.position.x + 5), 0.5f, Random.Range(transform.position.z, transform.position.z + 5));
    }

    IEnumerator Spawns()
    {
        while (true)
        {
            SpawnItemVelocidad();
            yield return new WaitForSeconds(tiempoRespawn);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(capturePoint.RedStatus())
        {
            actualSpawn = enemy;
        }

        if (capturePoint.BlueStatus())
        {
            actualSpawn = unit;
            tiempoRespawn = 25;
        }
    }
}
