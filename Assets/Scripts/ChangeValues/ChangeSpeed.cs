﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSpeed : MonoBehaviour, IPointerClickHandler
{
    public CannonStateHandler stateHandler;

    [SerializeField]
    private float deltaValue;

    private float highestValue = 50.0f;
    private float lowestValue = 5.0f;

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
}