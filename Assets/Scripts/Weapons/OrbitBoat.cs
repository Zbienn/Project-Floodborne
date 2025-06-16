using UnityEngine;

public class OrbitBoat : Weapon
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform holder;
    [SerializeField] private GameObject boatToSpawn;
    [SerializeField] private float spawnDistance = 2f;

    private float spawnCounter;

    void Update()
    {
        if (holder == null) return;

        holder.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0f)
        {
            spawnCounter = currentStats.Cooldown;

            if (boatToSpawn != null)
            {
                GameObject boat = Instantiate(boatToSpawn, holder);
                boat.transform.localPosition = new Vector3(spawnDistance, 0f, 0f);
                boat.transform.localRotation = Quaternion.identity;
                boat.SetActive(true);

                EnemyDamager damager = boat.GetComponent<EnemyDamager>();
                if (damager != null)
                {
                    damager.SetStats(currentStats.Damage, currentStats.Duration);
                }

            }
        }
    }
}
