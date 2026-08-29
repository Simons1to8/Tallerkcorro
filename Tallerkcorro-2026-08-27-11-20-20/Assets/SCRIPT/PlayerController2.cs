using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController2 : MonoBehaviour
{
    public float direction;
    public float speed;
    public Rigidbody2D rb;

    [SerializeField] private float jumpForce;
    public bool canJump;

    [SerializeField] private Transform GroundCheck2;
    [SerializeField] private float GroundCheckRadius;
    [SerializeField] private LayerMask GroundLayer;

    public bool isFacingRight;

    [SerializeField] private LayerMask MudLayer;
    [SerializeField] private float mudSpeed = 2f;

    public int coins = 0;
    public TMP_Text coinText;

    public GameObject Key;
    public GameObject Door;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coinText.text = "Coins: " + coins;
    }

    void Update()
    {
        canJump = Physics2D.OverlapCircle(GroundCheck2.position, GroundCheckRadius, GroundLayer);

        if (!isFacingRight && direction > 0f)
        {
            Flip();
        }
        else if (isFacingRight && direction < 0f)
        {
            Flip();
        }

        bool onMud = Physics2D.OverlapCircle(GroundCheck2.position, GroundCheckRadius, MudLayer);

        float currentSpeed = onMud ? mudSpeed : speed;

        rb.linearVelocity = new Vector2(direction * currentSpeed, rb.linearVelocityY);
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.SetActive(false);
            Door.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            coins = coins + 1;
            coinText.text = "Coins: " + coins;
            collision.gameObject.SetActive(false);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}