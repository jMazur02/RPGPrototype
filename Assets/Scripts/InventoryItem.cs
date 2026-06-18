using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public ItemData itemData;
    public int count = 1;
    public TextMeshProUGUI countText;

    [HideInInspector] public Transform parentAfterDrag;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        RefreshItem();
    }

    public void RefreshItem()
    {
        if (image != null && itemData != null)
        {
            image.sprite = itemData.itemIcon;
        }

        if (countText != null && itemData != null)
        {
            bool showText = itemData.isStackable && count > 1;
            countText.gameObject.SetActive(showText);
            countText.text = count.ToString();
        }
    }

    // Łapanie RMB podczas wciśnięcia
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && count > 1)
        {
            // Klonowanie obiektu i pozostawienie w slocie -1
            GameObject clone = Instantiate(gameObject, transform.parent);
            InventoryItem cloneItem = clone.GetComponent<InventoryItem>();

            cloneItem.count = count - 1;
            cloneItem.RefreshItem();

            // Item pod przyciskiem zmniejszony do count 1
            count = 1;
            RefreshItem();
        }
    }

    // Początek przeciągania
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;

        // POPRAWKA: Zamiast transform.root przypinamy bezpiecznie do Canvasu, w którym żyje przedmiot
        transform.SetParent(GetComponentInParent<Canvas>().transform);
        transform.SetAsLastSibling();

        // NAPRAWIONY BŁĄD: Wyłączamy blocksRaycasts, aby przedmiot stał się "duchem" podczas lotu
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    // Podczas przeciągania
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // Zakończenie przeciągania
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;

        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }

        if (parentAfterDrag.childCount > 1)
        {
            InventoryItem[] itemsInSlot = parentAfterDrag.GetComponentsInChildren<InventoryItem>();
            if (itemsInSlot.Length > 1)
            {
                InventoryItem cloneItem = (itemsInSlot[0] == this) ? itemsInSlot[1] : itemsInSlot[0];
                cloneItem.count += this.count;
                cloneItem.RefreshItem();
                Destroy(gameObject);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemData != null)
        {
            InventoryTooltip.Instance.ShowTooltip(itemData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryTooltip.Instance.HideTooltip();
    }
}