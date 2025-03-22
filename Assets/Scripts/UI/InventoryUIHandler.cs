using UnityEngine;

public class InventoryUIHandler : MonoBehaviour
{
    [SerializeField] private SlotUI _slotUIPrefab = null;

    private Inventory _playerInventory;

    private void Awake()
    {
        _playerInventory = Inventory.GetPlayerInventory();
        _playerInventory._inventoryUpdated += Redraw;
    }

    private void Start()
    {
        Redraw();
    }

    private void Redraw()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < _playerInventory.GetSize(); i++)
        {
            SlotUI slotUI = Instantiate(_slotUIPrefab, transform);
            slotUI.Setup(_playerInventory, i);
        }
    }
}
