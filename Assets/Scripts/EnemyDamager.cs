using UnityEngine;

public class EnemyDamager : MonoBehaviour
{

    [SerializeField] private float baseDamage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyScript>().TakeDamage(baseDamage);
        }
    }
}
