using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSkill1Knight : MonoBehaviour
{
    public float speed;
    public float damage = 10.0f;

    public Transform target;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if(target.position == transform.position)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyUnit"))
        {
            Debug.Log("Hit");
            other.gameObject.GetComponent<Unit>().TakeDamage(damage);
            //Debug.Log("Vida restante: " + other.gameObject.GetComponent<ControlJugador>().vida);
            Destroy(gameObject);
        }
    }

    public Transform getTarget()
    {
        return target;
    }
}
