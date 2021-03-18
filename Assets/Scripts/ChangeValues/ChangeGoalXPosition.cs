using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeGoalXPosition : MonoBehaviour, IPointerClickHandler
{
    public CannonStateHandler stateHandler;

    [SerializeField]
    private float deltaValue;

    private float highestValue = 50.0f;
    private float lowestValue = 1.0f;

    private void changeSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        float newXpos = state.goalXPosition + increment_value;
        if (newXpos >= this.lowestValue && newXpos <= this.highestValue) {
            state.goalXPosition = newXpos;
            stateHandler.setCannonState(state);
        }
        else if (newXpos < this.lowestValue)
        {
            state.goalXPosition = this.lowestValue;
            stateHandler.setCannonState(state);
        }
        else if (newXpos > this.highestValue)
        {
            state.goalXPosition = this.highestValue;
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
