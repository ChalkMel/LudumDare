using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Sprite walkSprite1;
    public Sprite walkSprite2;
    public float animationSpeed = 0.2f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private float timer;
    private bool isSprite1 = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        rb.linearVelocity = movement * moveSpeed;

        if (movement.magnitude > 0)
        {
            timer += Time.deltaTime;
            if (timer >= animationSpeed)
            {
                timer = 0;
                isSprite1 = !isSprite1;
                spriteRenderer.sprite = isSprite1 ? walkSprite1 : walkSprite2;
            }

            if (movement.x != 0)
            {
                spriteRenderer.flipX = movement.x < 0;
            }
        }
    }
}