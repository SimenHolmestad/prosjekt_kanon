using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChangeHorizontalAngle : MonoBehaviour, IPointerClickHandler, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    [SerializeField]
    private float deltaValue;

    private float highestValue = 90.0f;
    private float lowestValue = -90.0f;

    private void changeHorizontalAngle(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        float newHorizontalAngle = state.horizontalAngle + increment_value;
        if (newHorizontalAngle >= this.lowestValue && newHorizontalAngle <= this.highestValue) 
        {
            state.horizontalAngle = newHorizontalAngle;
        }
        else if (newHorizontalAngle < this.lowestValue)
        {
            state.horizontalAngle = this.lowestValue;
        }
        else if (newHorizontalAngle > this.highestValue)
        {
            state.horizontalAngle = this.highestValue;
        }
        state.horizontalAngle = (float)Math.Round(state.horizontalAngle, 1);
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        this.changeHorizontalAngle(this.deltaValue);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.changeHorizontalAngle(this.deltaValue);
    }

    public void applyChange(CannonState state){
        gameObject.SetActive(!state.horizontalAngleIsLocked);
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
