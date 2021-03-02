using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : MonoBehaviour
{
    public CannonStateHandler stateHandler;

    private void increaseSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        state.speed += increment_value;
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        this.increaseSpeed(1.0f);
    }
}

// Kan prøve denne?
// https://assetstore.unity.com/packages/tools/physics/vr-buttons-and-levers-66520