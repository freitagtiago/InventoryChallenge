using UnityEngine;

[CreateAssetMenu(menuName = ("Itens/Equipable Item"))]
public class EquipableItemSO : ItemSO
{
    [SerializeField] private EquipLocation _allowedEquipLocation = EquipLocation.Weapon;

    public EquipLocation GetAllowedEquipLocation()
    {
        return _allowedEquipLocation;
    }
}
