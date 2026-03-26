using UnityEngine;
using Cysharp.Threading.Tasks;

public class AppFlow : MonoBehaviour
{
    async UniTaskVoid Start()
    /*
    Returns a lightweight version of UniTask.
    Inside the Start method, we mark the script’s GameObject such that it 
    won’t be destroyed when changing scenes. 
    This is important because we are using a cancellation token that 
    triggers when the object gets destroyed. 
    Now, it can survive the scene change and the game loop can 
    continue running.
    */
    {
        DontDestroyOnLoad(gameObject);
        /*
        IMainMenuFlow.Register(new MainMenuFlow());
        IDataSerializer.Register(new DataSerializer());
        IDataStore.Register(new DataStore("GameData"));
        IDataSystem.Register(new DataSystem());
        */
        //Injector.Inject(); //Replaces above lines; we now have access to every system
        new DependencyInjection().Init();
        ISetUpSystem.Resolve().SetUp();
        while (true)
        {
            //await IMainMenuFlow.Resolve().Play(); 
            await IGameFlow.Resolve().Play(); //Replaces above
            await UniTask.NextFrame(this.GetCancellationTokenOnDestroy());
        }
    }
    private void OnDestroy()
    {
        ITearDownSystem.Resolve().TearDown();
    }
}