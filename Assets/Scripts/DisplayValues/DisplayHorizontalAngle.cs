using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHorizontalAngle : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;
    public Text horizontalAngleText;

    public void applyChange(CannonState state){
        horizontalAngleText.text = state.horizontalAngle.ToString("0") + "°";
    }

    void Start()
    {
        horizontalAngleText = GameObject.Find("Display_horizontal_angle").GetComponent<Text>();

        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
