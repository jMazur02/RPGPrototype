using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class InventoryTooltip : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static InventoryTooltip Instance;
    [Header("UI Components")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemStatsText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        HideTooltip();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Vector2 offset = new Vector2(100f, 130f);

            transform.position = mousePos + offset;

        }
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    public void ShowTooltip(ItemData data)
    {

        if(data == null)
        {
            return;
        }
        gameObject.SetActive(true);


        itemNameText.text = data.itemName;

        //Generowanie Opisu
        itemStatsText.text = GenerateStatsString(data);
    }

    private string GenerateStatsString(ItemData data)
    {
        string stats = "";
        if (data is WeaponData weapon)
        {
            stats += $"Damage             {weapon.damage}\n";
            stats += $"Attack Speed:             {weapon.attackSpeed}\n";

        }
        if (data is ToolData tool)
        {
            stats += $"Minig Speed:             {tool.miningSpeed}\n";
            stats += $"Durability:             {tool.durability}\n";

        }
        if (string.IsNullOrEmpty(stats))
        {
            if (data is ItemData item)
            {
                stats += $"Just a {item.itemName}\n";
            }
        }
        return stats;
    }
}
