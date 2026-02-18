using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class LinkOpener : MonoBehaviour, IPointerClickHandler
/*
The link opener is a script that was attached to the same object as
the main entry’s text component. It implements the IPointerClickHandler 
interface so that it can observe user interactions like mouse clicks 
(also would work with touch input on mobile). It uses a utility to check
if the location of the user interaction overlaps with the location of an
html link in the text. If there is an intersection, then it will invoke 
the “onClick” event and pass along the ID of the link.
*/
{
    public UnityEvent<string> onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        var text = GetComponent<TMP_Text>();
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, null);
        if (linkIndex != -1)
        {
            var linkInfo = text.textInfo.linkInfo[linkIndex];
            onClick.Invoke(linkInfo.GetLinkID());
        }
    }
}