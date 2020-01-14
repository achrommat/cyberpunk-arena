using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FixedJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
        backsprite.gameObject.SetActive(true);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        backsprite.gameObject.SetActive(false);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        backsprite.gameObject.SetActive(true);
        base.OnPointerUp(eventData);
    }
}