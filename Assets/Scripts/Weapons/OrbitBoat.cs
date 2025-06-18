using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class OrbitBoat : Weapon
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform holder, boatToSpawn;

    [SerializeField] private float cooldown;
    private float spawnCounter;

    [SerializeField] private EnemyDamager damager;

    [SerializeField] ExperienceUIController uiController;

    void Start()
    {
        SetStats();
    }

    void Update()
    {
        //holder.rotation = Quaternion.Euler(0F, 0F, holder.rotation.eulerAngles.z + (rotateSpeed*Time.deltaTime));
        holder.rotation = Quaternion.Euler(0F, 0F, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * Stats[WeaponLevel].Speed));


        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = cooldown;

            Instantiate(boatToSpawn, boatToSpawn.position, boatToSpawn.rotation, holder).gameObject.SetActive(true);
        }

        if(StatsUpdated == true)
        {
            StatsUpdated = false;
            SetStats();
        }
    }

    public void SetStats()
    {
        damager.BaseDamage = Stats[WeaponLevel].Damage;
        transform.localScale = Vector3.one * Stats[WeaponLevel].Range;
        cooldown = Stats[WeaponLevel].TimeBetweenAttacks;
        damager.Duration = Stats[WeaponLevel].Duration;

        spawnCounter = 0f;
    }
}
