using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingInfo : MonoBehaviour
{
    public Animator animator;
    public GameObject holder;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<PlayerMovement>())
        {
            holder.SetActive(true);
            PlayerInput.BlockPlayerInput(true);
            //animator.SetTrigger("Ending");
        }
    }
}
