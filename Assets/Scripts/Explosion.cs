using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionForce;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.attachedRigidbody != null)
        {
            Vector2 force = explosionForce * (other.transform.position - gameObject.transform.position);
            other.attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
        }
        if (other.GetComponent<Health>())
        {
            Health health = other.GetComponent<Health>();
            health.GetDamage(damage, "explosion");
        }
    }
    private void Start()
    {
        StartCoroutine(Timer(0.5f));
    }
    IEnumerator Timer(float explosionTime)
    {
        yield return new WaitForSeconds(explosionTime);
        Destroy(gameObject);
    }
}
