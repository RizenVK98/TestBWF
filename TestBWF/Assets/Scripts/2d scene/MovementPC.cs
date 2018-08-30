using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPC : MonoBehaviour {
    public float horizontalSpeed;
    float speedX;
    public float verticalImpulse;
    Rigidbody2D rb;
    SpriteRenderer sr;
    bool isGrounded;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.A))
        {
            speedX = -horizontalSpeed;
            sr.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            speedX = horizontalSpeed;
            sr.flipX = false;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, verticalImpulse), ForceMode2D.Impulse);
        }
        transform.Translate(speedX, 0, 0);
        speedX = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
