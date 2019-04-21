﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public LayerMask jumpLayers;
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool facingLeft;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Ef leikmaður snýr til hægri, snúa honum til vinstri og öfugt
    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = -currentScale.x;
        transform.localScale = currentScale;
        facingLeft = !facingLeft;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, jumpLayers);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);

        // Færa leikmann
        transform.position += movement * speed * Time.fixedDeltaTime;

        // Athuga hvort leikmaður sé á jörðinni
        if (IsGrounded()) {
            // Ef hann velur að hoppa, setja hopp-animation af stað
            if (Input.GetButton("Jump"))
            {
                Vector2 jump = new Vector2(0f, jumpSpeed);
                rb2d.AddForce(jump, ForceMode2D.Impulse);
                animator.SetBool("Jump", true);
            }
            // Ef hann er á jörðinni og ekki að hoppa, stoppa hopp-animation
            else
            {
                animator.SetBool("Jump", false);
            }
        }

        // Ef leikmaður er ekki að hoppa, færir sig til vinstri og sneri áður til hægri (eða öfugt), snúa honum við
        if (!animator.GetBool("Jump") &&
            (moveHorizontal < 0 && !facingLeft || moveHorizontal > 0 && facingLeft))
        {
            Flip();
        }

        // Gönguhraði fyrir animation
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
    }
}
