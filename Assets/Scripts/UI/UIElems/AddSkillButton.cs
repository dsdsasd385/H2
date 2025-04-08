using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddSkillButton : MonoBehaviour
{
    public event Action<Skill> SkillSelected; 
    
    [SerializeField] private Button   btnAdd;
    [SerializeField] private TMP_Text txtDescribe;
    
    public void SetSkill(Skill skillToAdd)
    {
        SkillSelected = null;
        
        btnAdd.onClick.RemoveAllListeners();
        btnAdd.onClick.AddListener(()=> SkillSelected?.Invoke(skillToAdd));
        txtDescribe.text = $"{skillToAdd}";
        gameObject.SetActive(true);
    }
}