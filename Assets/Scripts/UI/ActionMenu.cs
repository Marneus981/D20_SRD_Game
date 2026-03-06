using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Linq;
using TMPro;

public class ActionMenu : MonoBehaviour, IActionMenu
{
    [SerializeField] RectTransform rootPanel;
    [SerializeField] List<Button> buttons;
    [SerializeField] Layout onScreen;//Layout configuration representing how to display the menu.
    [SerializeField] Layout offScreen;//Layout configuration representing how to hide the menu.

    Entity entity;//Holds a convenient reference to the Entity whose turn it is
    int selection;//Holds the currently selected menu option
    int menuCount;//Holds the number of menu items that the menu has

    public async UniTask Setup()
    {
        selection = 0;
        buttons[0].Select();
        entity = ITurnSystem.Resolve().Current;
        var pairs = buttons.Zip(entity.EncounterActions.names, (Button button, string action) => (button, action));
        foreach (var pair in pairs)
        {
            var label = pair.button.GetComponentInChildren<TextMeshProUGUI>();
            label.text = pair.action;
        }
        menuCount = pairs.Count();
        await UniTask.CompletedTask;
    }
    public async UniTask TransitionIn()
    {
        await rootPanel.Layout(offScreen, onScreen, 0.25f).Play();
    }
    public async UniTask<string> SelectMenuItem()
    {
        var input = IInputSystem.Resolve();
        while (true)
        {
            await UniTask.NextFrame();
            if (input.GetKeyUp(InputAction.Confirm))
                break;

            var offset = -input.GetAxisUp(InputAxis.Vertical);
            if (offset == 0)
                continue;

            selection = (selection + offset + menuCount) % menuCount; //To wrap around the menu
            buttons[selection].Select();
        }
        return entity.EncounterActions.names[selection];
    }
    public async UniTask TransitionOut()
    {
        await rootPanel.Layout(onScreen, offScreen, 0.25f).Play();
    }
    private void OnEnable()
    {
        IActionMenu.Register(this);
    }

    private void OnDisable()
    {
        IActionMenu.Reset();
    }
}
public interface IActionMenu : IDependency<IActionMenu>
{
    UniTask Setup(); 
    UniTask TransitionIn();
    UniTask<string> SelectMenuItem();
    UniTask TransitionOut();
}
