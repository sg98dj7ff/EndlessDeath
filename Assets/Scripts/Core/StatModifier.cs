using UnityEngine;

/// <summary>
/// Модификатор характеристик, используется генами и баффами для изменения статов
/// </summary>
[System.Serializable]
public class StatModifier
{
    public float Value;
    public StatModifierType Type;
    public string SourceId;

    public StatModifier(float value, StatModifierType type, string sourceId)
    {
        Value = value;
        Type = type;
        SourceId = sourceId;
    }
}

/// <summary>
/// Тип модификации стата
/// </summary>
public enum StatModifierType
{
    Add,
    Multiply
}