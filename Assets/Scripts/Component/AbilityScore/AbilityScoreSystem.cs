using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*
Description:
    AbilityScore has an enum named Attribute. This system allows access to an Entity’s various scores via the cases of this enum. 
    Partial definition on Entity,allows get or set of ability scores by an indexer.
        e.g. 
            hero is an Entity
            var attribute = AbilityScore.Attribute.Strength; // could be any case
            var score = hero[attribute];
            var modifier = hero[attribute].Modifier;
*/ 
public interface IAbilityScoreSystem : IDependency<IAbilityScoreSystem>
{
    AbilityScore Get(Entity entity, AbilityScore.Attribute attribute);
    void Set(Entity entity, AbilityScore.Attribute attribute, AbilityScore value);
    void Set(Entity entity, IEnumerable<int> scores);
}

[Dependency(typeof(IAbilityScoreSystem))]
public class AbilityScoreSystem : IAbilityScoreSystem
{
    public void Set(Entity entity, IEnumerable<int> scores)
    {
        Debug.Assert(scores.Count() == 6, "Incorrect ability score count");
        IStrengthSystem.Resolve().Set(entity, scores.ElementAt(0));
        IDexteritySystem.Resolve().Set(entity, scores.ElementAt(1));
        IConstitutionSystem.Resolve().Set(entity, scores.ElementAt(2));
        IIntelligenceSystem.Resolve().Set(entity, scores.ElementAt(3));
        IWisdomSystem.Resolve().Set(entity, scores.ElementAt(4));
        ICharismaSystem.Resolve().Set(entity, scores.ElementAt(5));
    }

    public AbilityScore Get(Entity entity, AbilityScore.Attribute attribute)
    {
        switch (attribute)
        {
            case AbilityScore.Attribute.Strength:
                return IStrengthSystem.Resolve().Get(entity);
            case AbilityScore.Attribute.Dexterity:
                return IDexteritySystem.Resolve().Get(entity);
            case AbilityScore.Attribute.Constitution:
                return IConstitutionSystem.Resolve().Get(entity);
            case AbilityScore.Attribute.Intelligence:
                return IIntelligenceSystem.Resolve().Get(entity);
            case AbilityScore.Attribute.Wisdom:
                return IWisdomSystem.Resolve().Get(entity);
            case AbilityScore.Attribute.Charisma:
                return ICharismaSystem.Resolve().Get(entity);
        }
        return (AbilityScore)0;
    }

    public void Set(Entity entity, AbilityScore.Attribute attribute, AbilityScore value)
    {
        switch (attribute)
        {
            case AbilityScore.Attribute.Strength:
                IStrengthSystem.Resolve().Set(entity, value);
                break;
            case AbilityScore.Attribute.Dexterity:
                IDexteritySystem.Resolve().Set(entity, value);
                break;
            case AbilityScore.Attribute.Constitution:
                IConstitutionSystem.Resolve().Set(entity, value);
                break;
            case AbilityScore.Attribute.Intelligence:
                IIntelligenceSystem.Resolve().Set(entity, value);
                break;
            case AbilityScore.Attribute.Wisdom:
                IWisdomSystem.Resolve().Set(entity, value);
                break;
            case AbilityScore.Attribute.Charisma:
                ICharismaSystem.Resolve().Set(entity, value);
                break;
        }
    }
}

public partial struct Entity
{
    public AbilityScore this[AbilityScore.Attribute attribute]
    {
        get { return IAbilityScoreSystem.Resolve().Get(this, attribute); }
        set { IAbilityScoreSystem.Resolve().Set(this, attribute, value); }
    }
}