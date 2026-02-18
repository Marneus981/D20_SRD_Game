using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;
using System.Linq;

public interface IEntryPanel : IDependency<IEntryPanel>
/*
The user may interact with links in the text or select an entry option. 
The entry option would exit the current entry’s flow, but the links do not. 
*/
{
    void Setup(IEntry entry);
    UniTask TransitionIn();
    UniTask<int> SelectMenuOption(CancellationToken token);
    UniTask<string> SelectLink(CancellationToken token);
    UniTask TransitionOut();
}

public class EntryPanel : MonoBehaviour, IEntryPanel
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI entryText;
    [SerializeField] List<GameObject> entryOptions;

    public void Setup(IEntry entry)
    //The Setup method is what will configure the UI based on a given "entry"
    {
        // Setup the main entry text
        entryText.text = entry.Text;

        // Setup buttons for entry options
        /*
        Zip will create a new collection from two other collections. 
        New collection is made up of pairs (one from each of the zipped collections). 
        Total length of new coll is shortest length of the two colls. 
            Example: A List of four UI buttons that was zipped with
                    a dynamic number of entry options. There were two 
                    entry options, then the zipped result would hold 
                    two pairs because two is the size of the shortest collection.
                    The first pair would have the first button and first option, 
                    and the second pair would hold the second button and second option.
        */
        var pairs = entryOptions.Zip(entry.Options, (GameObject view, IEntryOption data) => (view, data));
        foreach (var pair in pairs)
        /*
        Iterating over the collection of zipped pairs, we can do our setup. 
        Any button that can be paired with an entry option will be set to active,
        and have its label's text display the option's text. 
        */
        {
            pair.view.SetActive(true);
            var label = pair.view.GetComponentInChildren<TextMeshProUGUI>();
            label.text = pair.data.Text;
        }

        // Hide any unused buttons
        for (int i = pairs.Count(); i < entryOptions.Count; ++i)
            entryOptions[i].SetActive(false);
    }
    const float transitionTime = 0.25f;
    //We are simply using the tween animation library to 
    //FadeIn or FadeOut CanvasGroup
    public async UniTask TransitionIn()
    {
        await canvasGroup
            .FadeIn(transitionTime, EasingEquations.EaseInOutQuad)
            .Play(this.GetCancellationTokenOnDestroy());
    }

    public async UniTask TransitionOut()
    {
        await canvasGroup
            .FadeOut(transitionTime, EasingEquations.EaseInOutQuad)
            .Play(this.GetCancellationTokenOnDestroy());
    }
    public async UniTask<int> SelectMenuOption(CancellationToken token)
    /*
    WhenAny is passed a dynamic list of "Press" button tasks. 
    We loop over our buttons, and for any active button, append a "Press"
    task for the button to that List.
    */
    {
        List<UniTask> tasks = new List<UniTask>(entryOptions.Count);
        for (int i = 0; i < entryOptions.Count; ++i)
        {
            if (!entryOptions[i].activeSelf)
                break;
            var button = entryOptions[i].GetComponent<Button>();
            var task = Press(button, token);
            tasks.Add(task);
        }
        var result = await UniTask.WhenAny(tasks);
        /*
        Compared to choosing a main menu option, we return an int instead
        of an enum. The int that we are returning represents the index of
        the option that the player chose. Providing the index makes more sense
        in this case, because at any given entry, the options can represent
        a different choice.
        */
        return result;
    }

    async UniTask Press(Button button, CancellationToken token)
    {
        using (var handler = button.GetAsyncClickEventHandler(token))
        {
            await handler.OnClickAsync();
        }
    }
    public async UniTask<string> SelectLink(CancellationToken token)
    {
        var linkOpener = entryText.GetComponent<LinkOpener>();
        string result = "";
        using (var handler = linkOpener.onClick.GetAsyncEventHandler(token))
        {
            result = await handler.OnInvokeAsync();
        }
        return result; //Passes along a string parameter representing the link that was clicked.
    }
    private void Awake()
    {
        canvasGroup.alpha = 0; //Make panel hidden while we load the asset and
                                //configure the panel with the asset
    }
    //We allow a MonoBehaviour to handle its own injection via
    //the OnEnable and OnDisable methods.
    private void OnEnable()
    {
        IEntryPanel.Register(this);
    }

    private void OnDisable()
    {
        IEntryPanel.Reset();
    }
}
