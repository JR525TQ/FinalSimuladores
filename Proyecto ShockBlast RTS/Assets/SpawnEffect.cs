using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effectSpawn;

    // Start is called before the first frame update
    void Start()
    {
        effectSpawn = vfx[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnVFX()
    {
        GameObject vfx;

        if(firePoint != null)
        {
            vfx = Instantiate(effectSpawn, firePoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Falta firepoint");
        }
    }
}
