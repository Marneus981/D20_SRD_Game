public enum Proficiency
{
    Untrained,
    Trained,
    Expert,
    Master,
    Legendary
}
public interface IProficiencySystem : IDependency<IProficiencySystem>
{
    Proficiency Get(Entity entity, Skill skill);
    void Set(Entity entity, Skill skill, Proficiency value);
}

public class ProficiencySystem : IProficiencySystem
{
    public Proficiency Get(Entity entity, Skill skill)
    {
        return GetSystem(skill).Get(entity);
    }

    public void Set(Entity entity, Skill skill, Proficiency value)
    {
        GetSystem(skill).Set(entity, value);
    }

    IEntityTableSystem<Proficiency> GetSystem(Skill skill)
    {
        switch (skill)
        {
            case Skill.Acrobatics:
                return IAcrobaticsProficiencySystem.Resolve();
            case Skill.Arcana:
                return IArcanaProficiencySystem.Resolve();
            case Skill.Athletics:
                return IAthleticsProficiencySystem.Resolve();
            case Skill.Crafting:
                return ICraftingProficiencySystem.Resolve();
            case Skill.Deception:
                return IDeceptionProficiencySystem.Resolve();
            case Skill.Diplomacy:
                return IDiplomacyProficiencySystem.Resolve();
            case Skill.Intimidation:
                return IIntimidationProficiencySystem.Resolve();
            case Skill.Lore:
                return ILoreProficiencySystem.Resolve();
            case Skill.Medicine:
                return IMedicineProficiencySystem.Resolve();
            case Skill.Nature:
                return INatureProficiencySystem.Resolve();
            case Skill.Occultism:
                return IOccultismProficiencySystem.Resolve();
            case Skill.Performance:
                return IPerformanceProficiencySystem.Resolve();
            case Skill.Religion:
                return IReligionProficiencySystem.Resolve();
            case Skill.Society:
                return ISocietyProficiencySystem.Resolve();
            case Skill.Stealth:
                return IStealthProficiencySystem.Resolve();
            case Skill.Survival:
                return ISurvivalProficiencySystem.Resolve();
            case Skill.Thievery:
                return IThieveryProficiencySystem.Resolve();
            default:
                return null;
        }
    }
}