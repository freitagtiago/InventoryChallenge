using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] private int _startingHealth = 100;
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _isDead = false;

    private void Awake()
    {
        _currentHealth = (_startingHealth / 2); //Just to show that the potions are recovering the HP
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public float GetHealthPoints()
    {
        return _currentHealth;
    }

    public float GetMaxHealthPoints()
    {
        return _startingHealth;
    }

    public void Heal(float healthToRestore)
    {
        _currentHealth = (int)Mathf.Min(_currentHealth + healthToRestore, GetMaxHealthPoints());
    }

    public object CaptureState()
    {
        return _currentHealth;
    }
    public void RestoreState(object state)
    {
        _currentHealth = (int)state;
    }
}
