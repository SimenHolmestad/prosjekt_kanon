using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeGoalYPosition : MonoBehaviour, IPointerClickHandler
{
    public CannonStateHandler stateHandler;

    [SerializeField]
    private float deltaValue;

    private float highestValue = 20.0f;
    private float lowestValue = -20.0f;

    private void changeSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        float newYpos = state.goalYPosition + increment_value;
        if (newYpos >= this.lowestValue && newYpos <= this.highestValue) {
            state.goalYPosition = newYpos;
            stateHandler.setCannonState(state);
        }
        else if (newYpos < this.lowestValue)
        {
            state.goalYPosition = this.lowestValue;
            stateHandler.setCannonState(state);
        }
        else if (newYpos > this.highestValue)
        {
            state.goalYPosition = this.highestValue;
            stateHandler.setCannonState(state);
        }
    }

    private void OnMouseDown()
    {
        this.changeSpeed(this.deltaValue);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.changeSpeed(this.deltaValue);
    }
}
