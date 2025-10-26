using System.Collections;
using UnityEngine;

public class DuckFall : MonoBehaviour
{
    public float walkSpeed = 2f;
    public string holeTag = "Hole";
    public float fallDelay = 1.2f;
    public float resetDelay = 0.5f;

    private Vector3 startPos;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isFalling = false;
    private Transform holeCenter;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.gravityScale = 0f; 
        if (anim != null) anim.Play("Duck_Walk");
    }

    void Update()
    {
        if (!isFalling)
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(holeTag) && !isFalling)
        {
            holeCenter = other.transform;
            StartCoroutine(WalkAndFall());
        }
    }

    IEnumerator WalkAndFall()
    {
        isFalling = true;

        while (Mathf.Abs(transform.position.x - holeCenter.position.x) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(holeCenter.position.x, transform.position.y, transform.position.z),
                walkSpeed * Time.deltaTime
            );
            yield return null;
        }

        if (anim != null) anim.enabled = false;
        rb.gravityScale = 3f;
        yield return new WaitForSeconds(fallDelay);

        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(resetDelay);
        transform.position = startPos;

        if (anim != null)
        {
            anim.enabled = true;
            anim.Play("Duck_Walk");
        }

        isFalling = false;
    }
}