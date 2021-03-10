using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shoot : MonoBehaviour, IPointerClickHandler
{
    public KanonKule kanonKule;

    private void OnMouseDown()
    {
        kanonKule.Shoot();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        kanonKule.Shoot();
    }
}
