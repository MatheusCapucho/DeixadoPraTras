using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (rb.velocity == Vector2.zero)
            anim.SetTrigger("Idle");
        else
        if (rb.velocity.x != 0)       
            anim.SetTrigger("Horizontal");
        else
        if (rb.velocity.y != 0)
            anim.SetTrigger("Vertical");

    }
}
