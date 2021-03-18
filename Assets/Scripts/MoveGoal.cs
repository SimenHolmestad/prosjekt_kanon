using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGoal : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;
    private float startHeight;
    private Vector3 goalPos;

    // Start is called before the first frame update
    void Start()
    {
        startHeight = gameObject.transform.position.y;
        goalPos = gameObject.transform.position;
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }

    public void applyChange(CannonState state)
    {
        goalPos = new Vector3(state.goalXPosition, startHeight, state.goalYPosition);
        gameObject.transform.position = goalPos;
    }
}