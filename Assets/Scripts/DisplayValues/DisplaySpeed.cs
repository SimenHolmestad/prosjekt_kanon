using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpeed : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;
    public Text speedText;

    public void applyChange(CannonState state){
        speedText.text = state.speed.ToString("0.0") + " m/s";
    }

    void Start()
    {
        speedText = GameObject.Find("Display_speed").GetComponent<Text>();

        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
