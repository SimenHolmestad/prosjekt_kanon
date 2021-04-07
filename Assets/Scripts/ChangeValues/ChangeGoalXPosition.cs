using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChangeGoalXPosition : MonoBehaviour, IPointerClickHandler, CannonStateObserver
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
        }
        else if (newXpos < this.lowestValue)
        {
            state.goalXPosition = this.lowestValue;
        }
        else if (newXpos > this.highestValue)
        {
            state.goalXPosition = this.highestValue;
        }
        state.goalXPosition = (float)Math.Round(state.goalXPosition, 1);
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        // Michael: added !hasLanded condition (and getCannonState)
        CannonState state = stateHandler.getCannonState();
        if(!state.hasLanded){ 
            this.changeSpeed(this.deltaValue);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Michael: added !hasLanded condition (and getCannonState)
        CannonState state = stateHandler.getCannonState();
        if(!state.hasLanded){ 
            this.changeSpeed(this.deltaValue);
        }
    }

    public void applyChange(CannonState state){
        gameObject.SetActive(!state.xPositionIsLocked);
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
