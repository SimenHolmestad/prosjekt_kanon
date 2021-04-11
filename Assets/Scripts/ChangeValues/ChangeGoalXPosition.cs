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

    private float highestValue;
    private float lowestValue;
    private float maxRadius = 109.0f;
    private float minRadius = 19.0f;

    private void changeSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        // v Limits goal position to a 90 deg cone from the origin (centered at the x-axis)
        this.highestValue = (float)Math.Sqrt(Math.Pow(maxRadius, 2) - Math.Pow(state.goalYPosition, 2));
        if ((float)Math.Abs(state.goalYPosition) >= minRadius / (float)Math.Sqrt(2)){
            this.lowestValue = (float)Math.Abs(state.goalYPosition);
        }
        else{
            this.lowestValue = (float)Math.Sqrt(Math.Pow(minRadius, 2) - Math.Pow(state.goalYPosition, 2));
        }


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
