using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class VelocityVector : MonoBehaviour
{
    public void VelocityVectorRescale(float v, float v_min, float v_max, float v_scale, float v_size){
        Vector3 scale = gameObject.transform.localScale;
        scale.Set(v_size, v_scale * (v - v_min)/(v_max - v_min) + v_size - v_scale, v_size); 
        gameObject.transform.localScale = scale;
    }

    public void VelocityVectorPosition(float h){
        gameObject.transform.position = new Vector3(0f, h, 0f);
    }

    public void VelocityVectorOrientation(float theta, float phi){
        gameObject.transform.eulerAngles = new Vector3(0f, -phi, -theta);
    }
}
