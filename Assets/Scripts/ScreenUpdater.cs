using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScreenUpdater : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        // TODO: Fix dette
        gameObject.Text = "Starthastighet: " + this.initSpeed;
    }

    void Start()
    {
        stateHandler.subscribe(this);
    }
}
