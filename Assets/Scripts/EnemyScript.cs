using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private EnemyData data;
    private float currentHealth;

    public int DamageAmount => data.DamageAmount;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = data.MaxHealth;
    }

    void FixedUpdate()
    {
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        rb.MovePosition(rb.position + data.Speed * Time.deltaTime * direction);

        if (direction.x != 0)
            spriteRenderer.flipX = direction.x < 0;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
