using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class MainMenu : MonoBehaviour, IMainMenu
{
    [SerializeField] RectTransform rootPanel;
    [SerializeField] CanvasGroup rootGroup;
    [SerializeField] CanvasGroup menuGroup;
    [SerializeField] Layout offscreen;
    [SerializeField] Layout onscreen;
    [SerializeField] Button continueButton;
    [SerializeField] Button newGameButton;

    CancellationTokenSource cts = new CancellationTokenSource();
        /*
        Fix for when we call Cancel on the Token’s Source 
        if the animation actually completes, or is skipped. 
        When we simply quit the app, neither one of those scenarios can occur.
        */
    public void Setup(bool hasSavedGame)
    {
        continueButton.gameObject.SetActive(hasSavedGame);
    }

    public async UniTask TransitionIn()
    {
        //var cts = new CancellationTokenSource(); not needed bc of class field
        await UniTask.WhenAny(
            Enter(cts),
            SkipEnter(cts));
    }

    void CancelToken()
{
    if (cts != null)
    {
        cts.Cancel();
        cts.Dispose();
        cts = null;
    }
}

    async UniTask Enter(CancellationTokenSource cts)
    {
        rootPanel.SetLayout(offscreen);
        menuGroup.alpha = 0;
        rootGroup.alpha = 1;
        await rootPanel.Layout(offscreen, onscreen, 5).Play(cts.Token);
        await menuGroup.FadeIn(1).Play(cts.Token);
        CancelToken();
    }

    async UniTask SkipEnter(CancellationTokenSource cts)
    {
        while (true)
        {
            await UniTask.NextFrame(cts.Token);
            if (Input.anyKey)
            {
                CancelToken();
                rootPanel.SetLayout(onscreen);
                menuGroup.alpha = 1;
                break;
            }
        }
    }

    public async UniTask<MainMenuOption> SelectMenuOption() //Will return MainMenuOption enum
    {
        var result = await UniTask.WhenAny(
            Press(newGameButton),
            Press(continueButton)
            );
        return (MainMenuOption)result; //Will return the index of the first task that completes
                                       // then cast the index to the enum and return it.                      
    }

    async UniTask Press(Button button)
    /*
    Observes the OnClick UI event from a passed UI Button. 
    Click waiting wrapped in handler that observes special type of cancellation token – GetCancellationTokenOnDestroy.
    When the script is destroyed, the token will be marked as cancelled and any still running task will be cancelled
    along with it.
    In this implementation, we never actually disable interaction on any of the menu buttons – 
    before or after interaction. User can still click either button, but there is only a brief lapase where the app 
    “responds” to a button click, and even then it is only to the first option that is clicked.
    */
    {
        using (var handler = button.GetAsyncClickEventHandler(this.GetCancellationTokenOnDestroy()))
        {
            await handler.OnClickAsync();
        }
    }

    public async UniTask TransitionOut()
    /*
    Awaits the FadeOut animation of rootGroup CanvasGroup – 
    Root level so that both the logo and menu buttons fade out together.
    */
    {
        await rootGroup.FadeOut().Play(this.GetCancellationTokenOnDestroy());
    }
    //We can use the MonoBehaviour methods OnEnable and OnDisable to 
    //handle registering and resetting registration of our interface.
    void OnEnable()
    {
        IMainMenu.Register(this);
    }

    void OnDisable()
    {
        CancelToken();
        IMainMenu.Reset(); //Correct Order?
    }
}

public enum MainMenuOption
{
    NewGame,
    Continue
}

public interface IMainMenu : IDependency<IMainMenu>
{
    void Setup(bool hasSavedGame);
    UniTask TransitionIn();
    UniTask<MainMenuOption> SelectMenuOption();
    UniTask TransitionOut();
}