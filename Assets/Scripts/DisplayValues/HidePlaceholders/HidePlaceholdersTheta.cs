using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class HidePlaceholdersTheta : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        gameObject.SetActive(state.verticalAngleIsLocked);        
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
