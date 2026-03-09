using System;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityBoost
{
    Strength,
    Dexterity,
    Constitution,
    Intelligence,
    Wisdom,
    Charisma,
    Free
}

public class AbilityBoostProvider : MonoBehaviour, IAttributeProvider
{
    public bool isFlaw;
    public List<AbilityBoost> boosts;

    public void Setup(Entity entity)
    {
        foreach (var boost in boosts)
        {
            AbilityScore.Attribute attribute;
            if (boost == AbilityBoost.Free)
            {
                var options = new List<AbilityBoost>((AbilityBoost[])Enum.GetValues(typeof(AbilityBoost)));
                foreach (var option in boosts)
                    options.Remove(option);
                var rand = UnityEngine.Random.Range(0, options.Count);
                var selectedBoost = options[rand];
                attribute = (AbilityScore.Attribute)selectedBoost;
            }
            else
            {
                attribute = (AbilityScore.Attribute)boost;
            }
            entity[attribute] += isFlaw ? -2 : 2;
        }
    }
}