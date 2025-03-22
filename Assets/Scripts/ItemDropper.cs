using UnityEngine;
using System.Collections.Generic;

public class ItemDropper : MonoBehaviour
{
    private List<Pickup> _droppedItems = new List<Pickup>();

    public void DropItem(ItemSO item, int number)
    {
        SpawnPickup(item, GetDropLocation(), number);
    }

    public void DropItem(ItemSO item)
    {
        SpawnPickup(item, GetDropLocation(), 1);
    }

    protected virtual Vector3 GetDropLocation()
    {
        Vector3 position = new Vector3(transform.position.x, 0.2f, transform.position.z);
        return position;
    }

    public void SpawnPickup(ItemSO item, Vector3 spawnLocation, int number)
    {
        var pickup = item.SpawnPickup(spawnLocation, number);
        _droppedItems.Add(pickup);
    }
}
