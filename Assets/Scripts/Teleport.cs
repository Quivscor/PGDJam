using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportDestination;
    [SerializeField] private GameObject teleportSprite;
    [SerializeField] private GameObject infoAboutTeleport;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<PlayerMovement>())
        {
            infoAboutTeleport.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<PlayerMovement>())
        {
            infoAboutTeleport.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<PlayerMovement>() && Input.GetKey(KeyCode.E))
        {
            collision.GetComponentInParent<PlayerMovement>().gameObject.transform.position = teleportDestination.position;
        }
    }

}
