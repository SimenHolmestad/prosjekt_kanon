﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
public class VelocityTriangle : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    private bool reloadCannon = false; 
    private bool acceptReload = false; 
    private float actualAngle; 
    private float NewHorizontalAngle;
    private float OldHorizontalAngle;
    private float NewVerticalAngle;
    private float OldVerticalAngle;
    private float NewSpeed;
    private float OldSpeed;
    private float waitTime = 0f; 
    private float waitLength = 1.5f;
    private string CopyLevel;

    private float max_vel = 20f;
    private float min_vel = 10f;

    
    private void VTRescale(float theta, float v){
        Vector3 scale = gameObject.transform.localScale;
        scale.Set((float)Math.Sin(theta * Math.PI/180)*(0.2f * (v - min_vel)/(max_vel - min_vel) + 0.5f), (float)Math.Cos(theta * Math.PI/180)*(0.2f * (v - min_vel)/(max_vel - min_vel) + 0.5f), 0.7f);
        gameObject.transform.localScale = scale;
    }

    Vector3 VTOrientation(float phi){
        return new Vector3(0f, -phi, 0f);
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

        if (state.hasLanded){
            acceptReload = true;
            OldHorizontalAngle = NewHorizontalAngle;
            OldVerticalAngle = NewVerticalAngle;
            OldSpeed = NewSpeed;
            actualAngle = OldVerticalAngle;
            CopyLevel = state.levelName;
        }
        else {
            gameObject.transform.eulerAngles = VTOrientation(state.horizontalAngle);
            VTRescale(state.verticalAngle, state.speed);

            if (state.levelName != CopyLevel){
                acceptReload = false;
            }
        }

        NewHorizontalAngle = state.horizontalAngle; 
        NewVerticalAngle = state.verticalAngle; 
        NewSpeed = state.speed; 
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }

    void Update(){
        
        UpdateReloadRotation();

    }

    private void CannonRotation(){
        float rotSpeed = 60f;

        if (actualAngle > 0f && waitTime <= waitLength){
            actualAngle -= rotSpeed * Time.deltaTime;
            gameObject.transform.eulerAngles = VTOrientation(OldHorizontalAngle);
            VTRescale(actualAngle, OldSpeed);
        }
        else if (actualAngle <= 0f && waitTime <= waitLength){
            actualAngle = 0f;
            waitTime += Time.deltaTime; 
            gameObject.transform.eulerAngles = VTOrientation(NewHorizontalAngle);
            VTRescale(actualAngle, OldSpeed + waitTime/waitLength * (NewSpeed - OldSpeed));
        }
        else if (actualAngle >= 0f && waitTime > waitLength){
            actualAngle += rotSpeed * Time.deltaTime;
            gameObject.transform.eulerAngles = VTOrientation(NewHorizontalAngle);
            VTRescale(actualAngle, NewSpeed);
        }

    }

    public void ReloadVelocityTriangle(){
        if (acceptReload){
            reloadCannon = true;
        }
    }

    private void UpdateReloadRotation(){
        if (reloadCannon){
            
            CannonRotation();

            if (actualAngle > NewVerticalAngle && waitTime > waitLength) // break condition for reloading
            {
                actualAngle = NewVerticalAngle;
                gameObject.transform.eulerAngles = VTOrientation(NewHorizontalAngle);
                VTRescale(actualAngle, NewSpeed);
                waitTime = 0f;
                acceptReload = false;
                reloadCannon = false;
            }
        }
    }
}