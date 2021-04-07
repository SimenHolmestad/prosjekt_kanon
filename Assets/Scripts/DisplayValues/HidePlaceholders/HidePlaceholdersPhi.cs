using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class HidePlaceholdersPhi : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        gameObject.SetActive(state.horizontalAngleIsLocked); 
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
