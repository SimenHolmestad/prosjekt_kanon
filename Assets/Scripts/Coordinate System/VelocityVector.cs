using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
public class VelocityVector : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public void applyChange(CannonState state){
        float max_vel = 20f;
        float min_vel = 10f;

        gameObject.transform.position = new Vector3(0f, state.height, 0f);
        gameObject.transform.eulerAngles = new Vector3(0f, -state.horizontalAngle, -state.verticalAngle);

        Vector3 scale = gameObject.transform.localScale;
        scale.Set(0.7f, 0.2f * (state.speed - min_vel)/(max_vel - min_vel) + 0.5f, 0.7f);
        gameObject.transform.localScale = scale;
    }

    void Start()
    {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }
}
