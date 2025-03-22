using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] private int _startingHealth = 100;
    LazyValue<int> _health;
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
        _health = new LazyValue<int>(GetInitialHealth);
    }

    private int GetInitialHealth()
    {
        return _startingHealth;
    }

    private void Start()
    {
        _health.ForceInit();
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
        return _health.value / _startingHealth;
    }
    public float GetHealthPoints()
    {
        return _health.value;
    }

    public float GetMaxHealthPoints()
    {
        return _startingHealth;
    }

    private void RegenerateHealth()
    {
        float fullHealth = _startingHealth;
        float regeneratePoints = fullHealth * (_regeneratePercentage / 100);
        float value = _health.value + regeneratePoints;
        _health.value = (int)Mathf.Max(_health.value, regeneratePoints);
    }

    public void TakeDamage(GameObject instigator, float damage)
    {
        bool isDead = false;

        Debug.Log(gameObject.name + " took damage of " + damage);

        _health.value = (int)Mathf.Max(_health.value - damage, 0);
        _takeDamage.Invoke(damage);
        if (_health.value == 0 && _isDead == false)
        {
            OnDie.Invoke();
            Die();
        }
    }

    public void Heal(float healthToRestore)
    {
        _health.value = (int)Mathf.Min(_health.value + healthToRestore, GetMaxHealthPoints());
    }

    private void Die()
    {
        GetComponent<Animator>().SetTrigger("isAlive");
        _isDead = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public object CaptureState()
    {
        return _health.value;
    }
    public void RestoreState(object state)
    {
        _health.value = (int)state;
        if (_health.value == 0 
            && _isDead == false)
        {
            Die();
        }
    }
}
