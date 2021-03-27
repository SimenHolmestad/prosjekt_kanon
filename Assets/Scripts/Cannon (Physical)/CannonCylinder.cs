using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class CannonCylinder : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        float length = 1f;
        gameObject.transform.position = new Vector3(length * (float)Math.Sin(state.verticalAngle * (float)Math.PI/180) * (float)Math.Cos(state.horizontalAngle * (float)Math.PI/180), state.height + length * (float)Math.Cos(state.verticalAngle * (float)Math.PI/180) ,length * (float)Math.Sin(state.verticalAngle * (float)Math.PI/180) * (float)Math.Sin(state.horizontalAngle * (float)Math.PI/180));
        gameObject.transform.eulerAngles = new Vector3(0f, -state.horizontalAngle, -state.verticalAngle);
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
