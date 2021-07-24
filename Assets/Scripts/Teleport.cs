using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    [SerializeField] private GameObject[] enemiesToKill;
    [SerializeField] private Transform teleportDestination;
    [SerializeField] private GameObject teleportSprite;
    private bool isActive = false;
    public bool IsActive { get => isActive; set => isActive = value; }

    private void Update()
    {
        if(!isActive)
        {
            foreach (GameObject enemy in enemiesToKill)
            {
                if (enemy != null)
                    return;
                ActivateTeleport();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isActive && collision.GetComponentInParent<PlayerMovement>())
        {
            collision.GetComponentInParent<PlayerMovement>().gameObject.transform.position = teleportDestination.position;
        }
    }

    public void ActivateTeleport()
    {
        isActive = true;
        teleportSprite.SetActive(true);
    }
}
