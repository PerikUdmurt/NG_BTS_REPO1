using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public Quaternion angle;
    public float knockback;
    public float speed;
    public bool piercing;
    public float destroyTime;
    public int bounce;
    public Vector2 direction;
    public Rigidbody2D rb;
    private void Start()
    {
        StartCoroutine(BulletDestroyer(destroyTime));
    }
    
    private void Update()
    {
        Move(speed);
    }

    public void Move(float bulletSpeed)
    {
       gameObject.transform.Translate((Vector2.right * bulletSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.attachedRigidbody)
        {
            collision.attachedRigidbody.AddForce(direction.normalized * knockback, ForceMode2D.Impulse);
        }
        if (collision.GetComponent<Health>())
        {
            Health health = collision.GetComponent<Health>();
            health.GetDamage(damage);
        }
        if (bounce > 0)
        {
            transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
            bounce--;
            return;
        }
        if (!piercing&&(bounce <= 0)) { Destroy(gameObject); }

    }

    private IEnumerator BulletDestroyer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
