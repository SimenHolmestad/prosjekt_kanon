using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reload : MonoBehaviour, IPointerClickHandler
{
    public KanonKule kanonKule;

    private void OnMouseDown()
    {
        kanonKule.Reload();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        kanonKule.Reload();
    }
}
