using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3 : MonoBehaviour
{
    public float direction;
    public float speed;
    public Rigidbody2D rb;

    [SerializeField] private float jumpForce;
    public bool canJump;

    [SerializeField] private Transform GroundCheck3;
    [SerializeField] private float GroundCheckRadius3;
    [SerializeField] private LayerMask GroundLayer3;


    public bool isFacingRight;
    private SpriteRenderer spriteRenderer;

    public GameObject Key;
    public GameObject Door;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        canJump = Physics2D.OverlapCircle(GroundCheck3.position, GroundCheckRadius3, GroundLayer3);

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocityY);

        if (!isFacingRight && direction > 0f)
        {
            Flip();
        }
        else if (isFacingRight && direction < 0f)
        {
            Flip();
        }
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

        if (collision.CompareTag("Red"))
        {
            spriteRenderer.color = Color.red;
        }

        if (collision.CompareTag("Purple"))
        {
            spriteRenderer.color = new Color(0.5f, 0f, 1f);
        }

        if (collision.CompareTag("White"))
        {
            spriteRenderer.color = Color.white;
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