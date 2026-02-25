public static class SkillsInjector
{
    public static void Inject()
    {
        IAthleticsProficiencySystem.Register(new AthleticsProficiencySystem());
        IAthleticsSystem.Register(new AthleticsSystem());
        //TODO
        IProficiencySystem.Register(new ProficiencySystem());
        ISkillSystem.Register(new SkillSystem());
    }
}