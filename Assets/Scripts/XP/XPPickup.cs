using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class XPPickup : MonoBehaviour
{
    [SerializeField] private float xpAmount = 5f;
    [SerializeField] private PlayerExperienceData playerExperience;
    private Transform target;
    private bool isBeingPulled = false;
    private float pullSpeed = 8f;

    private ExperienceUIController uiController;
    private PlayerWeapon playerWeapon;

    [SerializeField] private List<Weapon> weaponsToUpgrade;

    void Start()
    {
        if (uiController == null)
            uiController = FindFirstObjectByType<ExperienceUIController>();
        if (playerWeapon == null) playerWeapon = FindFirstObjectByType<PlayerWeapon>();
    }

    private void Update()
    {
        if (isBeingPulled && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, pullSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                if (playerExperience != null)
                {
                    bool leveledUp = playerExperience.AddXP(xpAmount);
                    if (leveledUp)
                        LeveledUp();
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isBeingPulled && collision.name == "PickUpRange")
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            isBeingPulled = true;
        }
    }

    void LeveledUp()
    {
        Debug.Log("Level Up!");
        uiController.LevelUpPanel.SetActive(true);

        Time.timeScale = 0f;

        //uiController.LevelUpButtons[1].UpdateButtonDisplay(playerWeapon.ActiveWeapon);
        /*uiController.LevelUpButtons[0].UpdateButtonDisplay(playerWeapon.CurrentWeapons[0]);
        uiController.LevelUpButtons[1].UpdateButtonDisplay(playerWeapon.UnassignedWeapons[0]);
        uiController.LevelUpButtons[2].UpdateButtonDisplay(playerWeapon.UnassignedWeapons[1]);*/

        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(playerWeapon.CurrentWeapons);

        if (availableWeapons.Count > 0) { 
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        if (playerWeapon.CurrentWeapons.Count + playerWeapon.FullyLeveledWeapons.Count < playerWeapon.MaxWeapons) { availableWeapons.AddRange(playerWeapon.UnassignedWeapons); }

        for (int i = weaponsToUpgrade.Count; i < playerWeapon.MaxWeapons; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }

        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++) {
            uiController.LevelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);    
        }

        for (int i = 0; i < uiController.LevelUpButtons.Length; i++) {
            if (i < weaponsToUpgrade.Count) {
                uiController.LevelUpButtons[i].gameObject.SetActive(true);
            } else
            {
                uiController.LevelUpButtons[i].gameObject.SetActive(false);
            }
        }

    }

}
