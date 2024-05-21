using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //TODO: јнимаци€ подсвечивани€ геро€ при получении урона
    private float health;
    public bool invincible;
    public float maxHealth;
    public bool invincibleAfterDamage;
    public float timeOfInvincibility;
    private List<ScriptableSkills> afterDamageEffect = new List<ScriptableSkills>();
    public void AddAfterDamageEffect(ScriptableSkills skill)
    {  afterDamageEffect.Add(skill); }
    
    void Awake()
    {
        health = maxHealth;
    }
    public void GetDamage(float damage)
    {
        GetDamage(damage, "Unknown");
    }
    public void GetDamage(float damage, string damageReason)
    {
        if (!invincible)
        {
            health = health - damage;
            DeathCheck();
            if (afterDamageEffect.Count > 0)
            {
                foreach (ScriptableSkills skill in afterDamageEffect)
                {
                    skill.Activate();
                }
            }
            else return;
        }
        if (invincibleAfterDamage)
        {
            StartCoroutine(GetInvincibleForTime(timeOfInvincibility));
        }
    }

    public void TakeHealth(float healPoint) 
    {
        health += healPoint;
        if (health > maxHealth) { health = maxHealth;}
    }

    public void TakeHealth(float healPoint, string healReason)
    {
        health += healPoint;
        if (health > maxHealth) { health = maxHealth; }
    }

    private void DeathCheck()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator GetInvincibleForTime(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }
}
