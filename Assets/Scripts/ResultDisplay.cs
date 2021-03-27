using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    private Texture2D myTexture;
    private string correctImagePath = "Images/feedback_images/congratulations";
    private string notCorrectImagePath = "Images/feedback_images/fail";

    public void applyChange(CannonState state){
        if (state.hasLanded) {
            this.updateDisplay(state.isCorrect());
        }
    }

    private void updateDisplay(bool isCorrectSolution){
        if (isCorrectSolution) {
            myTexture = Resources.Load(correctImagePath) as Texture2D;
        } else {
            myTexture = Resources.Load(notCorrectImagePath) as Texture2D;
        }
		gameObject.GetComponent<RawImage>().texture = myTexture;
    }

    void Start () {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
