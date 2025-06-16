using UnityEngine;

public class EnemyXPDropper : MonoBehaviour
{
    private GameObject xpPickupPrefab;

    public void SetXPPickup(GameObject pickup)
    {
        xpPickupPrefab = pickup;
    }

    public void DropXP()
    {
        if (xpPickupPrefab != null)
        {
            Instantiate(xpPickupPrefab, transform.position, Quaternion.identity);
        }
    }
}
