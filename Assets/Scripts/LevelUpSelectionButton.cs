using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeDescText, upgradeNameText;
    [SerializeField] private Image weaponIcon;
    private Weapon assignedWeapon;

    private ExperienceUIController uiController;

    void Start()
    {
        if (uiController == null) uiController = FindFirstObjectByType<ExperienceUIController>();
    }

    public void UpdateButtonDisplay(Weapon weapon)
    {
        upgradeDescText.text = weapon.Stats[weapon.WeaponLevel].UpgradeText;
        weaponIcon.sprite = weapon.Icon;

        upgradeNameText.text = weapon.name + " - Lvl " + (weapon.WeaponLevel + 1);

        assignedWeapon = weapon;
    }

    public void SelectUpgrade()
    {
        if(assignedWeapon != null)
        {
            assignedWeapon.LevelUp();

            uiController.LevelUpPanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
