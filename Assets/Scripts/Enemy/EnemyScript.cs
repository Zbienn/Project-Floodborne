using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private CoinController coinController;

    [SerializeField] private AudioClip sfx;

    [SerializeField] private EnemyData data;
    private float currentHealth;
    private float currentSpeed;

    public int DamageAmount => data.DamageAmount;

    [SerializeField] private float knockBackTime = 0.5f;
    private float knockBackCounter;

    [SerializeField] private DamageNumberController damageNumberController;

    [Header("Coin Drop Settings")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinDropChance = 0.25f; // 25%

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (coinController == null) coinController = FindFirstObjectByType<CoinController>();

        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = data.MaxHealth;
        currentSpeed = data.Speed;

        StatsForJSON luckOffset = JsonHelper.FromJson<StatsForJSON>(PlayerPrefs.GetString("StatsArray", ""))[8];
        coinDropChance += luckOffset.level * 0.1f;
        if (coinDropChance > 1) coinDropChance = 1f;
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
        coinController.PlaySound(sfx);

        if (currentHealth <= 0)
        {
            // Drop XP if available
            EnemyXPDropper dropper = GetComponent<EnemyXPDropper>();
            if (dropper != null)
                dropper.DropXP();

            // 25% chance to drop a coin
            if (coinPrefab != null && Random.value <= coinDropChance)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        DamageNumberController.instance.SpawnDamage(amount, transform.position);
        if (shouldKnockBack)
            knockBackCounter = knockBackTime;
    }
}
