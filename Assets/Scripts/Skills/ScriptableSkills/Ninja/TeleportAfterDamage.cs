using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableSkills/TeleportAfterDamage")]
public class TeleportAfterDamage : ScriptableSkills
{
    private Health health;
    public float teleportDistance;
    public GameObject penekPrefab;
    public override void Init()
    {
        health = _Controller.GetComponent<Health>();
        health.AddAfterDamageEffect(this);
    }
    public override void Activate()
    {
        Instantiate(penekPrefab, _Controller.transform.position, Quaternion.identity);
        Vector2 randomDirection = new Vector2(Random.Range(-1,1), Random.Range(-1,1)).normalized;
        _Controller.transform.position = randomDirection * teleportDistance;
    }
}
