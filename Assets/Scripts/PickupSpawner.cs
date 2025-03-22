using UnityEngine;

public class PickupSpawner : MonoBehaviour, ISaveable
{
    [SerializeField] private ItemSO _item = null;
    [SerializeField] private int _quantity = 1;
    private void Awake()
    {
        SpawnPickup();
    }
    public Pickup GetPickup()
    {
        return GetComponentInChildren<Pickup>();
    }
    public bool isCollected()
    {
        return GetPickup() == null;
    }
    private void SpawnPickup()
    {
        var spawnedPickup = _item.SpawnPickup(transform.position, _quantity);
        spawnedPickup.transform.SetParent(transform);
    }

    private void DestroyPickup()
    {
        if (GetPickup())
        {
            Destroy(GetPickup().gameObject);
        }
    }

    object ISaveable.CaptureState()
    {
        return isCollected();
    }

    void ISaveable.RestoreState(object state)
    {
        bool shouldBeCollected = (bool)state;

        if (shouldBeCollected && !isCollected())
        {
            DestroyPickup();
        }

        if (!shouldBeCollected && isCollected())
        {
            SpawnPickup();
        }
    }
}