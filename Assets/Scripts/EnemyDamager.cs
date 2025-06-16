using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damage;
    private float duration;
    public bool shouldKnockBack;
    public bool destroyParent;

    private float growSpeed = 5f;
    private Vector3 targetSize;

    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        // remover esta linha:
        // duration = weaponData.Duration;
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetStats(float damage, float duration)
    {
        this.damage = damage;
        this.duration = duration;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyScript>().TakeDamage(damage, shouldKnockBack);
        }
    }
}
