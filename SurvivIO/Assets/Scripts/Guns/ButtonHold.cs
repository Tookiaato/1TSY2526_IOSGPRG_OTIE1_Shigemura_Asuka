using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonHeld = false;
    private float heldTime;

    public void OnPointerDown(PointerEventData eventData)
    {
        heldTime += Time.deltaTime;
        buttonHeld = true;
        
        Debug.Log(buttonHeld);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        heldTime = 0;
        buttonHeld = false;
    }
}
