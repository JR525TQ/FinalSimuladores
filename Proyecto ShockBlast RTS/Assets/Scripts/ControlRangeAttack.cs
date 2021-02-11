using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRangeAttack : MonoBehaviour
{
    public float damage;
    public float speedDisparo;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if(target==null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speedDisparo * Time.deltaTime);

        if (target.transform.position == transform.position)
        {
            //target.GetComponent<ControlEnemigo>().RecibirDaño(damage);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyUnit"))
        {
            Debug.Log("Hit");
            other.gameObject.GetComponent<Unit>().TakeDamage(damage);
            //Debug.Log("Vida restante: " + other.gameObject.GetComponent<ControlJugador>().vida);
            Destroy(gameObject);
        }
    }

}
