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

    private AudioSource sfx;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
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
            sfx.Play();

            //Instantiate(boatToSpawn, boatToSpawn.position, boatToSpawn.rotation, holder).gameObject.SetActive(true);
            for (int i = 0; i < Stats[WeaponLevel].Amount; i++)
            {
                float newRotation = (360 / Stats[WeaponLevel].Amount) * i;
                Instantiate(boatToSpawn, boatToSpawn.position, Quaternion.Euler(0f, 0f, newRotation), holder).gameObject.SetActive(true);
            }
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
