using UnityEngine;
[CreateAssetMenu(menuName = ("Itens/Consumable Item"))]
public class ConsumableItemSO : ItemSO
{
    [SerializeField] public int _hpToRecover = 10;
}
