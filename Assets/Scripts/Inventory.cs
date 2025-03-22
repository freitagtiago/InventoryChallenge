using System;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveable, IPredicateEvaluation
{
    [SerializeField] private int _inventorySize = 16;
    private InventorySlot[] _slots;

    public struct InventorySlot
    {
        public ItemSO _item;
        public int _quantity;
    }

    public event Action _inventoryUpdated;

    public static Inventory GetPlayerInventory()
    {
        GameObject player = GameObject.FindWithTag("Player");
        return player.GetComponent<Inventory>();
    }

    public bool HasSpaceFor(ItemSO item)
    {
        return FindSlot(item) >= 0;
    }

    public int GetSize()
    {
        return _slots.Length;
    }

    public bool AddToFirstEmptySlot(ItemSO item, int quantity)
    {
        int i = FindSlot(item);

        if (i < 0)
        {
            return false;
        }

        _slots[i]._item = item;
        _slots[i]._quantity += quantity;
        if (_inventoryUpdated != null)
        {
            _inventoryUpdated();
        }
        return true;
    }

    public bool HasItem(ItemSO item)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (object.ReferenceEquals(_slots[i]._item, item))
            {
                return true;
            }
        }
        return false;
    }

    public ItemSO GetItemInSlot(int slot)
    {
        return _slots[slot]._item;
    }

    public int GetQuantityInSlot(int slot)
    {
        return _slots[slot]._quantity;
    }

    public void RemoveFromSlot(int slot, int number)
    {
        _slots[slot]._quantity -= number;
        if (_slots[slot]._quantity <= 0)
        {
            _slots[slot]._quantity = 0;
            _slots[slot]._item = null;
        }
        if (_inventoryUpdated != null)
        {
            _inventoryUpdated();
        }
    }
    public bool AddItemToSlot(int slot, ItemSO item, int quantity)
    {
        if (_slots[slot]._item != null)
        {
            return AddToFirstEmptySlot(item, quantity); ;
        }

        var i = FindStack(item);
        if (i >= 0)
        {
            slot = i;
        }

        _slots[slot]._item = item;
        _slots[slot]._quantity += quantity;
        if (_inventoryUpdated != null)
        {
            _inventoryUpdated();
        }
        return true;
    }

    private void Awake()
    {
        _slots = new InventorySlot[_inventorySize];
    }

    private int FindSlot(ItemSO item)
    {
        int i = FindStack(item);
        if (i < 0)
        {
            i = FindEmptySlot();
        }
        return i;
    }

    private int FindEmptySlot()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item == null)
            {
                return i;
            }
        }
        return -1;
    }
    private int FindStack(ItemSO item)
    {
        if (!item.GetIsStackable())
        {
            return -1;
        }

        for (int i = 0; i < _slots.Length; i++)
        {
            if (object.ReferenceEquals(_slots[i]._item, item))
            {
                return i;
            }
        }
        return -1;
    }

    [System.Serializable]
    private struct InventorySlotRecord
    {
        public string _itemID;
        public int _quantity;
    }

    object ISaveable.CaptureState()
    {
        var slotStrings = new InventorySlotRecord[_inventorySize];
        for (int i = 0; i < _inventorySize; i++)
        {
            if (_slots[i]._item != null)
            {
                slotStrings[i]._itemID = _slots[i]._item.GetItemId();
                slotStrings[i]._quantity = _slots[i]._quantity;
            }
        }
        return slotStrings;
    }

    void ISaveable.RestoreState(object state)
    {
        var slotStrings = (InventorySlotRecord[])state;
        for (int i = 0; i < _inventorySize; i++)
        {
            _slots[i]._item = ItemSO.GetFromID(slotStrings[i]._itemID);
            _slots[i]._quantity = slotStrings[i]._quantity;
        }
        if (_inventoryUpdated != null)
        {
            _inventoryUpdated();
        }
    }

    public bool? Evaluate(string predicate, string[] parameters)
    {
        switch (predicate)
        {
            case "HasInventoryItem":
                return HasItem(ItemSO.GetFromID(parameters[0]));
        }
        return null;
    }
}
