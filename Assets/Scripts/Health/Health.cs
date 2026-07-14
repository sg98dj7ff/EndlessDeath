using System;
using UnityEngine;

/// <summary>
/// Компонент управления здоровьем, использует StatCollection и поддерживает модификаторы
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField] private CharacterStats _characterStats;
    
    private StatCollection _stats;
    private float _currentHealth;
    
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;
    
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _stats.GetStatValue("MaxHealth");
    public StatCollection Stats => _stats;
    
    private void Start()
    {
        InitializeStats();
    }
    
    /// <summary>
    /// Инициализация характеристик из эталона CharacterStats
    /// </summary>
    private void InitializeStats()
    {
        _stats = _characterStats.CreateStatCollection();
        _currentHealth = MaxHealth;
        OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
    }
    
    /// <summary>
    /// Получение урона
    /// </summary>
    public void TakeDamage(float damage)
    {
        float damageReduction = _stats.GetStatValue("DamageReduction");
        float actualDamage = Mathf.Max(1, damage - damageReduction);
        
        _currentHealth = Mathf.Max(0, _currentHealth - actualDamage);
        OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    
    /// <summary>
    /// Лечение
    /// </summary>
    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(MaxHealth, _currentHealth + amount);
        OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
    }
    
    /// <summary>
    /// Смерть персонажа
    /// </summary>
    private void Die()
    {
        OnDeath?.Invoke();
    }
}