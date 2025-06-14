using UnityEngine;

public class OrbitBoat : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform holder, boatToSpawn;

    [SerializeField] private float cooldown;
    private float spawnCounter;

    void Start()
    {
        
    }

    void Update()
    {
        holder.rotation = Quaternion.Euler(0F, 0F, holder.rotation.eulerAngles.z + (rotateSpeed*Time.deltaTime));

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = cooldown;

            Instantiate(boatToSpawn, boatToSpawn.position, boatToSpawn.rotation, holder).gameObject.SetActive(true);
        }
    }
}
