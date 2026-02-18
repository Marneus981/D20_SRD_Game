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
        var entry = await IEntryAssetSystem.Resolve().Load();

        // TODO: Resolve the Entry Panel
        var panel = IEntryPanel.Resolve();

        // TODO: Configure the panel with the asset
        panel.Setup(entry);

        // TODO: Enter transition for the panel
        await panel.TransitionIn();

        // MARK: - Loop
        while (true)
        {
            // TODO: Interact with the panel
            // Either select a menu option or interact with a link in the text
            var cts = new CancellationTokenSource();
            var (win, menu, link) = await UniTask.WhenAny(
                    panel.SelectMenuOption(cts.Token),
                    panel.SelectLink(cts.Token)
                );
            cts.Cancel();
            cts.Dispose();

            if (win == 0)
            {
                // Selected a menu option
                entry.Options[menu].Select();
                break;
            }
            else
            {
                // TODO: Selected a link in the text
            }
        }

        // MARK: - Exit
        // TODO: Exit transition for the panel
        IDataSystem.Resolve().Save();
        await panel.TransitionOut();
    }
}