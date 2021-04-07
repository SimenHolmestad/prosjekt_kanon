using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class CannonCylinder : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    private bool reloadCannon = false; 
    private bool acceptReload = false; 
    private float actualAngle; 
    private float NewHorizontalAngle;
    private float OldHorizontalAngle;
    private float NewVerticalAngle;
    private float OldVerticalAngle;
    private float CopyHeight;
    private string CopyLevel;
    private float waitTime = 0f; 
    private float waitLength = 1.5f;
    private float length = 1f;

    Vector3 CCPosition(float phi, float theta, float h){
        return new Vector3(length * (float)Math.Sin(theta * (float)Math.PI/180) * (float)Math.Cos(phi * (float)Math.PI/180), h + length * (float)Math.Cos(theta * (float)Math.PI/180), length * (float)Math.Sin(theta * (float)Math.PI/180) * (float)Math.Sin(phi * (float)Math.PI/180));
    }

    Vector3 CCOrientation(float phi, float theta){
        return new Vector3(0, -phi, -theta);
    }

    public void applyChange(CannonState state){
        
        if (state.hasLanded){
            acceptReload = true;
            OldHorizontalAngle = NewHorizontalAngle;
            OldVerticalAngle = NewVerticalAngle;
            actualAngle = OldVerticalAngle;
            CopyLevel = state.levelName;
        }
        else {
            gameObject.transform.position = CCPosition(state.horizontalAngle, state.verticalAngle, state.height);
            gameObject.transform.eulerAngles = CCOrientation(state.horizontalAngle, state.verticalAngle);

            if (state.levelName != CopyLevel){
                acceptReload = false;
            }
        }

        NewHorizontalAngle = state.horizontalAngle; //
        NewVerticalAngle = state.verticalAngle; //
        CopyHeight = state.height; //
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
            gameObject.transform.position = CCPosition(OldHorizontalAngle, actualAngle, CopyHeight);
            gameObject.transform.eulerAngles = CCOrientation(OldHorizontalAngle, actualAngle);
        }
        else if (actualAngle <= 0f && waitTime <= waitLength){
            actualAngle = 0f;
            waitTime += Time.deltaTime; 
            gameObject.transform.position = CCPosition(NewHorizontalAngle, actualAngle, CopyHeight);
            gameObject.transform.eulerAngles = CCOrientation(NewHorizontalAngle, actualAngle);
        }
        else if (actualAngle >= 0f && waitTime > waitLength){
            actualAngle += rotSpeed * Time.deltaTime;
            gameObject.transform.position = CCPosition(NewHorizontalAngle, actualAngle, CopyHeight);
            gameObject.transform.eulerAngles = CCOrientation(NewHorizontalAngle, actualAngle);
        }

    }

    public void ReloadCannonCylinder(){
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
                gameObject.transform.position = CCPosition(NewHorizontalAngle, actualAngle, CopyHeight);
                gameObject.transform.eulerAngles = CCOrientation(NewHorizontalAngle, actualAngle);
                waitTime = 0f;
                acceptReload = false;
                reloadCannon = false;
            }
        }
    }

}
