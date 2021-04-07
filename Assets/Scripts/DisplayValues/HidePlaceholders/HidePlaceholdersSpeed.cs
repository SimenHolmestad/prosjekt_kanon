using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class HidePlaceholdersSpeed : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        gameObject.SetActive(state.speedIsLocked);        
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
