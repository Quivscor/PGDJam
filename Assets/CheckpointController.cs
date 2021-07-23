using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController Instance;

    private Transform lastCheckpoint;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetCheckpoint(Transform transform)
    {
        lastCheckpoint = transform;
    }

    public void RespawnPlayer()
    {
        FindObjectOfType<PlayerMovement>().gameObject.transform.position = lastCheckpoint.position;
        StatsController.Instance.HP = StatsController.Instance.MaxHP;
    }
}
