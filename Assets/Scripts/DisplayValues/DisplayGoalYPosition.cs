using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGoalYPosition : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;
    public Text goalYPosText;

    public void applyChange(CannonState state){
        goalYPosText.text = state.goalYPosition.ToString("0.0") + " m";
    }

    void Start()
    {
        goalYPosText = GameObject.Find("Display_goal_y_position").GetComponent<Text>();

        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
