using UnityEngine;

public class EquipmentSlotUI : MonoBehaviour, IItemHolder, IDragContainer<ItemSO>
{
    [SerializeField] private SlotIcon _icon = null;
    [SerializeField] private EquipLocation _equipLocation = EquipLocation.Weapon;

    private Equipment _playerEquipment;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerEquipment = player.GetComponent<Equipment>();
        _playerEquipment._equipmentUpdated += RedrawUI;
    }

    private void Start()
    {
        RedrawUI();
    }

    public int MaxAcceptable(ItemSO item)
    {
        EquipableItemSO equipableItem = item as EquipableItemSO;
        if (equipableItem == null) 
        { 
            return 0; 
        }
        if (equipableItem.GetAllowedEquipLocation() != _equipLocation)
        {
            return 0;
        }
        if (GetItem() != null) 
        { 
            return 0; 
        }
        return 1;
    }

    public void AddItems(ItemSO item, int number)
    {
        _playerEquipment.AddItem(_equipLocation, (EquipableItemSO)item);
    }

    public ItemSO GetItem()
    {
        return _playerEquipment.GetItemInSlot(_equipLocation);
    }

    public int GetNumber()
    {
        if (GetItem() != null)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void RemoveItems(int number)
    {
        _playerEquipment.RemoveItem(_equipLocation);
    }

    void RedrawUI()
    {
        _icon.SetItem(_playerEquipment.GetItemInSlot(_equipLocation));
    }
}