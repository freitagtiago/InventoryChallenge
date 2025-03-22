using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Equipable Item", menuName = "Itens/Stat Equipable Item", order = 1)]
public class StatsEquipableItem : EquipableItemSO, IModifierProvider
{
    [SerializeField] private Modifier[] _additiveModifiers;
    [SerializeField] private Modifier[] _percentageModifiers;

    [System.Serializable]
    struct Modifier
    {
        public Stat _stat;
        public float _value;
    }

    public IEnumerable<float> GetAdditiveModifiers(Stat stat)
    {
        foreach (var modifier in _additiveModifiers)
        {
            if (modifier._stat == stat)
            {
                yield return modifier._value;
            }
        }
    }

    public IEnumerable<float> GetPercentageModifiers(Stat stat)
    {
        foreach (var modifier in _percentageModifiers)
        {
            if (modifier._stat == stat)
            {
                yield return modifier._value;
            }
        }
    }
}