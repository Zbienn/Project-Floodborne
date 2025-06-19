using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private List<Weapon> unassignedWeapons, currentWeapons;
    [SerializeField] private int maxWeapons = 3;
    private List<Weapon> fullyLeveledWeapons = new List<Weapon>();

    public List<Weapon> UnassignedWeapons { get => unassignedWeapons; set => unassignedWeapons = value; }
    public List<Weapon> CurrentWeapons { get => currentWeapons; set => currentWeapons = value; }
    public int MaxWeapons { get => maxWeapons; set => maxWeapons = value; }
    public List<Weapon> FullyLeveledWeapons { get => fullyLeveledWeapons; set => fullyLeveledWeapons = value; }

    private void Start()
    {
        if(currentWeapons.Count == 0) AddWeapon(Random.Range(0, unassignedWeapons.Count));
    }

    public void AddWeapon(int weaponNum)
    {
        if (weaponNum < unassignedWeapons.Count)
        {
            currentWeapons.Add(unassignedWeapons[weaponNum]);

            unassignedWeapons[weaponNum].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNum);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);
        currentWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}
