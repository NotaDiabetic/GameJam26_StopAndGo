using System.Collections;
using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Decay());
        }
    }
}
