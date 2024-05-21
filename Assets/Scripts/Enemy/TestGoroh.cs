using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class TestGoroh : ShooterEnemy
{
    public GameObject goroshinaPrefab;
    public int numOfGoroshina;
    private void OnDestroy()
    {
        for (int i = 0; i < numOfGoroshina; i++)
        {
            Instantiate(goroshinaPrefab, transform.position, Quaternion.identity);
        }
        
    }
}
