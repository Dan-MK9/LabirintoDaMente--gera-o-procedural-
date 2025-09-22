using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]

public class PlayerManager : MonoBehaviour
{

    public float moveSpeed = 4f;

    public float jumpForce = 6f;

    private Rigidbody2D rb;

    private int jumpCount;

    private int maxJumps = 1;

    private float moveInput;

    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpCount++;
        }
    }

    private void FixedUpdate()
    {
        float targetVelocity = moveInput != 0 ? moveInput * moveSpeed : 0f;

        rb.linearVelocity = new Vector2(targetVelocity, rb.linearVelocity.y);

        if (isGrounded)
        {
            jumpCount = 0;
        }

        isGrounded = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }    
    }
}