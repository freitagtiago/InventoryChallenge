using System.Collections.Generic;

interface IModifierProvider
{
    public IEnumerable<float> GetAdditiveModifiers(Stat stat);
    public IEnumerable<float> GetPercentageModifiers(Stat stat);
}