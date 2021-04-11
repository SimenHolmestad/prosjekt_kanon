using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShowBulletPath : MonoBehaviour
{
    public GameObject cannonPathPrefab;

    private int numOfTrajectoryPoints;
    private float ptsPerSecond = 5.0f;
    private float flightTime;
    private float timeStep;
    private float timeParam;
    private float gravConst = 9.81f;
    private float maxTime; // maximum flight time given cannon constraints
    public bool justChangedLevel = true;
    private List<GameObject> trajectoryPoints = new List<GameObject>();
    

    public void InstantiateBulletPath(){
        trajectoryPoints = new List<GameObject>();
    
        this.maxTime = 2.0f * 35.0f * (float)Math.Cos(30f * Math.PI/180) / gravConst;
        numOfTrajectoryPoints = (int)Math.Ceiling(maxTime * ptsPerSecond);
        for(int i=0 ; i < numOfTrajectoryPoints ; i++){
            GameObject dot = (GameObject) Instantiate(cannonPathPrefab);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Add(dot);
        }
    }

    public void DrawBulletPath(float v, float theta, float phi){
        foreach (GameObject obj in trajectoryPoints) {
            if(timeParam < flightTime){
                Vector3 pos = new Vector3(timeParam * v * (float)Math.Sin(theta * Math.PI/180) * (float)Math.Cos(phi * Math.PI/180), timeParam * v * (float)Math.Cos(theta * Math.PI/180) - 0.5f * gravConst * (float)Math.Pow(timeParam, 2), timeParam * v * (float)Math.Sin(theta * Math.PI/180) * (float)Math.Sin(phi * Math.PI/180));
                obj.transform.position = pos;
                obj.GetComponent<Renderer>().enabled = true;
                this.timeParam += timeStep;
            }
        }
        this.timeParam = 0.0f;
    }

    public void UpdateBulletPath(float theta, float phi, float v){ //
            
        this.justChangedLevel = true;
        this.timeParam = 0.0f;
        this.flightTime = 2f * v / 9.81f * (float)Math.Cos(theta * Math.PI/180);
        this.timeStep = 1f / ptsPerSecond;

        this.DestroyBulletPath();
        this.DrawBulletPath(v, theta, phi);
    }

    public void DestroyBulletPath(){
        foreach (GameObject obj in trajectoryPoints) {
            obj.GetComponent<Renderer>().enabled = false;
        }
    }
}
