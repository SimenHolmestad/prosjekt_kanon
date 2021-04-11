using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhiLabelAnchor : MonoBehaviour
{

    public void PlacePhiLabel(float theta, float phi, float v, float v_min, float v_max, float v_scale, float v_size){
        float radius = 0.7f * 5f * (v_scale * (v - v_min)/(v_max - v_min) + v_size - v_scale) * (float)Math.Sin(theta * Math.PI/180) + 0.5f;
        gameObject.transform.position = new Vector3(radius * (float)Math.Cos(phi/2 * Math.PI/180), 0.5f, radius * (float)Math.Sin(phi/2 * Math.PI/180));
    }

    public void ShowPhiLabel(bool show){ //
        foreach (Transform child in transform)
            child.gameObject.SetActive(show);
    }
}
