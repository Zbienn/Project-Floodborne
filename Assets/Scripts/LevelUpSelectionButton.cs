using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeDescText, upgradeNameText;
    [SerializeField] private Image weaponIcon;
    private Weapon assignedWeapon;

    private ExperienceUIController uiController;
    [SerializeField] private PlayerWeapon player;

    void Start()
    {
        if (uiController == null) uiController = FindFirstObjectByType<ExperienceUIController>();
    }

    public void UpdateButtonDisplay(Weapon weapon)
    {
        weaponIcon.preserveAspect = true;
        weaponIcon.sprite = weapon.Icon;

        if (weapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = weapon.Stats[weapon.WeaponLevel].UpgradeText;
            upgradeNameText.text = weapon.name + " - Lvl " + (weapon.WeaponLevel + 1);
        } 
        else
        {
            upgradeDescText.text = "Unlock " + weapon.name;
            upgradeNameText.text = weapon.name;
        }
        assignedWeapon = weapon;
    }

    public void SelectUpgrade()
    {
        if(assignedWeapon != null)
        {
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
            }
            else
            {
                player.AddWeapon(assignedWeapon);
            }

            uiController.LevelUpPanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
