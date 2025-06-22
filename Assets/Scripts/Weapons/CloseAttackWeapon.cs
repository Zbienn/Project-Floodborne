using UnityEngine;
using UnityEngine.WSA;

public class CloseAttackWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float attackCounter, direction;
    private AudioSource sfx;
     
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        SetStats();
    }

    void Update()
    {
        if (StatsUpdated == true)
        {
            StatsUpdated = false;
            SetStats();
        }

        attackCounter -= Time.deltaTime;
        if(attackCounter <= 0)
        {
            attackCounter = Stats[WeaponLevel].TimeBetweenAttacks;


            direction = Input.GetAxisRaw("Horizontal");
            if (direction != 0)
            {
                if(direction > 0)
                {
                    damager.transform.rotation = Quaternion.identity;
                } else
                {
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f); 
                }
            }

            sfx.Play();
            Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);
            for (int i = 1; i < Stats[WeaponLevel].Amount; i++)
            {
                float newRotation = (360 / Stats[WeaponLevel].Amount) * i;
                Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f, damager.transform.eulerAngles.z + newRotation), transform).gameObject.SetActive(true);
            }
        }
    }

    void SetStats()
    {
        damager.BaseDamage = Stats[WeaponLevel].Damage;
        damager.Duration = Stats[WeaponLevel].Duration;
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].Range;

        attackCounter = 0f;
    }
}
