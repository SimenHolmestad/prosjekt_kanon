﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChangeVerticalAngle : MonoBehaviour, IPointerClickHandler, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    [SerializeField]
    private float deltaValue;

    private float highestValue = 70.0f;
    private float lowestValue = 20.0f;

    private void changeHorizontalAngle(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        float newVerticalAngle = state.verticalAngle + increment_value;
        if (newVerticalAngle >= this.lowestValue && newVerticalAngle <= this.highestValue) 
        {
            state.verticalAngle = newVerticalAngle;
        }
        else if (newVerticalAngle < this.lowestValue)
        {
            state.verticalAngle = this.lowestValue;
        }
        else if (newVerticalAngle > this.highestValue)
        {
            state.verticalAngle = this.highestValue;
        }
        state.verticalAngle = (float)Math.Round(state.verticalAngle, 1);
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        // Michael: added !hasLanded condition (and getCannonState)
        CannonState state = stateHandler.getCannonState();
        if(!state.hasLanded){ 
            this.changeHorizontalAngle(this.deltaValue);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Michael: added !hasLanded condition (and getCannonState)
        CannonState state = stateHandler.getCannonState();
        if(!state.hasLanded){ 
            this.changeHorizontalAngle(this.deltaValue);
        }
    }

    public void applyChange(CannonState state){
        gameObject.SetActive(!state.verticalAngleIsLocked);
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
