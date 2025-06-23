using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float spawnTime, spawnCounter;

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

    private void Update()
    {
        if (StatsUpdated == true)
        {
            StatsUpdated = false;
            SetStats();
        }

        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0f)
        {
            spawnCounter = spawnTime;
            sfx.Play();
            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
    }

    void SetStats()
    {
        damager.BaseDamage = Stats[WeaponLevel].Damage + offsets["Damage"];
        damager.Duration = Stats[WeaponLevel].Duration + offsets["Duration"]; ;
        damager.TimeBetweenDamage = Stats[WeaponLevel].Speed + offsets["Speed"];
        Stats[WeaponLevel].Range += offsets["Area"];
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].Range;
        spawnTime = Stats[WeaponLevel].TimeBetweenAttacks - offsets["Cooldown"];
        spawnCounter = 0f;
    }

}
