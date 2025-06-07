using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSkillList", menuName = "Scriptable Objects/UpgradeSkillList")]
public class UpgradeSkillList : ScriptableObject
{
    public List<UpgradeSkill> upgradeSkillList = new List<UpgradeSkill>();
}
