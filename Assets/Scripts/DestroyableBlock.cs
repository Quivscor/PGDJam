using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBlock : Character
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile p;
        if(collision.transform.TryGetComponent(out p))
        {
            if(p.userTag == "Player" && StatsController.Instance.canDestroyBlockades)
            {
                GetHit(maxHp);
            }
        }
    }
}
