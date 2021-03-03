using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class KanonKule : MonoBehaviour, CannonStateObserver
{
    private bool isMoving = false;
    private float totalTime = 0.0f;
    private float initSpeed; // Initial speed in m/s
    private float initAngleDeg; // Initial angle in degrees
    private float gravConst = 9.81f;
    private Vector3 startPos;
    public CannonStateHandler stateHandler;

    private bool reLoading = false;
    private float reloadOffset = 5; // Reloading starting hight (y-axis)
    private Vector3 reloadPos;

    public void applyChange(CannonState state){
        this.initSpeed = state.speed;
        this.initAngleDeg = state.verticalAngle;
        Debug.Log("Speed is now " + this.initSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
        reloadPos = startPos + new Vector3(0, reloadOffset, 0);
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }

    Vector3 CalculateRelativePosition(float totalTime) 
    {
        // How do we only calculate these once? Maybe calculate before passing to class? 
        float initAngleRad = initAngleDeg * (float)Math.PI / 180f;
        float initSpeed_y = initSpeed * (float)Math.Sin(initAngleRad);
        float initSpeed_x = initSpeed * (float)Math.Cos(initAngleRad);
        return new Vector3(totalTime * initSpeed_x, totalTime * initSpeed_y - gravConst * (float)Math.Pow(totalTime, 2) / 2, 0);
    }

    // Set the new position of the object based on the result of CalculateRelativePosition
    private void PlaceObject() 
    {
        totalTime += Time.deltaTime;
        Vector3 movement = CalculateRelativePosition(totalTime);
        gameObject.transform.position = startPos + movement;
    }

    // Sets the position of the object based on gravity alone. Input: starting position (constant)
    private void FreeFall(Vector3 startFFPos)
    {
        totalTime += Time.deltaTime;
        Vector3 freeFallMovement = new Vector3(0, -gravConst*(float)Math.Pow(totalTime,2)/2, 0);
        gameObject.transform.position = startFFPos + freeFallMovement;
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot
        if(Input.GetKeyDown(KeyCode.F) && (gameObject.transform.position == startPos)) 
        {
            isMoving = !isMoving;
        }
        if (isMoving) 
        {
            PlaceObject();
            
            if(gameObject.transform.position.y <= 0f)
            {
                isMoving = false;
                totalTime = 0;
            }
        }        

        // Reload
        if(Input.GetKeyDown(KeyCode.R) && !isMoving && (gameObject.transform.position.x != startPos.x))
        {
            reLoading = true;
        }
        if (reLoading)
        {
            FreeFall(reloadPos);

            if(gameObject.transform.position.y <= 0f)
            {
                gameObject.transform.position = startPos;
                reLoading = false;
                totalTime = 0;
            }
        }
    }
}
