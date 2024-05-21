using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploseOnDesroy : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float explosionDamage;
    public float explosionImpulse;

    private void OnDestroy()
    {
        GameObject expObj = Instantiate(explosionPrefab,transform.position,Quaternion.identity);
        Explosion expOption = expObj.GetComponent<Explosion>();
        expOption.damage = explosionDamage;
        expOption.explosionForce = explosionImpulse;
    }
}
