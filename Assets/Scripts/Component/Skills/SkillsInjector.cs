public static class SkillsInjector
{
    public static void Inject()
    {
        IAcrobaticsProficiencySystem.Register(new AcrobaticsProficiencySystem());
        IAcrobaticsSystem.Register(new AcrobaticsSystem());
        IArcanaProficiencySystem.Register(new ArcanaProficiencySystem());
        IArcanaSystem.Register(new ArcanaSystem());
        IAthleticsProficiencySystem.Register(new AthleticsProficiencySystem());
        IAthleticsSystem.Register(new AthleticsSystem());
        ICraftingProficiencySystem.Register(new CraftingProficiencySystem());
        ICraftingSystem.Register(new CraftingSystem());
        IDeceptionProficiencySystem.Register(new DeceptionProficiencySystem());
        IDeceptionSystem.Register(new DeceptionSystem());
        IDiplomacyProficiencySystem.Register(new DiplomacyProficiencySystem());
        IDiplomacySystem.Register(new DiplomacySystem());
        IIntimidationProficiencySystem.Register(new IntimidationProficiencySystem());
        IIntimidationSystem.Register(new IntimidationSystem());
        ILoreProficiencySystem.Register(new LoreProficiencySystem());
        ILoreSystem.Register(new LoreSystem());
        IMedicineProficiencySystem.Register(new MedicineProficiencySystem());
        IMedicineSystem.Register(new MedicineSystem());
        INatureProficiencySystem.Register(new NatureProficiencySystem());
        INatureSystem.Register(new NatureSystem());
        IOccultismProficiencySystem.Register(new OccultismProficiencySystem());
        IOccultismSystem.Register(new OccultismSystem());
        IPerformanceProficiencySystem.Register(new PerformanceProficiencySystem());
        IPerformanceSystem.Register(new PerformanceSystem());
        IReligionProficiencySystem.Register(new ReligionProficiencySystem());
        IReligionSystem.Register(new ReligionSystem());
        ISocietyProficiencySystem.Register(new SocietyProficiencySystem());
        ISocietySystem.Register(new SocietySystem());
        IStealthProficiencySystem.Register(new StealthProficiencySystem());
        IStealthSystem.Register(new StealthSystem());
        ISurvivalProficiencySystem.Register(new SurvivalProficiencySystem());
        ISurvivalSystem.Register(new SurvivalSystem());
        IThieveryProficiencySystem.Register(new ThieveryProficiencySystem());
        IThieverySystem.Register(new ThieverySystem());
        IProficiencySystem.Register(new ProficiencySystem());
        ISkillSystem.Register(new SkillSystem());
    }
    public static void SetUp()
    {
        IAcrobaticsProficiencySystem.Resolve().SetUp();
        IAcrobaticsSystem.Resolve().SetUp();
        IArcanaProficiencySystem.Resolve().SetUp();
        IArcanaSystem.Resolve().SetUp();
        IAthleticsProficiencySystem.Resolve().SetUp();
        IAthleticsSystem.Resolve().SetUp();
        ICraftingProficiencySystem.Resolve().SetUp();
        ICraftingSystem.Resolve().SetUp();
        IDeceptionProficiencySystem.Resolve().SetUp();
        IDeceptionSystem.Resolve().SetUp();
        IDiplomacyProficiencySystem.Resolve().SetUp();
        IDiplomacySystem.Resolve().SetUp();
        IIntimidationProficiencySystem.Resolve().SetUp();
        IIntimidationSystem.Resolve().SetUp();
        ILoreProficiencySystem.Resolve().SetUp();
        ILoreSystem.Resolve().SetUp();
        IMedicineProficiencySystem.Resolve().SetUp();
        IMedicineSystem.Resolve().SetUp();
        INatureProficiencySystem.Resolve().SetUp();
        INatureSystem.Resolve().SetUp();
        IOccultismProficiencySystem.Resolve().SetUp();
        IOccultismSystem.Resolve().SetUp();
        IPerformanceProficiencySystem.Resolve().SetUp();
        IPerformanceSystem.Resolve().SetUp();
        IProficiencySystem.Resolve().SetUp();
        IReligionProficiencySystem.Resolve().SetUp();
        IReligionSystem.Resolve().SetUp();
        ISkillSystem.Resolve().SetUp();
        ISocietyProficiencySystem.Resolve().SetUp();
        ISocietySystem.Resolve().SetUp();
        IStealthProficiencySystem.Resolve().SetUp();
        IStealthSystem.Resolve().SetUp();
        ISurvivalProficiencySystem.Resolve().SetUp();
        ISurvivalSystem.Resolve().SetUp();
        IThieveryProficiencySystem.Resolve().SetUp();
        IThieverySystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        IAcrobaticsProficiencySystem.Resolve().TearDown();
        IAcrobaticsSystem.Resolve().TearDown();
        IArcanaProficiencySystem.Resolve().TearDown();
        IArcanaSystem.Resolve().TearDown();
        IAthleticsProficiencySystem.Resolve().TearDown();
        IAthleticsSystem.Resolve().TearDown();
        ICraftingProficiencySystem.Resolve().TearDown();
        ICraftingSystem.Resolve().TearDown();
        IDeceptionProficiencySystem.Resolve().TearDown();
        IDeceptionSystem.Resolve().TearDown();
        IDiplomacyProficiencySystem.Resolve().TearDown();
        IDiplomacySystem.Resolve().TearDown();
        IIntimidationProficiencySystem.Resolve().TearDown();
        IIntimidationSystem.Resolve().TearDown();
        ILoreProficiencySystem.Resolve().TearDown();
        ILoreSystem.Resolve().TearDown();
        IMedicineProficiencySystem.Resolve().TearDown();
        IMedicineSystem.Resolve().TearDown();
        INatureProficiencySystem.Resolve().TearDown();
        INatureSystem.Resolve().TearDown();
        IOccultismProficiencySystem.Resolve().TearDown();
        IOccultismSystem.Resolve().TearDown();
        IPerformanceProficiencySystem.Resolve().TearDown();
        IPerformanceSystem.Resolve().TearDown();
        IProficiencySystem.Resolve().TearDown();
        IReligionProficiencySystem.Resolve().TearDown();
        IReligionSystem.Resolve().TearDown();
        ISkillSystem.Resolve().TearDown();
        ISocietyProficiencySystem.Resolve().TearDown();
        ISocietySystem.Resolve().TearDown();
        IStealthProficiencySystem.Resolve().TearDown();
        IStealthSystem.Resolve().TearDown();
        ISurvivalProficiencySystem.Resolve().TearDown();
        ISurvivalSystem.Resolve().TearDown();
        IThieveryProficiencySystem.Resolve().TearDown();
        IThieverySystem.Resolve().TearDown();
    }
}