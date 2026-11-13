using System.Collections;
using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private IEnumerator Decay()
    {
        anim.SetTrigger("decay");
        yield return new WaitForSeconds(2.65f);
        Destroy(gameObject);
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Decay());
            StartCoroutine(Fall());
        }
    }
}
