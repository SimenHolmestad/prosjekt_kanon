using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public class ThetaPath : MonoBehaviour, CannonStateObserver
{
    // TrajectoryPoint will be instantiated
    public CannonStateHandler stateHandler;
    public GameObject TrajectoryPointPrefab;

    private int numOfTrajectoryPoints = 20;
    private List<GameObject> trajectoryPoints = new List<GameObject>();

    public void applyChange(CannonState state){

        setTrajectoryPoints(state.verticalAngle, state.horizontalAngle, state.height);
    }

    void Start()
    {
        trajectoryPoints = new List<GameObject>();
    
        // TrajectoryPoints are instatiated
        for(int i=0 ; i < numOfTrajectoryPoints ; i++)
        {
            GameObject dot = (GameObject) Instantiate(TrajectoryPointPrefab);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Add(dot);
        }

        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
        
    }

    // Following method displays projectile trajectory path
    void setTrajectoryPoints(float verticalTheta, float horizontalPhi, float heightH)
    {
        float radius = 2f;
        float thetaParam = 0f;
        float dTheta = (float)Math.PI/(2f * (numOfTrajectoryPoints + 1));
        float cannonIntersect = radius * (float)Math.Cos(verticalTheta * Math.PI/180);

        foreach (GameObject obj in trajectoryPoints) {
            Vector3 pos = new Vector3(radius * (float)Math.Sin(thetaParam) * (float)Math.Cos(horizontalPhi * Math.PI/180), radius * (float)Math.Cos(thetaParam) + heightH , radius * (float)Math.Sin(thetaParam) * (float)Math.Sin(horizontalPhi * Math.PI/180));
            obj.transform.position = pos;
            if(pos[1] >= cannonIntersect + heightH){
                obj.GetComponent<Renderer>().enabled = true;
            }
            else{
                obj.GetComponent<Renderer>().enabled = false;
            }
            thetaParam += dTheta;
        }
    }
}