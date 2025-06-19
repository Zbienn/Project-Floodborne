using System.Collections.Generic;
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

    [SerializeField] private bool damageOverTime;
    [SerializeField] private float timeBetweenDamage;
    private float damageCounter;

    private List<EnemyScript> enemiesInRange = new List<EnemyScript>();

    public float BaseDamage { get => baseDamage; set => baseDamage = Mathf.Max(0f, value); }
    public float Duration { get => duration; set => duration = Mathf.Max(0f, value); }
    public float GrowSpeed { get => growSpeed; set => growSpeed = Mathf.Max(0f, value); }
    public float TimeBetweenDamage { get => timeBetweenDamage; set => timeBetweenDamage = value; }

    void Start()
    {
        //Destroy(gameObject, Duration);

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

        if(damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0)
            {
                damageCounter = TimeBetweenDamage;

                for(int i = 0; i < enemiesInRange.Count; i++)
                {
                    if(enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(BaseDamage, shouldKnockBack);
                    } else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
     

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageOverTime == false) { 
        
            if(collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyScript>().TakeDamage(baseDamage, shouldKnockBack);
            }

        } else
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<EnemyScript>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(damageOverTime == true)
        {
            if(collision.tag == "Enemy") enemiesInRange.Remove(collision.GetComponent<EnemyScript>());
        }
    }
}
