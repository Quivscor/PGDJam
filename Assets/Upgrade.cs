using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{

    public SkillType skillType;
    public float bonus;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI levelText;
    public Button buyButton;

    private int cost;
    private int level;

    public void OnEnable()
    {
        CanBuySkill();
    }

    public void AddLevel()
    {
        if (!CanBuySkill())
            return;

        if(skillType == SkillType.Health)
        {
            StatsController.Instance.SpendUpgradePoints(cost);
            StatsController.Instance.AddHp(bonus);
        }

        if (skillType == SkillType.Mana)
        {
            StatsController.Instance.SpendUpgradePoints(cost);
            StatsController.Instance.AddMaxMana();
        }

        if (skillType == SkillType.CrowsDamage)
        {
            StatsController.Instance.SpendUpgradePoints(cost);
            StatsController.Instance.UpgradeCrowDamage(bonus);
        }

        if (skillType == SkillType.CrowsCooldown)
        {
            StatsController.Instance.SpendUpgradePoints(cost);
            StatsController.Instance.UpgradeCrowCooldown(bonus * -1f);
        }

        LevelUpSkill();
    }


    public bool CanBuySkill()
    {
        if (cost <= StatsController.Instance.UpgradePoints)
        {
            buyButton.interactable = true;
            return true;
        }
        else
        {
            buyButton.interactable = false;
            return false;
        }
    }

    private void LevelUpSkill()
    {
        level++;
        cost = level + 1;

        levelText.text = "Poziom " + level;
        costText.text = "Koszt " + cost;
    }

}
public enum SkillType
{
    Health,
    Mana,
    CrowsDamage,
    CrowsCooldown
}
