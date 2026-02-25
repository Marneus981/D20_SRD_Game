using System;
public enum Skill
//Covers sample character sheet skills
{
    Acrobatics,
    Arcana,
    Athletics,
    Crafting,
    Deception,
    Diplomacy,
    Intimidation,
    Lore,
    Medicine,
    Nature,
    Occultism,
    Performance,
    Religion,
    Society,
    Stealth,
    Survival,
    Thievery
}
public interface ISkillSystem : IDependency<ISkillSystem>
{
    void Set(Entity entity, Skill skill, int value);
    int Get(Entity entity, Skill skill);
    void SetupAllSkills(Entity entity);
    void Setup(Entity entity, Skill skill);
}

public class SkillSystem : ISkillSystem
{
    public void Set(Entity entity, Skill skill, int value)
    {
        GetSystem(skill).Set(entity, value);
    }

    public int Get(Entity entity, Skill skill)
    {
        return GetSystem(skill).Get(entity);
    }

    public void SetupAllSkills(Entity entity)
    {
        foreach (Skill skill in Enum.GetValues(typeof(Skill)))
            GetSystem(skill).Setup(entity);
    }

    public void Setup(Entity entity, Skill skill)
    {
        GetSystem(skill).Setup(entity);
    }

    IBaseSkillSystem GetSystem(Skill skill)
    {
        switch (skill)
        {
            //TODO
            case Skill.Acrobatics:
                return IAcrobaticsSystem.Resolve();
            case Skill.Arcana:
                return IArcanaSystem.Resolve();
            case Skill.Athletics:
                return IAthleticsSystem.Resolve();
            case Skill.Crafting:
                return ICraftingSystem.Resolve();
            case Skill.Deception:
                return IDeceptionSystem.Resolve();
            case Skill.Diplomacy:
                return IDiplomacySystem.Resolve();
            case Skill.Intimidation:
                return IIntimidationSystem.Resolve();
            case Skill.Lore:
                return ILoreSystem.Resolve();
            case Skill.Medicine:
                return IMedicineSystem.Resolve();
            case Skill.Nature:
                return INatureSystem.Resolve();
            case Skill.Occultism:
                return IOccultismSystem.Resolve();
            case Skill.Performance:
                return IPerformanceSystem.Resolve();
            case Skill.Religion:
                return IReligionSystem.Resolve();
            case Skill.Society:
                return ISocietySystem.Resolve();
            case Skill.Stealth:
                return IStealthSystem.Resolve();
            case Skill.Survival:
                return ISurvivalSystem.Resolve();
            case Skill.Thievery:
                return IThieverySystem.Resolve();
            default:
                return null;
        }
    }
}

public partial struct Entity
{
    public int this[Skill skill]
    {
        get { return ISkillSystem.Resolve().Get(this, skill); }
        set { ISkillSystem.Resolve().Set(this, skill, value); }
    }
}