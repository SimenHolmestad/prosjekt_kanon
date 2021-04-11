using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonAndCoordinateSystemManager : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;
    public CannonCylinder cannonCylinder;
    public ShowBulletPath showBulletPath;
    public ThetaPath thetaPath;
    public ThetaLabelAnchor thetaLabelAnchor;
    public PhiPath phiPath;
    public PhiLabelAnchor phiLabelAnchor;
    public VelocityVector velocityVector;
    public XYVelocityVector xyVelocityVector; 
    public VelocityTriangle velocityTriangle; 
    
    private bool acceptReload = false;
    private bool reloadCannon = false; 

    private float oldVerticalAngle; // vertical angle to reload from
    private float oldHorizontalAngle; // horizontal angle to reload from
    private float actualAngle; // dynamical vertical angle during reload
    private float oldSpeed; // speed to reload from
    private string oldLevel; // for avoiding reload after level change 

    private float waitTime = 0.0f; // time parameter for reloading process
    private float waitLength = 1.5f; // time spent in vertical position during reload 
    private float maxSpeed = 35.0f;
    private float minSpeed = 15.0f;
    private float speedSize = 1.0f; // determines general size of velocity vector
    private float speedScale = 0.4f; // velocity vector scales to speedScale*speedSize when speed is minimum

    void Start()
    {
        this.thetaPath.InstantiateThetaPath();
        this.phiPath.InstantiatePhiPath();
        this.showBulletPath.InstantiateBulletPath();

        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState()); 
    }

    public void applyChange(CannonState state){

        if (state.hasLanded){
            this.acceptReload = true;
            this.oldHorizontalAngle = state.horizontalAngle;
            this.oldVerticalAngle = state.verticalAngle;
            this.actualAngle = oldVerticalAngle;
            this.oldSpeed = state.speed;
            this.oldLevel = state.levelName;
        }
        else {
            // execute normal update of coordinate system and (potentially) cannon path
            this.UpdateCannon(state.verticalAngle, state.horizontalAngle, state.height, state.speed, state.isThreeD);

            if (state.levelName != oldLevel){
                acceptReload = false;
            }
        }

        if (state.isThreeD){
            this.phiLabelAnchor.ShowPhiLabel(true);
        }
        else{
            this.phiLabelAnchor.ShowPhiLabel(false);
        }

        if(state.levelName == "Oppgave 0"){
            this.showBulletPath.UpdateBulletPath(state.verticalAngle, state.horizontalAngle, state.speed);
        }
        else{
            
            if(this.showBulletPath.justChangedLevel){
                this.showBulletPath.DestroyBulletPath();
                this.showBulletPath.justChangedLevel = false;
                acceptReload = false; //
            }
        }
    }

    void Update(){  
        if (reloadCannon){
            CannonState state = stateHandler.getCannonState();

            this.phiLabelAnchor.ShowPhiLabel(false);
            this.thetaLabelAnchor.ShowThetaLabel(false);
            this.showBulletPath.DestroyBulletPath();
            SetInMotion();

            if (actualAngle > state.verticalAngle && waitTime > waitLength) // break condition for reloading
            {
                this.UpdateCannon(state.verticalAngle, state.horizontalAngle, state.height, state.speed, state.isThreeD);
                if (state.isThreeD){
                    this.phiLabelAnchor.ShowPhiLabel(true);
                }
                if (state.levelName == "Oppgave 0"){
                    this.showBulletPath.DrawBulletPath(state.speed, state.verticalAngle, state.horizontalAngle);
                }
                this.thetaLabelAnchor.ShowThetaLabel(true);
                this.waitTime = 0f;
                this.acceptReload = false;
                this.reloadCannon = false;
            }
        }
    }

    public void ReloadMotion(){  // allows reload-level-button to reload cannon (if accepting)
        if (acceptReload){
            this.reloadCannon = true;
        }
    }

    private void UpdateCannon(float theta, float phi, float h, float v, bool threeD){ // update everything
        this.cannonCylinder.CannonCylinderOrientation(theta, phi);
        this.cannonCylinder.CannonCylinderPosition(theta, phi, h);
        this.velocityVector.VelocityVectorRescale(v, minSpeed, maxSpeed, speedScale, speedSize);
        this.velocityVector.VelocityVectorOrientation(theta, phi);
        this.velocityVector.VelocityVectorPosition(h);
        this.xyVelocityVector.XYVelocityVectorRescale(theta, v, minSpeed, maxSpeed, speedScale, speedSize);
        this.xyVelocityVector.XYVelocityVectorOrientation(phi);
        this.velocityTriangle.VelocityTriangleRescale(theta, v, minSpeed, maxSpeed, speedScale, speedSize);
        this.velocityTriangle.VelocityTriangleOrientation(phi);
        this.phiPath.CreatePhiPath(theta, phi, v, minSpeed, maxSpeed, speedScale, speedSize, threeD);
        if (threeD){
            this.phiLabelAnchor.PlacePhiLabel(theta, phi, v, minSpeed, maxSpeed, speedScale, speedSize);
        }
        this.thetaPath.CreateThetaPath(theta, phi, h);
        this.thetaLabelAnchor.PlaceThetaLabel(theta, phi, h);
    }

    private void SetInMotion(){
        CannonState state = stateHandler.getCannonState();
        float rotSpeed = 60f; // degrees per second for reloading motion
        float currentSpeed; 

        if (actualAngle > 0f && waitTime <= waitLength){
            actualAngle -= rotSpeed * Time.deltaTime;
            this.UpdateCannon(actualAngle, oldHorizontalAngle, state.height, oldSpeed, state.isThreeD);
        }
        else if (actualAngle <= 0f && waitTime <= waitLength){
            actualAngle = 0f;
            waitTime += Time.deltaTime; 
            currentSpeed = oldSpeed + waitTime/waitLength * (state.speed - oldSpeed); // rescales speed continuously
            this.UpdateCannon(actualAngle, state.horizontalAngle, state.height, currentSpeed, state.isThreeD);
        }
        else if (actualAngle >= 0f && waitTime > waitLength){
            actualAngle += rotSpeed * Time.deltaTime;
            this.UpdateCannon(actualAngle, state.horizontalAngle, state.height, state.speed, state.isThreeD);
        }

    }

}
