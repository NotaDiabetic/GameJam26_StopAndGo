using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] public float jumpingPower;

    public float horizontal;
    private bool movementBlocked = false;
    private bool facingRight = true;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if (movementBlocked) return;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if ((horizontal > 0 && facingRight) || horizontal < 0 && !facingRight) Flip();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        facingRight = !facingRight;
    }

}
