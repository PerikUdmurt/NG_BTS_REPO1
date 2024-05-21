using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAnimation : MonoBehaviour
{
    public Transform playerVisualTransform;
    public Transform target;

    void Update()
    {
        if (target == null)
        { return; }
        if (target.position.x >= playerVisualTransform.position.x)
        {
            playerVisualTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else { playerVisualTransform.localRotation = Quaternion.Euler(0, 180, 0); }
    }
}
