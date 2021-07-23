using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] protected float maxHp;
    public Action<Character> OnDeath;

    protected float hp;

    public virtual void Start()
    {
        hp = maxHp;
    }

    public virtual void GetHit(float value)
    {
        hp -= value;

        if (hp <= 0)
            Die();
    }

    public virtual void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(this.gameObject);
    }

    public virtual void AddHp(float value)
    {
        hp += value;

        if (hp > maxHp)
            hp = maxHp;
    }
}
