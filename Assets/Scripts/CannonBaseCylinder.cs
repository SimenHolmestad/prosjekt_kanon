﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class CannonBaseCylinder : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        gameObject.transform.position = new Vector3(0f, state.height - 1.5f, 0f);
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
