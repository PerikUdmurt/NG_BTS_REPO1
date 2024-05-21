using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skills/PlayerSpeed")]
public class PlayerSpeed : ScriptableSkills
{
    public float SpeedPercent;
    public override void Init()
    {
        _Controller.speed = _Controller.speed + (_Controller.speed * SpeedPercent * 0.01f);
    }
}
