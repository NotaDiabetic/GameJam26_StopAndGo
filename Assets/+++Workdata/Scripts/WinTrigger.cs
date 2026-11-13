using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private PlayerControls player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.movementBlocked = true;
            //win;
        }
    }
}
