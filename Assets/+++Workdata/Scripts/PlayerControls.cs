using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] public float jumpingPower;
    [SerializeField] private float timeLimit;

    [SerializeField] private float checkPointGain;

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

    [SerializeField] private Animator anim;

    [SerializeField] private Image bar;
    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject Lose;


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

        bar.fillAmount = timer / timeLimit;
        if (timer <= 0)
        {
            StartCoroutine(Die());
            return;
        }
        else if (timer <= timeLimit * 0.33f)
        {
            anim.SetTrigger("Low");
            //playerSprite.sprite = playerState[2];
            Debug.Log("33%");
        }
        else if(timer <= timeLimit * 0.66f && playerSprite != playerState[1])
        {
            anim.SetTrigger("Partial");
            //playerSprite.sprite = playerState[1];
            Debug.Log("66%");
        }
        else
        {
            anim.SetTrigger("Full");
            //playerSprite.sprite = playerState[0];
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
            WinGame();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Waga Baga Bobo");
            Destroy(collision.gameObject);
            timer += checkPointGain;
            if (timer >= timeLimit)
            {
                timer = timeLimit;
            }
        }
        if (collision.gameObject.CompareTag("Hazard"))
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator WinGame()
    {
        Debug.Log("You win!");
        movementBlocked = true;
        Win.SetActive(true);
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator Die()
    {
        anim.SetTrigger("dead");
        timer = 0;
        movementBlocked = true;
        Lose.SetActive(true);
        //playerSprite.sprite = playerState[3];
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
