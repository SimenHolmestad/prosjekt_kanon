using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChangeSpeed : MonoBehaviour, IPointerClickHandler, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    [SerializeField]
    private float deltaValue;

    private float highestValue = 35.0f;
    private float lowestValue = 15.0f;

    private void changeSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        float newSpeed = state.speed + increment_value;
        if (newSpeed >= this.lowestValue && newSpeed <= this.highestValue) 
        {
            state.speed = newSpeed;
        }
        else if (newSpeed < this.lowestValue)
        {
            state.speed = this.lowestValue;
        }
        else if (newSpeed > this.highestValue)
        {
            state.speed = this.highestValue;
        }
        state.speed = (float)Math.Round(state.speed, 1);
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
        gameObject.SetActive(!state.speedIsLocked);
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
