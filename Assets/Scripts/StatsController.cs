using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StatsController : Character
{
    public static StatsController Instance = null;

    [SerializeField] private float maxMana;
    [SerializeField] private float maxFaith;

    private float mana;
    private int upgradePoints;
    private float faith;

    // CROWS
    private int maxCrowsNumber;
    private float timeToSpawnCrow;
    private float crowDamage;

    public float HP { get => hp; set => hp = value; }
    public float Mana { get => mana; set => mana = value; }
    public int UpgradePoints { get => upgradePoints; set => upgradePoints = value; }
    public float Faith { get => faith; set => faith = value; }
    public float MaxFaith { get => maxFaith; }
    public float MaxHP { get => maxHp;}
    public float MaxMana { get => maxMana;}

    // CROWS GETTERS && SETTERS
    public int MaxCrowsNumber { get => maxCrowsNumber; set => maxCrowsNumber = value; }
    public float TimeToSpawnCrow { get => timeToSpawnCrow; set => timeToSpawnCrow = value; }
    public float CrowDamage { get => crowDamage; set => crowDamage = value; }

    public Action hpChanged;
    public Action manaChanged;
    public Action upgradePointsChanged;
    public Action faithChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public override void Start()
    {
        base.Start();
        Mana = maxMana;
        UpgradePoints = 0;
        Faith = 0;
        MaxCrowsNumber = 1;
        TimeToSpawnCrow = 3f;
        CrowDamage = 10;
        HUDController.Instance.UpdateWholeUI();
        UpdateDebugDisplay();
    }

    public void UpdateDebugDisplay()
    {
        if (TestingButtons.Instance == null)
            return;

        TestingButtons.Instance.statsText.text = 
              "HP: " + HP
            + "\nMax HP: " + MaxHP
            + "\nMana: " + Mana
            + "\nMax mana: " + MaxMana
            + "\nUpgrade points: " + UpgradePoints
            + "\nFaith: " + Faith
            + "\nCrow damage: " + CrowDamage
            + "\nMax crows number: " + MaxCrowsNumber
            + "\nTime to spawn crow: " + TimeToSpawnCrow;

    }

    #region HP
    public override void GetHit(float value)
    {
        base.GetHit(value);
        hpChanged.Invoke();
        UpdateDebugDisplay();
    }

    public override void AddHp(float value)
    {
        base.AddHp(value);
        hpChanged.Invoke();
        UpdateDebugDisplay();
    }

    public void AddMaxHP(float value)
    {
        maxHp += value;
        hpChanged.Invoke();
        UpdateDebugDisplay();
    }
    #endregion

    #region Mana
    public void AddMana(float value)
    {
        Mana += value;
        if (Mana > maxMana)
        {
            Mana = maxMana;
        }
        manaChanged.Invoke();
        UpdateDebugDisplay();
    }
    public void AddMaxMana()
    {
        maxMana += 1;
        if (maxMana > HUDController.Instance.manaOrbs.Length)
        {
            maxMana = HUDController.Instance.manaOrbs.Length;
        }
        manaChanged.Invoke();
        UpdateDebugDisplay();
    }

    public void SpendMana(float value)
    {
        Mana -= value;
        if (Mana < 0)
            Mana = 0;
        manaChanged.Invoke();
        UpdateDebugDisplay();
    }

    public bool HasEnoughMana(float value)
    {
        if (value <= Mana)
            return true;
        else
            return false;
    }
    #endregion

    #region Faith
    public void AddFaith(float value)
    {
        Faith += value;
        if(Faith > maxFaith)
        {
            Faith = maxFaith;
            Debug.Log("MAXIMUM FAITH WAS ACHIEVED.");
        }
        faithChanged.Invoke();
        UpdateDebugDisplay();
    }
    #endregion

    #region UpgradePoints
    public void AddUpgradePoints(int value)
    {
        UpgradePoints += value;
        upgradePointsChanged.Invoke();
        UpdateDebugDisplay();
    }

    public void SpendUpgradePoints(int value)
    {
        UpgradePoints -= value;
        if (UpgradePoints < 0)
            UpgradePoints = 0;
        upgradePointsChanged.Invoke();
        UpdateDebugDisplay();
    }

    public bool HasEnoughUpgradePoints(int value)
    {
        if (value <= UpgradePoints)
            return true;
        else
            return false;
    }
    #endregion

    #region Crows
    public void UpgradeCrowDamage(float bonus)
    {
        CrowDamage += bonus;
        UpdateDebugDisplay();
    }

    public void UpgradeCrowCooldown(float bonus)
    {
        TimeToSpawnCrow -= bonus;
        if (TimeToSpawnCrow <= 0)
            TimeToSpawnCrow = 0.25f;
        UpdateDebugDisplay();
    }

    public void UpgradeCrowsMaxNumber()
    {
        MaxCrowsNumber++;
        UpdateDebugDisplay();
    }

    #endregion
}
