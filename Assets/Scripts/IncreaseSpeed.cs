using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IncreaseSpeed : MonoBehaviour, IPointerClickHandler
{
    public CannonStateHandler stateHandler;

    private void increaseSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        state.speed += increment_value;
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        this.increaseSpeed(1.0f);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.increaseSpeed(1.0f);
    }
}
