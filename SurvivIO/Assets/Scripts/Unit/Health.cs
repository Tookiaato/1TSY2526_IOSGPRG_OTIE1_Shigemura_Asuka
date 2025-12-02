using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth => _maxHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    }

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public Action _onDeath;

    private void OnEnable()
    {
        _onDeath -= OnDeath;
        _onDeath += OnDeath;
    }

    private void OnDisable()
    {
        _onDeath -= OnDeath;
    }

    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _currentHealth = Mathf.Max(_currentHealth - damage, 0);

        if (_currentHealth <= 0)
        {
            _onDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Heal()
    {
        Inventory inventory = GameManager.Instance._inventory;

        if (_currentHealth < _maxHealth && inventory._healthKits > 0)
        {
            inventory._isHealing = true;
        }
    }

    private void OnDeath()
    {
        Debug.Log($"{gameObject.name} is Dead!");
    }
}
