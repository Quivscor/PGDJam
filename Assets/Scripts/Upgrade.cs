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
    public AudioClip purchaseSound;

    private int cost = 5;
    private int level = 0;
    private int maxLevel = 5;

    private void Start()
    {
        StatsController.Instance.upgradePointsChanged += CanUpgrade;
        CanBuySkill();
    }

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
            StatsController.Instance.AddMaxHP(bonus);
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
            StatsController.Instance.UpgradeCrowCooldown(bonus);
        }

        if (skillType == SkillType.MaxCrowsNumber)
        {
            StatsController.Instance.SpendUpgradePoints(cost);
            StatsController.Instance.UpgradeCrowsMaxNumber();
        }

        if (skillType == SkillType.CrowsSpeed)
        {
            StatsController.Instance.SpendUpgradePoints(cost);
            PlayerCombat.Instance.AddForceForRavens(100);
        }

        if (skillType == SkillType.DashCooldown)
        {
            StatsController.Instance.SpendUpgradePoints(cost);
            PlayerMovement.Instance.dashCd -= 0.3f;
        }

        LevelUpSkill();
    }


    public bool CanBuySkill()
    {
        if (cost <= StatsController.Instance.UpgradePoints && level <= maxLevel - 1)
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

    public void CanUpgrade()
    {
        if (cost <= StatsController.Instance.UpgradePoints && level <= maxLevel - 1)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    private void LevelUpSkill()
    {
        level++;
        //cost = level + 1;
        cost = 5;

        levelText.text = "Poziom " + level;
        costText.text = "Koszt " + cost;

        GetComponent<AudioSource>().PlayOneShot(purchaseSound);

        CanBuySkill();
    }

}
public enum SkillType
{
    Health,
    Mana,
    CrowsDamage,
    CrowsCooldown,
    CrowsSpeed,
    MaxCrowsNumber,
    DashCooldown
}
