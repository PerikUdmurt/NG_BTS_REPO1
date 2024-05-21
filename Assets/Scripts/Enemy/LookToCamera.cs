using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{

    private void Update()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
    }
}
