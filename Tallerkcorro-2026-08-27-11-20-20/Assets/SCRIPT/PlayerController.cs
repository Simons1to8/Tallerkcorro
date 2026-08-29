using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float direction;
    public float speed;
    public Rigidbody2D rb;

    [SerializeField] private float jumpForce;
    public bool canJump;

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundCheckRadius;
    [SerializeField] private LayerMask GroundLayer;

    public bool isFacingRight;

    [SerializeField] private LayerMask MudLayer;
    [SerializeField] private float mudSpeed = 2f;

    public GameObject Key;
    public GameObject Door;

    public float health = 0f;

    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;


    private SpriteRenderer spriteRenderer;

    void Start()
    {
                spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        canJump = Physics2D.OverlapCircle(GroundCheck.position,GroundCheckRadius,GroundLayer);
        if (!isFacingRight && direction > 0f)
        {
            Flip();
        }
        else if (isFacingRight && direction < 0f)

        {
            Flip();
        }


        bool onMud = Physics2D.OverlapCircle( GroundCheck.position,GroundCheckRadius,MudLayer
        );

        float currentSpeed = onMud ? mudSpeed : speed;

        rb.linearVelocity = new Vector2(
            direction * currentSpeed,
            rb.linearVelocityY
        );
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.SetActive(false);
            Door.SetActive(false);
        }



        if (collision.gameObject.CompareTag("Health"))
        {
            if (health < 3f)
            {
                health = health + 1f;
                Debug.Log("Health actual: " + health);
                collision.gameObject.SetActive(false);

                if (health == 1f)
                {
                    Health1.SetActive(true);
                }

                if (health == 2f)
                {
                    Health2.SetActive(true);
                }

                if (health == 3f)
                {
                    Health3.SetActive(true);
                }
                 
                          
            }
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
