using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpeed : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        gameObject.GetComponent<Text>().text = state.speed.ToString("0.0") + " m/s";
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
