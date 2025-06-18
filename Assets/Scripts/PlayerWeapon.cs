using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Weapon activeWeapon;

    public Weapon ActiveWeapon { get => activeWeapon; set => activeWeapon = value; }
}
