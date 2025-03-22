using UnityEngine;
public interface IPredicateEvaluation
{
    public bool? Evaluate(string predicate, string[] parameters);
}