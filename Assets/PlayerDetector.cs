using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDetector : MonoBehaviour
{
    public Action<Transform> OnPlayerDetected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            OnPlayerDetected?.Invoke(collision.transform.root);
    }
}
