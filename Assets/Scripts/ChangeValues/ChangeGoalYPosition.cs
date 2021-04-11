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

    private float highestValue;
    private float lowestValue;
    private float maxRadius = 109.0f;
    private float minRadius = 19.0f;

    private void changeSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();

        float newYpos = state.goalYPosition + increment_value;

        // v Limits goal position to a 90 deg cone from the origin (centered at the x-axis)
        if (newYpos >= 0){
            if (state.goalXPosition <= maxRadius / (float)Math.Sqrt(2)){
                this.highestValue = state.goalXPosition;
            }
            else{
                this.highestValue = (float)Math.Sqrt(Math.Pow(maxRadius, 2) - Math.Pow(state.goalXPosition, 2));
            }
            if (state.goalXPosition < minRadius){
                this.lowestValue = (float)Math.Sqrt(Math.Pow(minRadius, 2) - Math.Pow(state.goalXPosition, 2));
            }
            else{
                this.lowestValue = 0;
            }
        }
        else{
            if (state.goalXPosition <= maxRadius / (float)Math.Sqrt(2)){
                this.lowestValue = -state.goalXPosition;
            }
            else{
                this.lowestValue = -(float)Math.Sqrt(Math.Pow(maxRadius, 2) - Math.Pow(state.goalXPosition, 2));
            }
            if (state.goalXPosition < minRadius){
                this.highestValue = -(float)Math.Sqrt(Math.Pow(minRadius, 2) - Math.Pow(state.goalXPosition, 2));
            }
            else{
                this.highestValue = 0;
            }
        }

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
        gameObject.SetActive(!state.yPositionIsLocked);
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
