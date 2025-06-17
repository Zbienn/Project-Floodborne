using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{

    [SerializeField] private float baseDamage;

    [SerializeField] private float duration;

    private float growSpeed = 5f;
    private Vector3 targetSize;

    [SerializeField] private bool shouldKnockBack;

    [SerializeField] private bool destroyParent;

    public float BaseDamage { get => baseDamage; set => baseDamage = Mathf.Max(0f, value); }
    public float Duration { get => duration; set => duration = Mathf.Max(0f, value); }
    public float GrowSpeed { get => growSpeed; set => growSpeed = Mathf.Max(0f, value); }

    void Start()
    {
        
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;

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

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyScript>().TakeDamage(baseDamage, shouldKnockBack);
        }
    }
}
