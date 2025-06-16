using UnityEngine;

public class PickupRangeController : MonoBehaviour
{
    [SerializeField] private PlayerStatsData statsData;
    private CircleCollider2D rangeCollider;
    private float lastPickupRange;

    private void Awake()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        lastPickupRange = statsData.PickupRange;
        rangeCollider.radius = lastPickupRange;
    }

    private void Update()
    {
        if (Mathf.Abs(statsData.PickupRange - lastPickupRange) > 0.01f)
        {
            lastPickupRange = statsData.PickupRange;
            rangeCollider.radius = lastPickupRange;
        }
    }
}
