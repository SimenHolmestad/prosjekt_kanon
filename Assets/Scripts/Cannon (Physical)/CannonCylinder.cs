using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class CannonCylinder : MonoBehaviour
{
    private float length = 1f;

    public void CannonCylinderPosition(float theta, float phi, float h){ //
        gameObject.transform.position = new Vector3(length * (float)Math.Sin(theta * (float)Math.PI/180) * (float)Math.Cos(phi * (float)Math.PI/180), h + length * (float)Math.Cos(theta * (float)Math.PI/180), length * (float)Math.Sin(theta * (float)Math.PI/180) * (float)Math.Sin(phi * (float)Math.PI/180));
    }

    public void CannonCylinderOrientation(float theta, float phi){ //
        gameObject.transform.eulerAngles = new Vector3(0, -phi, -theta);
    }

}
