using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPosition : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject target;
    public string tagOfTarget;
    public float searchTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target == null )
        {
            target = FindNearesTarget(tagOfTarget);
        }
    }
    private void Update()
    {
        agent.SetDestination(target.transform.position); 
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
}
