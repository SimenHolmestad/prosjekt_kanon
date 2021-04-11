using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThetaLabelAnchor : MonoBehaviour
{
    private float dist = 2.7f;

    public void PlaceThetaLabel(float theta, float phi, float h){
        gameObject.transform.position = new Vector3(dist * (float)Math.Sin(theta/2 * Math.PI/180) * (float)Math.Cos(phi * Math.PI/180), h + dist * (float)Math.Cos(theta/2 * Math.PI/180), dist * (float)Math.Sin(theta/2 * Math.PI/180) * (float)Math.Sin(phi * Math.PI/180));
    }

    public void ShowThetaLabel(bool show){ //
        foreach (Transform child in transform)
            child.gameObject.SetActive(show);
    }
}
