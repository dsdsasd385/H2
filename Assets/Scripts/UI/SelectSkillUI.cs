using System.Collections.Generic;
using UnityEngine;

public class SelectSkillUI : UI
{
    private static SelectSkillUI Instance => UI.Open<SelectSkillUI>();
    
    public static Skill SelectedSkill { get; private set; }

    public static void Initialize(List<Skill> addableSkills)
    {
        SelectedSkill = null;
        Instance.SetButtons(addableSkills);
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/

    [SerializeField] private Transform      buttonParent;
    [SerializeField] private AddSkillButton buttonPrefab;

    private readonly List<AddSkillButton> _buttons = new();

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            var button = Instantiate(buttonPrefab, buttonParent);
            _buttons.Add(button);
        }
    }

    private void SetButtons(List<Skill> addableSkills)
    {
        _buttons.ForEach(btn => btn.gameObject.SetActive(false));

        for (var i = 0; i < addableSkills.Count; i++)
        {
            var skill = addableSkills[i];
            var button = _buttons[i];
            
            button.SetSkill(skill);
            button.SkillSelected += OnSkillSelected;
        }
    }

    private void OnSkillSelected(Skill skill)
    {
        SelectedSkill = skill;
        Close();
    }
}