using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Представляет одну характеристику с базовым значением, типом и модификаторами
/// </summary>
[System.Serializable]
public class Stat
{
    [SerializeField] private string _statName;
    [SerializeField] private float _baseValue;
    [SerializeField] private StatType _statType = StatType.Number;
    
    private List<StatModifier> _modifiers = new List<StatModifier>();
    private float _cachedValue;
    private bool _isDirty = true;
    
    public string StatName => _statName;
    public float BaseValue => _baseValue;
    public StatType StatType => _statType;
    
    public Stat() { }
    
    public Stat(string statName, StatType statType = StatType.Number)
    {
        _statName = statName;
        _baseValue = 0;
        _statType = statType;
        _cachedValue = 0;
    }

    public Stat(string statName, float baseValue, StatType statType = StatType.Number)
    {
        _statName = statName;
        _baseValue = baseValue;
        _statType = statType;
        _cachedValue = baseValue;
    }
    
    /// <summary>
    /// Добавление модификатора к характеристике
    /// </summary>
    public void AddModifier(StatModifier modifier)
    {
        _modifiers.Add(modifier);
        _isDirty = true;
    }
    
    /// <summary>
    /// Удаление всех модификаторов от определенного источника
    /// </summary>
    public void RemoveModifiersFromSource(string sourceId)
    {
        _modifiers.RemoveAll(m => m.SourceId == sourceId);
        _isDirty = true;
    }
    
    /// <summary>
    /// Получение текущего значения характеристики с учетом всех модификаторов
    /// </summary>
    public float GetValue()
    {
        if (_isDirty)
        {
            RecalculateValue();
        }
        return _cachedValue;
    }
    
    /// <summary>
    /// Пересчет значения характеристики в зависимости от типа
    /// </summary>
    private void RecalculateValue()
    {
        if (_statType == StatType.Percentage)
        {
            float percentageValue = _baseValue;
            float addModifiers = 0f;
            
            foreach (var modifier in _modifiers)
            {
                if (modifier.Type == StatModifierType.Add)
                {
                    addModifiers += modifier.Value;
                }
            }
            
            _cachedValue = percentageValue + addModifiers;
        }
        else
        {
            float value = _baseValue;
            float addModifiers = 0f;
            float multiplyModifiers = 1f;
            
            foreach (var modifier in _modifiers)
            {
                if (modifier.Type == StatModifierType.Add)
                {
                    addModifiers += modifier.Value;
                }
                else if (modifier.Type == StatModifierType.Multiply)
                {
                    multiplyModifiers *= modifier.Value;
                }
            }
            
            _cachedValue = (value + addModifiers) * multiplyModifiers;
        }
        
        _isDirty = false;
    }
}