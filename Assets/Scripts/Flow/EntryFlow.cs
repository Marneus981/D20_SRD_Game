using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Threading;

public interface IEntryFlow : IDependency<IEntryFlow>
{
    UniTask Play();
}

public class EntryFlow : IEntryFlow
{
    public async UniTask Play()
    {
        // MARK: - Enter
        await SceneManager.LoadSceneAsync("Explore").ToUniTask();
        // TODO: Load an Entry asset by name
        // TODO: Resolve the Entry Panel
        // TODO: Configure the panel with the asset
        // TODO: Enter transition for the panel

        // MARK: - Loop
        while (true)
        {
            // TODO: Interact with the panel
            await UniTask.NextFrame();
        }

        // MARK: - Exit
        // TODO: Exit transition for the panel
    }
}