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
    [SerializeField] public Image[] healthOrbs;
    [SerializeField] private Image faithBar;
    [SerializeField] private TextMeshProUGUI upgradePointsText;
    [SerializeField] private GameObject upgradesHUD;
    [SerializeField] private Animator vignetteAnimator;
    [SerializeField] private TextMeshProUGUI bonusText;
    [SerializeField] private Animator bonusTextAnimator;

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

    public void ToggleUpgrades(bool toggle)
    {
        upgradesHUD.SetActive(toggle);
        PlayerInput.BlockPlayerInput(toggle);

    }

    public void UpdateWholeUI()
    {
        UpdateHP();
        UpdateMana();
        UpdateFaith();
        UpdateUpgradePoints();
    }

    public void GetHitVignette()
    {
        vignetteAnimator.SetTrigger("Blink");
    }

    public void UpdateHP()
    {
        hpBar.fillAmount = StatsController.Instance.HP / StatsController.Instance.MaxHP;

        for (int i = 0; i < healthOrbs.Length; i++)
        {
            healthOrbs[i].gameObject.SetActive(true);
            if (i < StatsController.Instance.HP && StatsController.Instance.HP <= StatsController.Instance.MaxHP)
                healthOrbs[i].color = new Color(0.5849056f, 0f, 0f, 1f);
            else if (i < StatsController.Instance.MaxHP)
                healthOrbs[i].color = Color.white;
            else
                healthOrbs[i].gameObject.SetActive(false);

        }
    }

    public void AnimateBonusPoints(int bonus)
    {
        bonusText.text = "+ " + bonus + " pkt Bogobojności";
        bonusTextAnimator.SetTrigger("BonusPopup");
    }

    public void AnimateHealth(int i)
    {
        healthOrbs[i].gameObject.GetComponent<Animator>().SetTrigger("HpChange");
    }

    public void UpdateMana()
    {
        for (int i = 0; i < manaOrbs.Length; i++)
        {
            if (i < StatsController.Instance.Mana && StatsController.Instance.Mana <= StatsController.Instance.MaxMana)
                manaOrbs[i].color = new Color(0.5849056f, 0f, 0f, 1f);
            else if (i < StatsController.Instance.MaxMana)
                manaOrbs[i].color = Color.white;
            else
                manaOrbs[i].color = new Color(0, 0, 0, 0);

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
