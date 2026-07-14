using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Коллекция всех характеристик персонажа для использования в игре. 
/// Гарантирует наличие всех статов из StatId
/// </summary>
[System.Serializable]
public class StatCollection
{
    [SerializeField] private List<Stat> _stats = new List<Stat>();
    
    public IReadOnlyList<Stat> Stats => _stats;
    
    /// <summary>
    /// Добавление характеристики в коллекцию
    /// </summary>
    public void AddStat(Stat stat)
    {
        if (GetStat(stat.StatName) == null)
        {
            _stats.Add(stat);
        }
    }
    
    /// <summary>
    /// Получение характеристики по имени
    /// </summary>
    public Stat GetStat(string statName)
    {
        return _stats.Find(s => s.StatName == statName);
    }
    
    /// <summary>
    /// Получение значения характеристики по ID
    /// </summary>
    public float GetStatValue(string statName)
    {
        Stat stat = GetStat(statName);
        return stat != null ? stat.GetValue() : 0f;
    }
    
    /// <summary>
    /// Добавление модификатора к конкретной характеристике
    /// </summary>
    public void AddModifier(string statName, StatModifier modifier)
    {
        Stat stat = GetStat(statName);
        if (stat != null)
        {
            stat.AddModifier(modifier);
        }
    }
    
    /// <summary>
    /// Удаление всех модификаторов от источника для конкретной характеристики
    /// </summary>
    public void RemoveModifiersFromSource(string sourceId, string statName)
    {
        Stat stat = GetStat(statName);
        if (stat != null)
        {
            stat.RemoveModifiersFromSource(sourceId);
        }
    }
    
    /// <summary>
    /// Удаление всех модификаторов от источника для всех характеристик
    /// </summary>
    public void RemoveAllModifiersFromSource(string sourceId)
    {
        foreach (var stat in _stats)
        {
            stat.RemoveModifiersFromSource(sourceId);
        }
    }
}