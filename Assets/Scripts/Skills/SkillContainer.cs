using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    public List<SkillCard> skillCards;
    public SkillList skillList;
    
    public void SetCardsInfo()
    {
        List<ScriptableSkills> availableSkills = new List<ScriptableSkills>();
        availableSkills.AddRange(skillList.availableSkills.ToArray());
        foreach (SkillCard card in skillCards)
        {
            if (skillList.availableSkills.Count > 0)
            {
                ScriptableSkills currentSkill = availableSkills[Random.Range(0, availableSkills.Count)];
                card.SetCardInfo(currentSkill);
                availableSkills.Remove(currentSkill);
            }
            else { card.SetCardInfo(null); }
        }
    }
}

