using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStateHandler : MonoBehaviour
{
    private CannonState cannonState;
    private List<CannonStateObserver> observers;
    void Start()
    {
        this.cannonState = new CannonState();
        this.observers = new List<CannonStateObserver>();
    }

    public void subscribe(CannonStateObserver observer){
        this.observers.Add(observer);
    }

    public void notifyObservers(){
        foreach (CannonStateObserver observer in this.observers) {
            observer.applyChange(this.cannonState);
        }
    }

    public CannonState getCannonState(){
        return this.cannonState;
    }

    public void setCannonState(CannonState state){
        this.cannonState = state;
        this.notifyObservers();
    }
}

