using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DoubleTapOnCanvas : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnDoubleTapped;
    private int tapCount;
    private float lastTimeClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        float currentTimeClick = eventData.clickTime;
        if (Mathf.Abs(currentTimeClick - lastTimeClick) < 0.75f)
        {
            OnDoubleTapped.Invoke();
        }
        lastTimeClick = currentTimeClick;


       /* if (eventData.clickCount == 2)
        {
            OnDoubleTapped.Invoke();
        }*/
    }
}