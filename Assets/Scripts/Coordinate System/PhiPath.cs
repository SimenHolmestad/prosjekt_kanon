using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public class PhiPath : MonoBehaviour, CannonStateObserver
{
    // TrajectoryPoint will be instantiated
    public CannonStateHandler stateHandler;
    public GameObject TrajectoryPointPrefab;

    private int numOfTrajectoryPoints = 40; 
    private List<GameObject> trajectoryPoints = new List<GameObject>();

    private bool reloadCannon = false; 
    private bool acceptReload = false; 
    private float actualAngle; 
    private float NewHorizontalAngle;
    private float OldHorizontalAngle;
    private float NewVerticalAngle;
    private float OldVerticalAngle;
    private float NewSpeed;
    private float OldSpeed;
    private bool CopyThreeD;
    private string CopyLevel;
    private float waitTime = 0f; 
    private float waitLength = 1.5f;

    private float max_vel = 20f;
    private float min_vel = 10f;

    public void applyChange(CannonState state){

        if (state.hasLanded){
            acceptReload = true;
            OldHorizontalAngle = NewHorizontalAngle;
            OldVerticalAngle = NewVerticalAngle;
            actualAngle = OldVerticalAngle;
            CopyLevel = state.levelName;
        }
        else {
            setTrajectoryPoints(state.verticalAngle, state.horizontalAngle, state.speed, state.isThreeD);

            if (state.levelName != CopyLevel){
                acceptReload = false;
            }
        }

        NewHorizontalAngle = state.horizontalAngle; //
        NewVerticalAngle = state.verticalAngle; //
        CopyThreeD = state.isThreeD; //
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
        
        trajectoryPoints = new List<GameObject>();
    
        // TrajectoryPoints are instatiated
        for(int i=0 ; i < numOfTrajectoryPoints ; i++)
        {
            GameObject dot = (GameObject) Instantiate(TrajectoryPointPrefab);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Add(dot);
        }
    }

    // Following method displays projectile trajectory path
    void setTrajectoryPoints(float verticalTheta, float horizontalPhi, float speedV, bool threeD)
    {
        float radius = 0.7f * 5f * (0.2f * (speedV - min_vel)/(max_vel - min_vel) + 0.5f) * (float)Math.Sin(verticalTheta * Math.PI/180);
        float phiParam = 0f;
        float dPhi = (float)Math.PI/(numOfTrajectoryPoints + 1);
        float cannonIntersect = radius * (float)Math.Sin(horizontalPhi * Math.PI/180);

        foreach (GameObject obj in trajectoryPoints) {
            Vector3 pos = new Vector3(radius * (float)Math.Sin(phiParam), 0f, radius * (float)Math.Cos(phiParam));
            obj.transform.position = pos;

            if(horizontalPhi >= 0f){
                if(pos[2] >= 0 && pos[2] <= cannonIntersect && threeD){
                    obj.GetComponent<Renderer>().enabled = true;
                }
                else{
                    obj.GetComponent<Renderer>().enabled = false;
                }
            }
            else{
                if(pos[2] <= 0 && pos[2] >= cannonIntersect && threeD){
                    obj.GetComponent<Renderer>().enabled = true;
                }
                else{
                    obj.GetComponent<Renderer>().enabled = false;
                }
            }

            phiParam += dPhi;
        }
    }

    void Update(){
        
        UpdateReloadRotation();

    }

    private void CannonRotation(){
        float rotSpeed = 60f;

        if (actualAngle > 0f && waitTime <= waitLength){
            actualAngle -= rotSpeed * Time.deltaTime;
            setTrajectoryPoints(actualAngle, OldHorizontalAngle, OldSpeed, CopyThreeD);
        }
        else if (actualAngle <= 0f && waitTime <= waitLength){
            actualAngle = 0f;
            waitTime += Time.deltaTime; // increasing wait time 
            setTrajectoryPoints(actualAngle, NewHorizontalAngle, OldSpeed + waitTime/waitLength * (NewSpeed - OldSpeed), CopyThreeD);
        }
        else if (actualAngle >= 0f && waitTime > waitLength){
            actualAngle += rotSpeed * Time.deltaTime;
            setTrajectoryPoints(actualAngle, NewHorizontalAngle, NewSpeed, CopyThreeD);
        }

    }

    public void ReloadPhiPath(){
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
                setTrajectoryPoints(actualAngle, NewHorizontalAngle, NewSpeed, CopyThreeD);
                waitTime = 0f;
                acceptReload = false;
                reloadCannon = false;
            }
        }
    }
}