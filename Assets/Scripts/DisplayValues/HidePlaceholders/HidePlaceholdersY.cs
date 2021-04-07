using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class HidePlaceholdersY : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        gameObject.SetActive(state.yPositionIsLocked);        
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
