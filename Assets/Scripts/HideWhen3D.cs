using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWhen3D : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        gameObject.SetActive(state.isThreeD);
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
