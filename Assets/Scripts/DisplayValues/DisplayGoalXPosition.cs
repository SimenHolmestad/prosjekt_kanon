using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGoalXPosition : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;
    public Text goalXPosText;

    public void applyChange(CannonState state){
        goalXPosText.text = state.goalXPosition.ToString("0.0") + " m";
    }

    void Start()
    {
        goalXPosText = GameObject.Find("Display_goal_x_position").GetComponent<Text>();

        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
