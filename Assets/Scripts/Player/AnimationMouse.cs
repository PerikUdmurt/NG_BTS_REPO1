using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMouse : MonoBehaviour
{
    private Transform playerVisualTransform;
    public Joystick shootJoystick;
    public bool androidMode;

    private void Awake()
    {
        playerVisualTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (androidMode)
        {
            if (shootJoystick.Horizontal >= 0)
            {
                playerVisualTransform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else { playerVisualTransform.rotation = Quaternion.Euler(0, 180, 0); }
        }
        else
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mouseWorldPosition.x >= playerVisualTransform.position.x)
            {
                playerVisualTransform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else { playerVisualTransform.rotation = Quaternion.Euler(0, 180, 0); }
        }
    }

}
