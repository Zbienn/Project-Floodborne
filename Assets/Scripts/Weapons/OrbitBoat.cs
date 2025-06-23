using System.Collections.Generic;
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
        damager.BaseDamage = Stats[WeaponLevel].Damage + offsets["Damage"];
        damager.Duration = Stats[WeaponLevel].Duration + offsets["Duration"];
        Stats[WeaponLevel].Range += offsets["Area"];
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].Range;
        cooldown = Stats[WeaponLevel].TimeBetweenAttacks - offsets["Cooldown"];
        Stats[WeaponLevel].Speed += offsets["Speed"];
        Stats[WeaponLevel].Amount += amountOffset;
        spawnCounter = 0f;
    }
}
