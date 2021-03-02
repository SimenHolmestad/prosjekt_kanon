using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseSpeed : MonoBehaviour
{
    public CannonStateHandler stateHandler;

    private void decreaseSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        state.speed -= increment_value;
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        this.decreaseSpeed(1.0f);
    }
}