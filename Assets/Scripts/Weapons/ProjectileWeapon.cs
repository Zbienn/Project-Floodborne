using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    [SerializeField] private Projectile projectile;

    private float shotCounter;

    [SerializeField] private float weaponRange;
    [SerializeField] private LayerMask whatIsEnemy;

    private AudioSource sfx;
    private StatsForJSON[] array;
    private Dictionary<string, float> offsets;
    private int amountOffset = 0;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        array = JsonHelper.FromJson<StatsForJSON>(PlayerPrefs.GetString("StatsArray", ""));
        offsets = new Dictionary<string, float>()
        {
            { "Cooldown", array[1].level * 0.2f },
            { "Area", array[2].level * 0.5f },
            { "Speed", array[3].level * 0.2f },
            { "Duration", array[4].level * 0.3f },
            { "Damage", array[9].level * 0.5f }
        };
        amountOffset += array[5].level;
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
                sfx.Play();
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
        Stats[WeaponLevel].TimeBetweenAttacks -= offsets["Cooldown"];
        damager.BaseDamage = Stats[WeaponLevel].Damage + offsets["Damage"];
        damager.Duration = Stats[WeaponLevel].Duration + offsets["Duration"];
        Stats[WeaponLevel].Range += offsets["Area"];
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].Range;
        Stats[WeaponLevel].Amount += amountOffset;
        shotCounter = 0f;
        projectile.MoveSpeed = Stats[WeaponLevel].Speed + offsets["Speed"];
    }
}
