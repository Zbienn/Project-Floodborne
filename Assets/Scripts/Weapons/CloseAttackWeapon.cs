using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class CloseAttackWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float attackCounter, direction;
    private AudioSource sfx;
    private StatsForJSON[] array;
    private Dictionary<string, float> offsets;
    private int amountOffset = 0;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        array = JsonHelper.FromJson<StatsForJSON>(PlayerPrefs.GetString("StatsArray", ""));
        offsets = new Dictionary<string,float>()
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
                    damager.transform.rotation = Quaternion.identity;
                else
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
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
        Stats[WeaponLevel].TimeBetweenAttacks -= offsets["Cooldown"];
        damager.BaseDamage = Stats[WeaponLevel].Damage + offsets["Damage"];
        damager.Duration = Stats[WeaponLevel].Duration + offsets["Duration"];
        Stats[WeaponLevel].Range += offsets["Area"];
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].Range;
        Stats[WeaponLevel].Amount += amountOffset;
        attackCounter = 0f;
    }
}
