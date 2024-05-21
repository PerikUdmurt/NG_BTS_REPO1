using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterEnemy : MonoBehaviour
{
    [Header("AiLogic")]
    public bool playAiLogic;
    public bool MoveToTarget;
    [Header("Navigation")]
    private NavMeshAgent agent;
    private GameObject shootTarget;
    public string tagOfShootTarget;
    public float searchTime;
    [Header("Attack")]
    public Weapon weapon;
    public bool attackTrigger;
    public float stayTime;
    public float maxDistanceToTarget;
    public float smallRange;
    public float collisionDamage;

    private VisualAnimation anim;
    private void Awake()
    {
        anim = GetComponent<VisualAnimation>();
        weapon = GetComponentInChildren<Weapon>();
        agent = GetComponent<NavMeshAgent>();
        
        if (shootTarget == null)
        {
            shootTarget = FindNearesTarget(tagOfShootTarget);
        }
        StartCoroutine(TargetFinder(searchTime));
    }
    private void Update()
    {
        anim.target = shootTarget.transform;
        weapon.inputTarget = shootTarget;
        weapon.SetAttackTrigger(attackTrigger);

        PlayAiLogic();
    }
    
    public virtual void PlayAiLogic()
    {
        if (playAiLogic) 
        {
            if (!MoveToTarget)
            {
                if (agent.pathPending || agent.remainingDistance > 0.1f)
                    return;
                agent.destination = smallRange * Random.insideUnitCircle;

                Vector3 currentDistance = transform.position - shootTarget.transform.position;
                if (currentDistance.magnitude < maxDistanceToTarget || agent.pathPending)
                    return;
                agent.destination = shootTarget.transform.position;
            }
            else { agent.SetDestination(shootTarget.transform.position); }
        }
    }

    private GameObject FindNearesTarget(string tag)
    {
        GameObject nearestTarget = null;
        float minDistance = float.MaxValue;
        GameObject[] targetList = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject item in targetList)
        {
            Vector3 distance = gameObject.transform.position - item.transform.position;
            if (distance.magnitude < minDistance)
            {
                minDistance = distance.magnitude;
                nearestTarget = item;
            }
            else continue;
        }
        return nearestTarget;
    }
    private IEnumerator TargetFinder(float time)
    {
        yield return new WaitForSeconds(time);
        shootTarget = FindNearesTarget(tagOfShootTarget);
        StartCoroutine(TargetFinder(time));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() && collision.transform.CompareTag(tagOfShootTarget))
        {
            Health health = collision.GetComponent<Health>();
            health.GetDamage(collisionDamage, "collision");
        }
    }
    
}
