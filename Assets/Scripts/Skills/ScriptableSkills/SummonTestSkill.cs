using System.Collections;
using System.Collections.Generic;
using Unity.Services.Matchmaker.Models;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Summon Shooter")]
public class SummonTestSkill : ScriptableSkills
{
    public GameObject summonEntity;
    public int numOfObject;
    public override void Init()
    {
        for (int i = 0; i < numOfObject; i++) { GameObject.Instantiate(summonEntity, new Vector2(_Controller.transform.position.x, _Controller.transform.position.y + 2), Quaternion.identity); }
    }
}
