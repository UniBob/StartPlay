using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMovement : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        // if (anim.GetBool("isAlive"))

        Move();
        Rotate();

    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if (new Vector2(inputX, inputY) != Vector2.zero)
            anim.SetBool("isMoving", true);
        else anim.SetBool("isMoving", false);
        rb.velocity = new Vector2(inputX, inputY) * speed;
    }

    private void Rotate()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPosition - transform.position;
        transform.up = -direction;
    }
}
