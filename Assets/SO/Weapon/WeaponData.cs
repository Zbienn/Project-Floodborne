using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Sprite weaponIcon;

    [System.Serializable]
    public class WeaponLevelStats
    {
        [SerializeField] private float damage;
        [SerializeField] private float cooldown;
        [SerializeField] private float duration;
        [SerializeField] private float speed;
        [SerializeField] private float amount;
        [SerializeField] private float areaSize;
        [TextArea][SerializeField] private string description;

        // Getters públicos
        public float Damage => damage;
        public float Cooldown => cooldown;
        public float Duration => duration;
        public float Speed => speed;
        public float Amount => amount;
        public float AreaSize => areaSize;
        public string Description => description;
    }

    [SerializeField] private WeaponLevelStats[] levels;

    public WeaponLevelStats GetStatsForLevel(int level)
    {
        if (level <= 0) level = 1;
        if (level > levels.Length) level = levels.Length;
        return levels[level - 1];
    }

    public int MaxLevel => levels.Length;
    public string WeaponName => weaponName;
    public GameObject WeaponPrefab => weaponPrefab;
    public Sprite WeaponIcon => weaponIcon;

    public string GetDescriptionForLevel(int level)
    {
        return GetStatsForLevel(level).Description;
    }
}
