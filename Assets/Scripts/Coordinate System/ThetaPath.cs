using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public class ThetaPath : MonoBehaviour
{
    public GameObject TrajectoryPointPrefab;

    private int numOfTrajectoryPoints = 20;
    private List<GameObject> trajectoryPoints = new List<GameObject>();

    public void InstantiateThetaPath(){
        trajectoryPoints = new List<GameObject>();
    
        for(int i=0 ; i < numOfTrajectoryPoints ; i++)
        {
            GameObject dot = (GameObject) Instantiate(TrajectoryPointPrefab);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Add(dot);
        }
    }

    public void CreateThetaPath(float theta, float phi, float h)
    {
        float radius = 2f;
        float thetaParam = 0f;
        float dTheta = (float)Math.PI/(2f * (numOfTrajectoryPoints + 1));
        float cannonIntersect = radius * (float)Math.Cos(theta * Math.PI/180);

        foreach (GameObject obj in trajectoryPoints) {
            Vector3 pos = new Vector3(radius * (float)Math.Sin(thetaParam) * (float)Math.Cos(phi * Math.PI/180), radius * (float)Math.Cos(thetaParam) + h , radius * (float)Math.Sin(thetaParam) * (float)Math.Sin(phi * Math.PI/180));
            obj.transform.position = pos;
            if(pos[1] >= cannonIntersect + h){
                obj.GetComponent<Renderer>().enabled = true;
            }
            else{
                obj.GetComponent<Renderer>().enabled = false;
            }
            thetaParam += dTheta;
        }
    }
}