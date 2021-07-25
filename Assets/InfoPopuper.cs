using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPopuper : MonoBehaviour
{
    private Animator animator;
    public GameObject holder;
    private bool wasPlayed = false;

    void Start()
    {
       animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<PlayerMovement>() && !wasPlayed)
        {
            wasPlayed = true;
            animator.SetTrigger("ShowPopup");
        }
    }


}
