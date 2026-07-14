using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject-эталон, содержащий ПОЛНЫЙ набор характеристик игры.
/// Используется как база для создания StatCollection любого персонажа
/// </summary>
[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats/Character Stats")]
public class CharacterStats : ScriptableObject
{
    [SerializeField] private List<Stat> _defaultStats = new List<Stat>
    {
        { new Stat("MaxHealth", 100f, StatType.Number) },
        { new Stat("Damage", 10f, StatType.Number) },
        { new Stat("MoveSpeed", 2f, StatType.Number) },
        { new Stat("JumpForce", 10f, StatType.Number) },
        { new Stat("DamageReduction", 0f, StatType.Number) },
        { new Stat("AttackSpeed", 1f, StatType.Number) },
        { new Stat("AttackRange", 5f, StatType.Number) },
        { new Stat("PoisonResistance", 0f, StatType.Percentage) }
    };
    
    /// <summary>
    /// Создание полной копии всех характеристик из эталона.
    /// Если в overrides есть значение для стата, используется оно, иначе берется дефолтное из эталона
    /// </summary>
    public StatCollection CreateStatCollection()
    {
        StatCollection collection = new StatCollection();
        foreach (var stat in _defaultStats)
        {
            collection.AddStat(stat);
        }
        return collection;
    }
    
    /// <summary>
    /// Получение дефолтного значения характеристики из эталона
    /// </summary>
    public float GetDefaultValue(string statName)
    {
        Stat stat = _defaultStats.Find(s => s.StatName == statName);
        return stat != null ? stat.BaseValue : 0f;
    }
}