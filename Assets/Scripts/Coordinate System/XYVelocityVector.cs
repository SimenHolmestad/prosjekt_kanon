﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class XYVelocityVector : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void XYVelocityVectorRescale(float theta, float v, float v_min, float v_max, float v_scale, float v_size){
        Vector3 scale = gameObject.transform.localScale;
        scale.Set(v_size, (v_scale * (v - v_min)/(v_max - v_min) + v_size - v_scale) * (float)Math.Sin(theta * Math.PI/180), v_size);
        gameObject.transform.localScale = scale;
    }

    public void XYVelocityVectorOrientation(float phi){
        gameObject.transform.eulerAngles = new Vector3(0f, -phi, -90f);
    }

    public void applyChange(CannonState state){
        if(state.isThreeD){
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material.color = new Color(1.0f, 0f, 0f, 0.7f);            
            gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer> ().material.color = new Color(1.0f, 0f, 0f, 0.7f);            
            }
        else{
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);            
            gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer> ().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);         
        }
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }

}
