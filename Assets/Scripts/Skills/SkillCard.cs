using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    public GameObject panel;
    public SkillList playerSkillList; 
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public Image cardIcon;
    private ScriptableSkills inputSkill;
    private Image cardImage;

    private void Awake()
    {
        cardImage = GetComponent<Image>();
    }
    public void SetCardInfo(ScriptableSkills skill)
    {
        cardName.text = skill.name;
        cardDescription.text = skill.skillDescription;
        inputSkill = skill;
        cardImage.color = skill.color;
    }

    public void GetCard()
    {
        playerSkillList.GetSkill(inputSkill, inputSkill.includeSkillsToList, inputSkill.excludeSkillsToList);
        Time.timeScale = 1.0f;
        panel.SetActive(false);
    }

}
