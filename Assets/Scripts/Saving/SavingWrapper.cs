using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingWrapper : MonoBehaviour
{
    const string DEFAULT_SAVE_FILE = "save";

    public static SavingWrapper Instance;
    private SavingSystem _savingSystem;

    private Inventory _inventory;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        _savingSystem = GetComponent<SavingSystem>();
        _inventory = FindFirstObjectByType<Inventory>();
    }

    private void OnEnable()
    {
        _inventory._inventoryUpdated += SavingWrapper.Instance.Save;
    }

    private void OnDisable()
    {
        _inventory._inventoryUpdated -= SavingWrapper.Instance.Save;
    }

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        _savingSystem.Save(DEFAULT_SAVE_FILE);
    }

    public void Load()
    {
        _savingSystem.Load(DEFAULT_SAVE_FILE);
    }

    public void Delete()
    {
        _savingSystem.Delete(DEFAULT_SAVE_FILE);
    }
}
