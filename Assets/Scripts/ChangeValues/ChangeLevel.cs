using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeLevel : MonoBehaviour, IPointerClickHandler
{
    public CannonStateHandler stateHandler;

    [SerializeField]
    private float deltaValue;

    private void changeLevel(float increment_value) {
        if (deltaValue > 0) {
            this.stateHandler.goToNextLevel();
        } else {
            this.stateHandler.goToPreviousLevel();
        }
    }

    private void OnMouseDown()
    {
        this.changeLevel(this.deltaValue);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.changeLevel(this.deltaValue);
    }
}
