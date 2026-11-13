using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] public float jumpingPower;
    [SerializeField] private float timeLimit;

    public float horizontal;
    public bool movementBlocked = false;
    private bool facingRight = true;
    
    [SerializeField] private float timer;
    private float timerSpeed;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LightManager light;

    [SerializeField] private Sprite[] playerState;
    [SerializeField] private SpriteRenderer playerSprite;


    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        timer = timeLimit;
        Time.timeScale = 1;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        playerSprite.sprite = playerState[0];
    }

    private void Update()
    {

        timer -= timerSpeed * Time.deltaTime;
        if (timer <= 0)
        {
            StartCoroutine(Die());
            return;
        }
        else if (timer <= timeLimit * 0.33f)
        {
            playerSprite.sprite = playerState[2];
            Debug.Log("33%");
        }
        else if(timer <= timeLimit * 0.66f && playerSprite != playerState[1])
        {
            playerSprite.sprite = playerState[1];
            Debug.Log("66%");
        }
        else
        {
            playerSprite.sprite = playerState[0];
        }



        if (horizontal == 0 && rb.linearVelocity.y == 0)
        {
            light.Flare();
            timerSpeed = 1f;

        }
        else
        {
            light.Dim();
            timerSpeed = 0.5f;
        }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            StartCoroutine(Die());
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            Win();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Waga Baga Bobo");
            Destroy(collision.gameObject);
            timer += 30f;
            if (timer >= timeLimit)
            {
                timer = timeLimit;
            }
        }
    }

    private void Win()
    {
        Debug.Log("You win!");
    }

    private IEnumerator Die()
    {
        timer = 0;
        movementBlocked = true;
        playerSprite.sprite = playerState[3];
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
