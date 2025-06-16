using UnityEngine;

public class PlayerXPResetter : MonoBehaviour
{
    [SerializeField] private PlayerExperienceData playerExperience;

    void Awake()
    {
        if (playerExperience != null)
        {
            playerExperience.ResetXP();
        }
    }
}
