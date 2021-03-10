using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUpdater : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;
    public Text speedText;

    public void applyChange(CannonState state){
        // TODO: Fix dette
        speedText.text = "Starthastighet: " + state.speed;
    }

    void Start()
    {
        this.stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());

        speedText = GameObject.Find("Display_speed").GetComponent<Text>();
    }
}