using Cysharp.Threading.Tasks;

public interface IGameFlow : IDependency<IGameFlow>
{
    UniTask Play(); //Single method definition - Play, like in MainMenuFlow
}

public class GameFlow : IGameFlow
{
    public async UniTask Play()
    {
        await Enter();
        await Loop();
        await Exit();
    }
    /*
    When utilizing flows as a replacement for the State Machine Pattern,
    we can make use of the simplicaity of the State Machine interface:
        A State does something when it enters, it often has an update loop, 
        and it does something when it exits. 
    All we are really doing is calling each of those steps in sequence, 
    but it helps visually document what happens and when.
    */
    async UniTask Enter()
    {
        /*
        When a GameFlow “enters” we want to show the main menu
        Note: We use our new GameSystem (because it will be injected to 
        the corresponding interface) to do the real work of creating or 
        loading the game based on the user’s choice.
        */
        var option = await IMainMenuFlow.Resolve().Play();
        switch (option)
        {
            case MainMenuOption.Continue:
                await IGameSystem.Resolve().ContinueGame();
                break;
            case MainMenuOption.NewGame:
                await IGameSystem.Resolve().NewGame();
                break;
        }
    }
    async UniTask Loop()
    //Main game loop; handles exploration, encounters until a win or lost 
    //condition. 
    /*
    {
        await UniTask.CompletedTask;
    }
    */
    {
        while (true) //Replaces above
        {
            var entryName = IEntrySystem.Resolve().GetName();
            if (!string.IsNullOrEmpty(entryName))
                await IEntryFlow.Resolve().Play();
            else
                break;
            await UniTask.NextFrame();
        }
    }
    async UniTask Exit()
    {
        IDataSystem.Resolve().Delete();
        /*
        After determining that the game is “complete”, 
        we can delete the current game data and exit the game flow. 
        The application flow can decide to start another game flow 
        if it wants to.
        */
        await UniTask.CompletedTask;
    }
}