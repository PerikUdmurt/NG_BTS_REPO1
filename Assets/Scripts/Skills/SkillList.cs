using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillList : MonoBehaviour
{
    public Controller controller;
    public List <ScriptableSkills> skills;
    public List <ScriptableSkills> availableSkills;
    
    private void Update()
    {
        foreach (var skill in skills) 
        {
            skill.Run();
        }
    }

    public void GetSkill(ScriptableSkills skill,List<ScriptableSkills> includeSkills,List<ScriptableSkills>excludeSkills)
    {
        if (skill != null)
        {
            skills.Add(skill);
            availableSkills.Remove(skill);
            foreach (ScriptableSkills item in includeSkills) {availableSkills.Add(item);}
            foreach(ScriptableSkills item in excludeSkills) {availableSkills.Remove(item);}
            skill._Controller = controller;
            skill.Init();
        }
    }
    
}
