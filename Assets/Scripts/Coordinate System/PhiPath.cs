using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public class PhiPath : MonoBehaviour
{
    public GameObject TrajectoryPointPrefab;

    private int numOfTrajectoryPoints = 40; 
    private List<GameObject> trajectoryPoints = new List<GameObject>();

    public void InstantiatePhiPath(){
        trajectoryPoints = new List<GameObject>();
    
        // TrajectoryPoints are instatiated
        for(int i=0 ; i < numOfTrajectoryPoints ; i++)
        {
            GameObject dot = (GameObject) Instantiate(TrajectoryPointPrefab);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Add(dot);
        }
    }

    public void CreatePhiPath(float theta, float phi, float v, float v_min, float v_max, float v_scale, float v_size, bool threeD)
    {
        float radius = 0.7f * 5f * (v_scale * (v - v_min)/(v_max - v_min) + v_size - v_scale) * (float)Math.Sin(theta * Math.PI/180);
        float phiParam = 0f;
        float dPhi = (float)Math.PI/(numOfTrajectoryPoints + 1);
        float cannonIntersect = radius * (float)Math.Sin(phi * Math.PI/180);

        foreach (GameObject obj in trajectoryPoints) {
            Vector3 pos = new Vector3(radius * (float)Math.Sin(phiParam), 0f, radius * (float)Math.Cos(phiParam));
            obj.transform.position = pos;

            if(phi >= 0f){
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