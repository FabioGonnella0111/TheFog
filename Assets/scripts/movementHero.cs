using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class scrip : MonoBehaviour
{
    Rigidbody2D body;
    Animator anim;

    public Transform Skeleton;
    public float moveSpeed = 8f;
    private float speed = 0;
    public float JumpForce = 6f;

    bool isJumping = false;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = moveSpeed;
    }


    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(Vector2.right.x * speed * h, body.velocity.y);
        body.velocity = velocity;

        if (h > .1f || h < -.1f)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        if (velocity.x > 0)
        {
            body.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velocity.x < 0)
        {
            body.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Jump()
    {
        if (isJumping)
        {
            if (body.velocity.y == 0)
            {
                isJumping = false;
                anim.SetBool("isJumping", false);
            }
        }
        else
        {
            if (Input.GetAxis("Jump") > 0)
            {
                body.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                isJumping = true;
                anim.SetBool("isJumping", true);
            }
        }
        if(transform.position.y < 0)
        {
            anim.SetBool("isFall", true);
        }
    }

   

}
