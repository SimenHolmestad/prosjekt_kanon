using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStateHandler : MonoBehaviour
{
    private CannonState cannonState;
    private List<CannonStateObserver> observers;
    private LevelHandler levelHandler;
    void Awake()
    {
        this.observers = new List<CannonStateObserver>();
        this.levelHandler = new LevelHandler();
        this.resetLevel();
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

    public void resetLevel() {
        this.setCannonState(this.levelHandler.getCurrentLevelState());
    }

    public void goToNextLevel() {
        int level_number = this.levelHandler.getCurrentLevelNumber();
        this.levelHandler.goToNextLevel();
        int next_level_number = this.levelHandler.getCurrentLevelNumber();
        if (level_number != next_level_number){
            this.resetLevel();
        }
    }

    public void goToPreviousLevel() {
        int level_number = this.levelHandler.getCurrentLevelNumber();
        this.levelHandler.goToPreviousLevel();
        int next_level_number = this.levelHandler.getCurrentLevelNumber();
        if (level_number != next_level_number){
            this.resetLevel();
        }
    }
}
