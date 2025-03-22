using UnityEngine;

public class SlotUI : MonoBehaviour, IItemHolder, IDragContainer<ItemSO>
{
    [SerializeField] private SlotIcon _icon = null;

    private int _index;
    private ItemSO _item;
    private Inventory _inventory;

    // PUBLIC

    public void Setup(Inventory inventory, int index)
    {
        this._inventory = inventory;
        this._index = index;
        _icon.SetItem(inventory.GetItemInSlot(index), inventory.GetQuantityInSlot(index));
    }

    public int MaxAcceptable(ItemSO item)
    {
        if (_inventory.HasSpaceFor(item))
        {
            return int.MaxValue;
        }
        return 0;
    }

    public void AddItems(ItemSO item, int number)
    {
        _inventory.AddItemToSlot(_index, item, number);
    }

    public ItemSO GetItem()
    {
        return _inventory.GetItemInSlot(_index);
    }

    public int GetNumber()
    {
        return _inventory.GetQuantityInSlot(_index);
    }

    public void RemoveItems(int number)
    {
        _inventory.RemoveFromSlot(_index, number);
    }
}
