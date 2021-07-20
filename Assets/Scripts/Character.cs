using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] protected float maxHp;

    protected float hp;

    public virtual void Start()
    {
        hp = maxHp;
    }

    public virtual void GetHit(float value)
    {
        hp -= value;

        if (hp <= 0)
            Destroy(this.gameObject);
    }

    public virtual void AddHp(float value)
    {
        hp += value;

        if (hp > maxHp)
            hp = maxHp;
    }
}
