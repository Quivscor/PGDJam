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

    public float HP { get => hp; set => hp = value; }
    public float Mana { get => mana; set => mana = value; }
    public int UpgradePoints { get => upgradePoints; set => upgradePoints = value; }
    public float Faith { get => faith; set => faith = value; }

    public float MaxFaith { get => maxFaith; }
    public float MaxHP { get => maxHp;}
    public float MaxMana { get => maxMana;}

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
    }

    #region HP
    public override void GetHit(float value)
    {
        base.GetHit(value);
        hpChanged.Invoke();
    }

    public override void AddHp(float value)
    {
        base.AddHp(value);
        hpChanged.Invoke();
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
    }

    public void SpendMana(float value)
    {
        Mana -= value;
        if (Mana < 0)
            Mana = 0;
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
    }
    #endregion

    #region UpgradePoints
    public void AddUpgradePoints(int value)
    {
        UpgradePoints += value;
    }

    public void SpendUpgradePoints(int value)
    {
        UpgradePoints -= value;
        if (UpgradePoints < 0)
            UpgradePoints = 0;
    }

    public bool HasEnoughUpgradePoints(int value)
    {
        if (value <= UpgradePoints)
            return true;
        else
            return false;
    }
    #endregion 
}
