using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    public GameObject MainInventoryGroup; 
    public InputAction toggleAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggleAction.Enable();
        MainInventoryGroup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleAction.triggered)
        {
            // activeSelf mówi, czy obiekt jest teraz włączony (true/false)
            bool invState = MainInventoryGroup.activeSelf;
            if( invState == true)
            {

            }
            // Ustawiamy stan panelu na ODWROTNY niż jest teraz (!)
            MainInventoryGroup.SetActive(!invState);


            if (invState && InventoryTooltip.Instance != null)
            {
                InventoryTooltip.Instance.HideTooltip();
            }
        }

    }
}
