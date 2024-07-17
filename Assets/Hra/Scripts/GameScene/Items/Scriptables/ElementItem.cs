using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    None = 0,
    Fire = 1,
    Frost = 2,
    Electric = 3,
    Life = 4
}

public enum SpecialEffect
{
    None = 0,
    Heal = 1,
    WeaponSpeedMultiplier = 2,
    Dot = 3,
    ChainDamage = 4,
    RangeMultiplier = 5,
    EnemySlow = 6
}

[CreateAssetMenu(fileName = "New element", menuName = "Item/Element")]
public class ElementItem : ItemBase
{
    public Element Element;
    public SerializedDictionary<SpecialEffect, List<float>> SpecialEffects = new();
}
