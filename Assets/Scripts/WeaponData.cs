using UnityEngine;

public enum WeaponType { Sword, Dagger, Bow, Staff, Tool }

[CreateAssetMenu(fileName = "WeaponData", menuName = "Inventory/Weapons")]
public class WeaponData : ItemData
{
    [Header("Attributes")]
    public WeaponType weaponType;
    public int damage;
    public int attackSpeed;
}
