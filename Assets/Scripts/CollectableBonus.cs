using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBonus : MapEvent
{
    public NewAbility newAbilityType;
    public GameObject itemHolder;
    private bool canBeTaken = false;

    public override void StopEvent()
    {
        itemHolder.SetActive(true);
        canBeTaken = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<PlayerMovement>() && Input.GetKey(KeyCode.E) && canBeTaken)
        {
            if(newAbilityType == NewAbility.Dash)
            {
                StatsController.Instance.canDash = true;
            }

            if (newAbilityType == NewAbility.BreakingWood)
            {
                StatsController.Instance.canDestroyBlockades = true;
            }

            Destroy(this.gameObject);
        }
    }

    public enum NewAbility
    {
        Dash,
        BreakingWood
    }
}
