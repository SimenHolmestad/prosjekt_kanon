﻿using UnityEngine;
using UnityEngine.UI;
 
public class LoadTexture : MonoBehaviour, CannonStateObserver
{
	private Texture2D myTexture;
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
		if(state.hasLanded){
			if(state.isCorrect()){
            	state.taskImagePath = "Images/feedback_images/Gratulerer";
			}
			else{
				state.taskImagePath = "Images/feedback_images/Dessverre";
			}
        }
		myTexture = Resources.Load(state.taskImagePath) as Texture2D;
		gameObject.GetComponent<RawImage>().texture = myTexture;
    }
 
	// Use this for initialization
	void Start () {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
	}
}
