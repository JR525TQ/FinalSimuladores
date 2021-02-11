using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDamage : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Hit");

        if (other.gameObject.CompareTag("EnemyUnit"))
        {

            other.gameObject.GetComponent<Unit>().health -= 5;
        }
    }
}
