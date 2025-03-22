using UnityEngine;
using UnityEngine.InputSystem;

public class UICanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject _uiPanels;
    [SerializeField] private InputActionAsset inputActions;
    private InputAction _inventoryAction;

    private void Awake()
    {
        var playerActionMap = inputActions.FindActionMap("Player");
        _inventoryAction = playerActionMap.FindAction("Inventory");
    }

    private void OnEnable()
    {
        _inventoryAction.performed += OnInventoryPanelPerformed;
    }

    private void OnDisable()
    {
        _inventoryAction.performed -= OnInventoryPanelPerformed;
    }

    public void OnInventoryPanelPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _uiPanels.SetActive(!_uiPanels.activeInHierarchy);
        }
    }
}
