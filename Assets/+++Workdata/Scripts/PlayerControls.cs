using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

}
