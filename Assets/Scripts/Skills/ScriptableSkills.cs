using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScriptableSkills : ScriptableObject
{
    [HideInInspector]public Controller _Controller;
    public Image skillImage;
    public string skillName;
    public string Class;
    public string skillDescription;
    public Color color;
    public List<ScriptableSkills> includeSkillsToList;
    public List<ScriptableSkills> excludeSkillsToList;
    public float ReloadDuration;
    public int maxActivateNum = 1;
    [HideInInspector]public int currentActivateNum;


    public abstract void Init();
    public virtual void Run() {}
    public virtual void Activate() { }
}
