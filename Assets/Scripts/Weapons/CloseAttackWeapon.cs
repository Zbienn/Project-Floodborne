using UnityEngine;

public class CloseAttackWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float attackCounter, direction;
     
    void Start()
    {

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

            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                if(Input.GetAxisRaw("Horizontal") > 0)
                {
                    damager.transform.rotation = Quaternion.identity;
                } else
                {
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f); 
                }
                Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);
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
