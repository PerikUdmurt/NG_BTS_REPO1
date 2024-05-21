using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float time;
    void Start()
    {
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        StartCoroutine(Timer());
    }
}
