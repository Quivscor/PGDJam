using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image[] manaOrbs;
    [SerializeField] private Image faithBar;
    [SerializeField] private TextMeshProUGUI upgradePointsText;

    private void Awake()
    {
        StatsController.Instance.hpChanged += UpdateHP;
        StatsController.Instance.manaChanged += UpdateMana;
        StatsController.Instance.faithChanged += UpdateFaith;
        StatsController.Instance.upgradePointsChanged += UpdateUpgradePoints;
    }

    void Start()
    {
        
    }

    public void UpdateHP()
    {
        hpBar.fillAmount = StatsController.Instance.HP / StatsController.Instance.MaxHP;
    }

    public void UpdateMana()
    {
        for (int i = 0; i < StatsController.Instance.MaxMana; i++)
        {
            if (i <= StatsController.Instance.Mana)
                manaOrbs[i].color = Color.cyan;
            else
                manaOrbs[i].color = Color.gray;
        }
    }

    public void UpdateFaith()
    {
        faithBar.fillAmount = StatsController.Instance.Faith / StatsController.Instance.MaxFaith;
    }

    public void UpdateUpgradePoints()
    {
        upgradePointsText.text = StatsController.Instance.UpgradePoints.ToString();
    }
}
