using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<PlayerMovement>())
        {
            CheckpointController.Instance.SetCheckpoint(this.gameObject.transform);
        }
    }
}
