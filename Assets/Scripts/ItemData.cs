using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("Basic Information")]
    public string itemName;       // Nazwa przedmiotu
    public Sprite itemIcon;       // Ikonka przedmiotu

    [Header("Stackable")]
    public bool isStackable;      // Czy można go stackować?
    public int maxStackSize = 12; // Maksymalna ilość w jednym slocie
}