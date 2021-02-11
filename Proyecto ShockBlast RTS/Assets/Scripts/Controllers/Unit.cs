using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    public enum UnitAttackType { Melee, Ranged };
    public UnitAttackType unitAttackType;

    public float rotateSpeedForAttack;
    public GameObject rangeAttack;

    public bool basicAtkIdle = false;
    public bool isAlive = true;
    public bool performMeleeAttack = true;
    public bool performRangeAttack = true;


    private NavMeshAgent navAgent;
    public Transform currentTarget;
    public UnitStats unitStats;

    private float attackTimer;

    public float rotateSpeed = 0.075f;
    float rotateVelocity;

    public float health;
    public Slider unitSlider;

    private Animator anim;

    private PlayerUnit[] playerUnits;
    private SkeletonUnit[] skeletonUnits;
    public float followingDistance = 1;
    private EnemyPos[] enemyPos;

    public GameObject bloodParticle;
    private void Start()
    {
        currentTarget = null;
        navAgent = GetComponent<NavMeshAgent>();
        attackTimer = unitStats.attackSpeed;
        health = unitStats.health;
        unitSlider.maxValue = unitStats.health;
        anim = GetComponent<Animator>();

        if (this.CompareTag("EnemyUnit"))
        {
            skeletonUnits = FindObjectsOfType<SkeletonUnit>();

            if(skeletonUnits.Length > 0)
            {
                Transform skeletonUnit = skeletonUnits[UnityEngine.Random.Range(0, skeletonUnits.Length)].transform;

                if (skeletonUnit.CompareTag("Unit") && Vector3.Distance(transform.position, skeletonUnit.position) > 10)
                {
                    /*enemyPos = FindObjectsOfType<EnemyPos>();
                    MoveUnit(enemyPos[UnityEngine.Random.Range(0, enemyPos.Length)].transform.position);*/
                }
                else
                {
                    SetNewTarget(skeletonUnit);
                }
            }
            else
            {
                playerUnits = FindObjectsOfType<PlayerUnit>();
                Transform playerUnit = playerUnits[UnityEngine.Random.Range(0, playerUnits.Length)].transform;

                if (playerUnit.CompareTag("Unit") && Vector3.Distance(transform.position, playerUnit.position) > 10)
                {
                    /*enemyPos = FindObjectsOfType<EnemyPos>();
                    MoveUnit(enemyPos[UnityEngine.Random.Range(0, enemyPos.Length)].transform.position);*/
                }
                else
                {
                    SetNewTarget(playerUnit);
                }
            }
        }
    }


    private void Update()
    {

        if (currentTarget == null)
        {
            anim.SetBool("Basic Attack", false);
            performMeleeAttack = true;

            if(this.CompareTag("EnemyUnit"))
            {
                skeletonUnits = FindObjectsOfType<SkeletonUnit>();

                if (skeletonUnits.Length > 0)
                {
                    Transform skeletonUnit = skeletonUnits[UnityEngine.Random.Range(0, skeletonUnits.Length)].transform;

                    if (skeletonUnit.CompareTag("Unit") && Vector3.Distance(transform.position, skeletonUnit.position) > 10)
                    {
                        playerUnits = FindObjectsOfType<PlayerUnit>();
                        if (playerUnits.Length > 0)
                        {
                            Transform playerUnit = playerUnits[UnityEngine.Random.Range(0, playerUnits.Length)].transform;

                            if (Vector3.Distance(transform.position, playerUnit.position) > 10)
                            {

                            }
                            else
                            {
                                SetNewTarget(playerUnit);
                            }
                        }
                    }
                    else
                    {
                        SetNewTarget(skeletonUnit);
                    }
                } 
                else
                {
                    playerUnits = FindObjectsOfType<PlayerUnit>();
                    if (playerUnits.Length > 0)
                    {
                        Transform playerUnit = playerUnits[UnityEngine.Random.Range(0, playerUnits.Length)].transform;

                        if (Vector3.Distance(transform.position, playerUnit.position) > 10)
                        {

                        }
                        else
                        {
                            SetNewTarget(playerUnit);
                        }
                    }
                }
            }
        }

        /*if (currentTarget!=null)
        {
            Debug.Log(currentTarget.name);
        }*/


        unitSlider.value = health;

        attackTimer += Time.deltaTime;

        if(health <= 0)
        {
            currentTarget = null;
            performMeleeAttack = false;
            if(gameObject.CompareTag("EnemyUnit"))
            {
                GestorDeAudio.instancia.ReproducirSonido("SkeletonDie");
            }
            Destroy(gameObject);
        }

        if(currentTarget != null)
        {
            navAgent.destination = currentTarget.transform.position;
            Quaternion rotationLookAt = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeed * (Time.deltaTime * 5));

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            var distance = (transform.position - currentTarget.transform.position).magnitude;
            if (distance <= unitStats.attackRange)
            {
                //navAgent.isStopped = true;
                if(unitAttackType == UnitAttackType.Melee)
                {
                    if(performMeleeAttack)
                    {
                        Debug.Log("Attack");
                        StartCoroutine(AttackInterval());
                    }
                }

                if (unitAttackType == UnitAttackType.Ranged)
                {

                }


            }
        }
    }

    IEnumerator AttackInterval()
    {
        performMeleeAttack = false;
        performRangeAttack = false;
        anim.SetBool("Basic Attack", true);

        yield return new WaitForSeconds(attackTimer / ((100 + attackTimer) * 0.01f));

    }

    private void Attack()
    {
        //if(attackTimer >= unitStats.attackSpeed)
        //{
            if(currentTarget != null)
            {
                //performMeleeAttack = false;
                Debug.Log("Attacking");
                ShockBlastGameManager.UnitTakeDamage(this, currentTarget.GetComponent<Unit>());
                //attackTimer = 0;
            }
            else
            {

            }
        //}
        performMeleeAttack = true;
    }

    public void TakeDamage(Unit enemy, float damage)
    {
        //StartCoroutine(Flasher(GetComponent<Renderer>().material.color));
        health -= damage;

        if(gameObject.CompareTag("EnemyUnit"))
        {
            GameObject blood = Instantiate(bloodParticle, transform.position, Quaternion.identity);
            Destroy(blood, 0.3f);
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (gameObject.CompareTag("EnemyUnit"))
        {
            GameObject blood = Instantiate(bloodParticle, transform.position, Quaternion.identity);
            Destroy(blood, 0.3f);
        }
    }

    public void MoveUnit(Vector3 destination)
    {
        navAgent.isStopped = false;
        currentTarget = null;
        navAgent.destination = destination;
        Quaternion rotationLookAt = Quaternion.LookRotation(destination - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeed * (Time.deltaTime * 5));

        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void SetSelected(bool isSelected)
    {
        transform.Find("Highlight").gameObject.SetActive(isSelected);
    }

    public void SetNewTarget(Transform enemy)
    {
        currentTarget = enemy;
    }

    IEnumerator Flasher(Color defaultColor)
    {
        var renderer = GetComponent<Renderer>();
        for(int i=0; i<2;i++)
        {
            renderer.material.color = Color.gray;
            yield return new WaitForSeconds(0.5f);
            renderer.material.color = defaultColor;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
