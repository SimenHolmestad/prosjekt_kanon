using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChangeGoalYPosition : MonoBehaviour, IPointerClickHandler, CannonStateObserver
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
        }
        else if (newYpos < this.lowestValue)
        {
            state.goalYPosition = this.lowestValue;
        }
        else if (newYpos > this.highestValue)
        {
            state.goalYPosition = this.highestValue;
        }
        state.goalYPosition = (float)Math.Round(state.goalYPosition, 1);
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        this.changeSpeed(this.deltaValue);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.changeSpeed(this.deltaValue);
    }

    public void applyChange(CannonState state){
        gameObject.SetActive(!state.yPositionIsLocked);
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
