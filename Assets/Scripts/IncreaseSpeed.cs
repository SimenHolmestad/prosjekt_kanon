using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseSpeed : MonoBehaviour
{
    public CannonStateHandler stateHandler;
    public Text speedText;

    private void increaseSpeed(float increment_value) {
        CannonState state = stateHandler.getCannonState();
        state.speed += increment_value;
        speedText.text = "Starthastighet: " + state.speed;
        stateHandler.setCannonState(state);
    }

    private void OnMouseDown()
    {
        this.increaseSpeed(1.0f);
    }
}