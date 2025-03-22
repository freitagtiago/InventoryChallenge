using System;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour, ISaveable
{
    Dictionary<EquipLocation, EquipableItemSO> _equippedItems = new Dictionary<EquipLocation, EquipableItemSO>();
    public event Action _equipmentUpdated;

    public EquipableItemSO GetItemInSlot(EquipLocation equipLocation)
    {
        if (!_equippedItems.ContainsKey(equipLocation))
        {
            return null;
        }

        return _equippedItems[equipLocation];
    }

    public void AddItem(EquipLocation slot, EquipableItemSO item)
    {
        _equippedItems[slot] = item;

        if (_equipmentUpdated != null)
        {
            _equipmentUpdated();
        }
    }

    public void RemoveItem(EquipLocation slot)
    {
        _equippedItems.Remove(slot);
        if (_equipmentUpdated != null)
        {
            _equipmentUpdated();
        }
    }

    public IEnumerable<EquipLocation> GetAllPopulatedSlots()
    {
        return _equippedItems.Keys;
    }

    object ISaveable.CaptureState()
    {
        var equippedItemsForSerialization = new Dictionary<EquipLocation, string>();
        foreach (var pair in _equippedItems)
        {
            equippedItemsForSerialization[pair.Key] = pair.Value.GetItemId();
        }
        return equippedItemsForSerialization;
    }

    void ISaveable.RestoreState(object state)
    {
        _equippedItems = new Dictionary<EquipLocation, EquipableItemSO>();

        var equippedItemsForSerialization = (Dictionary<EquipLocation, string>)state;

        foreach (var pair in equippedItemsForSerialization)
        {
            var item = (EquipableItemSO)ItemSO.GetFromID(pair.Value);
            if (item != null)
            {
                _equippedItems[pair.Key] = item;
            }
        }
    }
}