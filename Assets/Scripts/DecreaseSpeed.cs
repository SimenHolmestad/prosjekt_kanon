using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DecreaseSpeed : MonoBehaviour, IPointerClickHandler
{
    public CannonStateHandler stateHandler;

    private void decreaseSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        if (state.speed > increment_value) {
            state.speed -= increment_value;
        }
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        this.decreaseSpeed(1.0f);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.decreaseSpeed(1.0f);
    }
}
