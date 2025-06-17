using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceUIController : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField] private PlayerExperienceData playerXP;

    [Header("UI Elements")]
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TMP_Text levelText;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        xpSlider.maxValue = playerXP.XPToNextLevel;
        xpSlider.value = playerXP.CurrentXP;

        levelText.text = $"Level: {playerXP.CurrentLevel}";
    }
}
