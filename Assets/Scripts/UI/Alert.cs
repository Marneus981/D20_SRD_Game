using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public interface IAlert : IDependency<IAlert>
{
    
}

public class Alert : MonoBehaviour, IAlert
{
    [SerializeField] RectTransform rootPanel;
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] Button button;
}
