using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DoubleTapOnCanvas : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnDoubleTapped;
    private int tapCount;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            OnDoubleTapped.Invoke();
        }
    }
}