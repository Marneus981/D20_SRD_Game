using Cysharp.Threading.Tasks;

public interface IGameSystem : IDependency<IGameSystem>
{
    UniTask NewGame();
    UniTask ContinueGame();
}

public class GameSystem : IGameSystem
{
    public async UniTask NewGame()
    /*
    NewGame method: Coordinates with DataSystem so that we create a new Data
    object. In the future, we can also use this method to do whatever kind of 
    initial setup of our data that will be needed, 
    such as creating a hero entity and determining an initial story entry.
    */
    {
        var dataSystem = IDataSystem.Resolve();
        dataSystem.Create();
        //Placeholder
        IEntrySystem.Resolve().SetName("Entry_01"); //Update the game system so that it assigns the 
                                                    //initial “Entry” for when you begin a New Game
        await UniTask.CompletedTask;
    }

    public async UniTask ContinueGame()
    {
        IDataSystem.Resolve().Load();
        //Placeholder
        await UniTask.CompletedTask;
    }
}