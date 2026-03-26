using UnityEngine;
using Cysharp.Threading.Tasks;

public partial class Data
{
    public string encounterName;
}

public interface IEncounterSystem : IDependency<IEncounterSystem>
{
    void SetName(string name);
    string GetName();
    UniTask Setup(IEncounter encounter);
}
[Dependency(typeof(IEncounterSystem))]
public class EncounterSystem : IEncounterSystem
{
    string heroPath = "Assets/Prefabs/Combatants/Heroes/{0}.prefab";
    string monsterPath = "Assets/Prefabs/Combatants/Monsters/{0}.prefab";
    public void SetName(string name)
    {
        IDataSystem.Resolve().Data.encounterName = name;
        if (!string.IsNullOrEmpty(name))////When we set a non-empty Encounter name, then we should clear any Entry name
            IEntrySystem.Resolve().SetName(string.Empty);
    }

    public string GetName()
    {
        return IDataSystem.Resolve().Data.encounterName;
    }
    public async UniTask Setup(IEncounter encounter)
    {
        foreach (var spawn in encounter.MonsterSpawns)
        {
            var monster = await IEntityRecipeSystem.Resolve().Create(spawn.assetName);
            monster.Position = spawn.position;
            await CreateView(monster, monsterPath);
        }

        foreach (var entity in IPartyOrderSystem.Resolve().Table.Keys)
        {
            var hero = entity;
            if (hero.PartyOrder >= encounter.HeroPositions.Count)
                continue;
            hero.Position = encounter.HeroPositions[hero.PartyOrder];
            await CreateView(hero, heroPath);
        }
        IBoardSystem.Resolve().Load(encounter);
    }

    async UniTask CreateView(Entity entity, string path)
    {
        var viewProvider = IEntityViewProvider.Resolve();
        var assetManager = IAssetManager<GameObject>.Resolve();
        var key = string.Format(path, entity.CombatantAsset);
        var view = await assetManager.InstantiateAsync(key);
        view.transform.position = entity.Position;
        viewProvider.SetView(view, entity, ViewZone.Combatant);
        var combatant = view.GetComponent<CombatantView>();
        ICombatantViewSystem.Resolve().SetLayerOrder(combatant, entity.Position.y);//arrange according to y value
    }
}