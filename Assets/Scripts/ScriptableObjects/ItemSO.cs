using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private string _id = null;
    [SerializeField] private string _itemName = string.Empty;
    [SerializeField][TextArea] private string _description = string.Empty;
    [SerializeField] private Pickup _pickupPrefab = null;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isStackable = false;

    private static Dictionary<string, ItemSO> _itemLookupCache;

    public static ItemSO GetFromID(string itemID)
    {
        if (_itemLookupCache == null)
        {
            _itemLookupCache = new Dictionary<string, ItemSO>();
            var itemList = Resources.LoadAll<ItemSO>("");
            foreach (var item in itemList)
            {
                if (_itemLookupCache.ContainsKey(item._id))
                {
                    Debug.LogError(string.Format("Duplicate InventorySystem ID for itens: {0} and {1}", _itemLookupCache[item._id], item));
                    continue;
                }

                _itemLookupCache[item._id] = item;
            }
        }

        if (itemID == null || !_itemLookupCache.ContainsKey(itemID)) return null;
        return _itemLookupCache[itemID];
    }

    public Pickup SpawnPickup(Vector3 position, int number)
    {
        Pickup pickup = Instantiate(this._pickupPrefab);
        pickup.transform.position = position;
        pickup.Setup(this, number);
        return pickup;
    }

    public string GetItemName()
    {
        return _itemName;
    }

    public string GetItemDescription()
    {
        return _description;
    }

    public bool GetIsStackable()
    {
        return _isStackable;
    }

    public Sprite GetIcon()
    {
        return _icon;
    }

    public string GetItemId()
    {
        return _id;
    }

    public void OnBeforeSerialize()
    {
        if (string.IsNullOrWhiteSpace(_id))
        {
            _id = System.Guid.NewGuid().ToString();
        }
    }

    public void OnAfterDeserialize()
    {

    }
}
