using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
public class VelocityVector : MonoBehaviour, CannonStateObserver
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
    private float CopyHeight;
    private float waitTime = 0f; 
    private float waitLength = 1.5f;
    private string CopyLevel;

    private float max_vel = 20f;
    private float min_vel = 10f;

    private void VVRescale(float v){
        Vector3 scale = gameObject.transform.localScale;
        scale.Set(0.7f, 0.2f * (v - min_vel)/(max_vel - min_vel) + 0.5f, 0.7f);
        gameObject.transform.localScale = scale;
    }

    Vector3 VVPosition(float h){
        return new Vector3(0f, h, 0f);
    }

    Vector3 VVOrientation(float phi, float theta){
        return new Vector3(0f, -phi, -theta);
    }

    public void applyChange(CannonState state){

        if (state.hasLanded){
            acceptReload = true;
            OldHorizontalAngle = NewHorizontalAngle;
            OldVerticalAngle = NewVerticalAngle;
            OldSpeed = NewSpeed;
            actualAngle = OldVerticalAngle;
            CopyLevel = state.levelName;
        }
        else {
            gameObject.transform.position = VVPosition(state.height);
            gameObject.transform.eulerAngles = VVOrientation(state.horizontalAngle, state.verticalAngle);
            VVRescale(state.speed);

            if (state.levelName != CopyLevel){
                acceptReload = false;
            }
        }

        NewHorizontalAngle = state.horizontalAngle; 
        NewVerticalAngle = state.verticalAngle; 
        NewSpeed = state.speed; 
        CopyHeight = state.height; 

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
            gameObject.transform.position = VVPosition(CopyHeight);
            gameObject.transform.eulerAngles = VVOrientation(OldHorizontalAngle, actualAngle);
            VVRescale(OldSpeed);
        }
        else if (actualAngle <= 0f && waitTime <= waitLength){
            actualAngle = 0f;
            waitTime += Time.deltaTime; 
            gameObject.transform.position = VVPosition(CopyHeight);
            gameObject.transform.eulerAngles = VVOrientation(NewHorizontalAngle, actualAngle);
            VVRescale(OldSpeed + waitTime/waitLength * (NewSpeed - OldSpeed));
        }
        else if (actualAngle >= 0f && waitTime > waitLength){
            actualAngle += rotSpeed * Time.deltaTime;
            gameObject.transform.position = VVPosition(CopyHeight);
            gameObject.transform.eulerAngles = VVOrientation(NewHorizontalAngle, actualAngle);
            VVRescale(NewSpeed);
        }

    }

    public void ReloadVelocityVector(){
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
                gameObject.transform.position = VVPosition(CopyHeight);
                gameObject.transform.eulerAngles = VVOrientation(NewHorizontalAngle, actualAngle);
                VVRescale(NewSpeed);
                waitTime = 0f;
                acceptReload = false;
                reloadCannon = false;
            }
        }
    }
}
