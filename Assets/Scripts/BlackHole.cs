using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float radius;
    public float damage;
    private float time = 1;
    private float radiusChangeSpeed = 0.03f;
    public float attractionForce;
    public HashSet<Rigidbody2D> rigList = new HashSet<Rigidbody2D>();
    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(radius, radius, 0), radiusChangeSpeed);
        foreach (var other in rigList)
        {
            Vector2 direction = (gameObject.transform.position - other.transform.position).normalized;
            float distance = (gameObject.transform.position - other.transform.position).magnitude;
            float force = attractionForce / distance;
            other.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody)
        {
            rigList.Add(other.attachedRigidbody);
            if (other.GetComponent<Health>() != null)
            {
                StartCoroutine(SetDamage(damage, time, other.attachedRigidbody));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        rigList.Remove(other.attachedRigidbody);
    }

    private IEnumerator SetDamage(float damage, float time, Rigidbody2D rig)
    {
        Health health = rig.GetComponent<Health>();
        float distance = (gameObject.transform.position - rig.transform.position).magnitude;
        float finalDamage = damage / distance;
        health.GetDamage(finalDamage);
        yield return new WaitForSeconds(time);
        
        if(rigList.Contains(rig)) { StartCoroutine(SetDamage(damage, time, rig)); }
        
    }
    
    public void SetConfiguration(float n_radius, float n_attractionForce, float n_damage)
    {
        attractionForce = n_attractionForce;
        radius = n_radius;
        damage = n_damage;
    }
}
