using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.Port;


public class InventorySlot : MonoBehaviour, IDropHandler
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem draggingItem = dropped.GetComponent<InventoryItem>();
        if (transform.childCount == 0)
        {
            draggingItem.parentAfterDrag = transform;
            return;
        }
            if (transform.childCount > 0)
            {
                InventoryItem slotItem = GetComponentInChildren<InventoryItem>();

                if (slotItem.itemData == draggingItem.itemData && slotItem.itemData.isStackable)
                {
                    int maxCapacity = slotItem.itemData.maxStackSize;
                    if (slotItem.count < maxCapacity)
                    {
                        int availableSpace = maxCapacity - slotItem.count;
                        int itemsToAdd = Mathf.Min(draggingItem.count, availableSpace);

                        slotItem.count += itemsToAdd;
                        draggingItem.count -= itemsToAdd;

                        slotItem.RefreshItem();
                        draggingItem.RefreshItem();
                        
                        if(draggingItem.count == 0)
                        {
                            Destroy(dropped);
                        }
                    }
                }
            }
        }
        }
    

