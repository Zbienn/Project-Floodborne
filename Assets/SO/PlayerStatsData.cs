using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Game/PlayerStatsData")]
public class PlayerStatsData : ScriptableObject
{
    [SerializeField] private float pickupRange = 2f;
    public float PickupRange => pickupRange;
}
