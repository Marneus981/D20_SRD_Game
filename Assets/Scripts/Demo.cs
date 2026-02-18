using UnityEngine;
using Cysharp.Threading.Tasks;

public class Demo : MonoBehaviour
{
    [SerializeField] Entry entry;

    async UniTaskVoid Start()
    {
        var panel = IEntryPanel.Resolve();
        panel.Setup(entry);
        await panel.TransitionIn();
    }
}