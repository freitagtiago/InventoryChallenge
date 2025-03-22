using UnityEngine;

public class InventoryDropTarget : MonoBehaviour, IDragDestination<ItemSO>
{
    public void AddItems(ItemSO item, int number)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ItemDropper>().DropItem(item, number);
    }

    public int MaxAcceptable(ItemSO item)
    {
        return int.MaxValue;
    }
}