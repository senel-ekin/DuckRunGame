using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce = 10f;

    bool isGrounded = true;

    [SerializeField] Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.speed = 0;
    }

    void Update()
    {
        if (!GamePlayManager.gameStarted) return; //Son Eklenen Kod
        animator.speed = 1;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }


    void Jump()
    {
        rb.velocity = new Vector2(0f, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;

        animator.SetTrigger("JumpTrigger");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
