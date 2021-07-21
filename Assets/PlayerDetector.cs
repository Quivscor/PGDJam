using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDetector : MonoBehaviour
{
    public Action<Transform> OnPlayerDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
            OnPlayerDetected?.Invoke(other.transform.root);
    }
}
