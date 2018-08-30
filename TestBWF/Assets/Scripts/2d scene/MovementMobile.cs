using UnityEngine;

public class MovementMobile : MonoBehaviour {

    public float horizontalSpeed;
    float speedX;
    public float verticalImpulse;
    Rigidbody2D rb;
    SpriteRenderer sr;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void LeftButtonDown()
    {
        speedX = -horizontalSpeed;
        sr.flipX = true;
    }

    public void RightButtonDown()
    {
        speedX = horizontalSpeed;
        sr.flipX = false;
    }

    public void Stop()
    {
        speedX = 0;
    }

    public void OnClickJump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, verticalImpulse), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    { 
        transform.Translate(speedX, 0, 0);
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
