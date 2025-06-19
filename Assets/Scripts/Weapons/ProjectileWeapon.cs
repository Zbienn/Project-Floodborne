using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    [SerializeField] private Projectile projectile;

    private float shotCounter;

    [SerializeField] private float weaponRange;
    [SerializeField] private LayerMask whatIsEnemy;

    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (StatsUpdated == true)
        {
            StatsUpdated = false;
            SetStats();
        }

        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            shotCounter = Stats[WeaponLevel].TimeBetweenAttacks;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * Stats[WeaponLevel].Range, whatIsEnemy);

            if (enemies.Length > 0) {
                for (int i = 0; i < Stats[WeaponLevel].Amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90; //Tem de ser ajustado para ficar no ângulo certo
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);

                }
            }
        }


    }

    void SetStats()
    {
        damager.BaseDamage = Stats[WeaponLevel].Damage;
        damager.Duration = Stats[WeaponLevel].Duration;
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].Range;

        shotCounter = 0f;
        projectile.MoveSpeed = Stats[WeaponLevel].Speed;
    }
}
