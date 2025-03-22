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
    [SerializeField] private float _regeneratePercentage = 75f;
    [SerializeField] private TakeDamageEvent _takeDamage;
    [SerializeField] private UnityEvent OnDie;

    [System.Serializable]
    public class TakeDamageEvent : UnityEvent<float>
    {

    }
    private void Awake()
    {
        _currentHealth = _startingHealth;
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public float GetPercentage()
    {
        return 100 * GetFraction();
    }

    public float GetFraction()
    {
        return _currentHealth / _startingHealth;
    }
    public float GetHealthPoints()
    {
        return _currentHealth;
    }

    public float GetMaxHealthPoints()
    {
        return _startingHealth;
    }

    public void TakeDamage(GameObject instigator, float damage)
    {
        _currentHealth = (int)Mathf.Max(_currentHealth - damage, 0);
        _takeDamage.Invoke(damage);
        if (_currentHealth == 0 && _isDead == false)
        {
            OnDie.Invoke();
            Die();
        }
    }

    public void Heal(float healthToRestore)
    {
        _currentHealth = (int)Mathf.Min(_currentHealth + healthToRestore, GetMaxHealthPoints());
    }

    private void Die()
    {
        GetComponent<Animator>().SetTrigger("isAlive");
        _isDead = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public object CaptureState()
    {
        return _currentHealth;
    }
    public void RestoreState(object state)
    {
        _currentHealth = (int)state;
        if (_currentHealth == 0 
            && _isDead == false)
        {
            Die();
        }
    }
}
