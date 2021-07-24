using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDetector : MonoBehaviour
{
    public Action<Transform> OnPlayerDetected;
    public Action OnPlayerLeaveAggroRange;

    CircleCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
            OnPlayerDetected?.Invoke(other.transform.root);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            OnPlayerLeaveAggroRange?.Invoke();
    }

    public bool ForceCheckPlayerInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, collider.radius);
        foreach(Collider2D col in hits)
        {
            if (col.tag == "Player")
                return true;
        }
        return false;
    }
}
