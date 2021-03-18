using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayVerticalAngle : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;
    public Text verticalAngleText;

    public void applyChange(CannonState state){
        verticalAngleText.text = state.verticalAngle.ToString("0") + "°";
    }

    void Start()
    {
        verticalAngleText = GameObject.Find("Display_vertical_angle").GetComponent<Text>();

        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
