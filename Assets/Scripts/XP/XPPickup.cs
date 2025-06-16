using UnityEngine;

public class XPPickup : MonoBehaviour
{
    [SerializeField] private float xpAmount = 5f;
    [SerializeField] private PlayerExperienceData playerExperience;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerExperience != null)
            {
                bool leveledUp = playerExperience.AddXP(xpAmount);
                if (leveledUp)
                {
                    Debug.Log("Level Up!");
                    // Aqui podes chamar algum evento de UI ou VFX
                }
            }

            Destroy(gameObject);
        }
    }
}
