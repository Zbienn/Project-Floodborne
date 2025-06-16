using UnityEngine;

[CreateAssetMenu(fileName = "PlayerExperienceData", menuName = "Game/Player Experience Data")]
public class PlayerExperienceData : ScriptableObject
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float currentXP = 0;

    public int CurrentLevel => currentLevel;
    public float CurrentXP => currentXP;
    public float XPToNextLevel => GetXPRequiredForLevel(currentLevel);

    public void ResetXP()
    {
        currentLevel = 1;
        currentXP = 0;
    }

    public bool AddXP(float amount)
    {
        currentXP += amount;

        while (currentXP >= GetXPRequiredForLevel(currentLevel))
        {
            currentXP -= GetXPRequiredForLevel(currentLevel);
            currentLevel++;
            return true; // Primeiro level up
        }

        return false;
    }

    private float GetXPRequiredForLevel(int level)
    {
        if (level == 1)
            return 5;

        if (level >= 2 && level < 21)
            return 5 + (level - 1) * 10;

        if (level >= 21 && level < 41)
            return GetXPRequiredForLevel(20) + (level - 20) * 13;

        return GetXPRequiredForLevel(40) + (level - 40) * 16;
    }
}
