using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    [Header("References")]
    [SerializeField] private Image hpBar;
    [SerializeField] public Image[] manaOrbs;
    [SerializeField] private Image faithBar;
    [SerializeField] private TextMeshProUGUI upgradePointsText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        StatsController.Instance.hpChanged += UpdateHP;
        StatsController.Instance.manaChanged += UpdateMana;
        StatsController.Instance.faithChanged += UpdateFaith;
        StatsController.Instance.upgradePointsChanged += UpdateUpgradePoints;
    }

    public void UpdateWholeUI()
    {
        UpdateHP();
        UpdateMana();
        UpdateFaith();
        UpdateUpgradePoints();
    }

    public void UpdateHP()
    {
        hpBar.fillAmount = StatsController.Instance.HP / StatsController.Instance.MaxHP;
    }

    public void UpdateMana()
    {
        for (int i = 0; i < manaOrbs.Length; i++)
        {
            if (i < StatsController.Instance.Mana && StatsController.Instance.Mana <= StatsController.Instance.MaxMana)
                manaOrbs[i].color = Color.cyan;
            else if(i < StatsController.Instance.MaxMana)
                manaOrbs[i].color = Color.gray;
            else
                manaOrbs[i].color = new Color(0,0,0,0);

        }
    }

    public void UpdateFaith()
    {
        faithBar.fillAmount = StatsController.Instance.Faith / StatsController.Instance.MaxFaith;
    }

    public void UpdateUpgradePoints()
    {
        upgradePointsText.text = StatsController.Instance.UpgradePoints.ToString() + " wolne pkt";
    }
}
