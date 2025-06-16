using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private EnemyData data;
    private float currentHealth;
    private float currentSpeed;

    public int DamageAmount => data.DamageAmount;

    [SerializeField] private float knockBackTime = 0.5f;
    private float knockBackCounter;

    [SerializeField] private DamageNumberController damageNumberController;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = data.MaxHealth;
        currentSpeed = data.Speed;
    }

    void FixedUpdate()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            if (currentSpeed > 0)
            {
                currentSpeed = -currentSpeed * 2f;
            }
            if (knockBackCounter <= 0)
            {
                currentSpeed = Mathf.Abs(currentSpeed / 2f);
            }
        }

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        rb.MovePosition(rb.position + currentSpeed * Time.deltaTime * direction);

        if (direction.x != 0)
            spriteRenderer.flipX = direction.x < 0;
    }


    public void TakeDamage(float amount, bool shouldKnockBack)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            EnemyXPDropper dropper = GetComponent<EnemyXPDropper>();
            if (dropper != null)
            {
                dropper.DropXP();
            }

            Destroy(gameObject);
        }

        DamageNumberController.instance.SpawnDamage(amount, transform.position);

        if (shouldKnockBack)
        {
            knockBackCounter = knockBackTime;
        }
    }

}
