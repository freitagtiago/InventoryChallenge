using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]private ItemSO _item;
    [SerializeField]private int _quantity = 1;
    private Inventory inventory;
    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }
    public void Setup(ItemSO item, int quantity)
    {
        this._item = item;
        if (!item.GetIsStackable())
        {
            quantity = 1;
        }
        this._quantity = quantity;
    }

    public ItemSO GetItem()
    {
        return _item;
    }

    public int GetNumber()
    {
        return _quantity;
    }

    public void PickupItem()
    {
        if (_item == null)
        { 
            return;
        }
        bool foundSlot = inventory.AddToFirstEmptySlot(_item, _quantity);
        if (foundSlot)
        {
            Destroy(gameObject);
        }
    }

    public bool CanBePickedUp()
    {
        return inventory.HasSpaceFor(_item);
    }
}
