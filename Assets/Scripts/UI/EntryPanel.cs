using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;

public interface IEntryPanel : IDependency<IEntryPanel>
{

}

public class EntryPanel : MonoBehaviour, IEntryPanel
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI entryText;
    [SerializeField] List<GameObject> entryOptions;
}
