using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecreaseSpeed : MonoBehaviour
{
    public CannonStateHandler stateHandler;
    public Text speedText;

    private void decreaseSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        if (state.speed > increment_value) {
            state.speed -= increment_value;
        }
        speedText.text = "Starthastighet: " + state.speed;
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        this.decreaseSpeed(1.0f);
    }
}