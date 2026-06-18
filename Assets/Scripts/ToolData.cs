using UnityEngine;

public enum ToolType { Pickaxe, Axe, Shovel, Hoe }

[CreateAssetMenu(fileName = "New Tool", menuName = "Inventory/Tools")]
public class ToolData : WeaponData
{
    [Header("Tool attributes")]
    public ToolType toolType;
    public int miningSpeed;
    public int durability;
}
