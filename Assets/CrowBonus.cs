using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<PlayerMovement>())
        {
            StatsController.Instance.UpgradeCrowsMaxNumber();
            Destroy(this.gameObject);
        }
    }
}
