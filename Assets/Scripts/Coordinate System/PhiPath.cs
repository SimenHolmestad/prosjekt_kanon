using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public class PhiPath : MonoBehaviour, CannonStateObserver
{
    // TrajectoryPoint will be instantiated
    public CannonStateHandler stateHandler;
    public GameObject TrajectoryPointPrefab;

    private int numOfTrajectoryPoints = 40; //maybe 40 here, 20 before
    private List<GameObject> trajectoryPoints = new List<GameObject>();

    public void applyChange(CannonState state){

        setTrajectoryPoints(state.verticalAngle, state.horizontalAngle, state.speed, state.isThreeD);
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
        float max_vel = 20f;
        float min_vel = 10f;
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
}