﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class KanonKule : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    private bool isMoving = false;
    private float totalTime = 0.0f;
    private float initSpeed; // Initial speed in m/s
    private float initHorizontalAngleDeg; // Initial horizontal angle in degrees
    private float initVerticalAngleDeg; // Initial vertical angle in degrees
    private float cannonHeight; // Height of cannon in m/s
    private float gravConst = 9.81f;
    private Vector3 startPos;

    private bool reLoading = false;
    private float reloadOffset = 10; // Reloading starting hight (y-axis)
    private Vector3 reloadPos;

    public void applyChange(CannonState state){
        if (!isMoving)
        {
            this.initSpeed = state.speed;
            this.initVerticalAngleDeg = state.verticalAngle;
            this.initHorizontalAngleDeg = state.horizontalAngle;
            this.cannonHeight = state.height;
            gameObject.transform.position = new Vector3(0f, cannonHeight, 0f);
            startPos = gameObject.transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
        reloadPos = startPos + new Vector3(0, reloadOffset, 0);
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCannonPosition();

        UpdateReloadPosition();
    }

    Vector3 CalculateRelativePosition(float totalTime)
    {
        float initVerticalAngleRad = initVerticalAngleDeg * (float)Math.PI / 180f;
        float initHorizontalAngleRad = initHorizontalAngleDeg * (float)Math.PI / 180f;
        float initSpeed_x = initSpeed * (float)Math.Sin(initVerticalAngleRad) * (float)Math.Cos(initHorizontalAngleRad);
        float initSpeed_y = initSpeed * (float)Math.Sin(initVerticalAngleRad) * (float)Math.Sin(initHorizontalAngleRad);
        float initSpeed_z = initSpeed * (float)Math.Cos(initVerticalAngleRad);
        return new Vector3(totalTime * initSpeed_x, totalTime * initSpeed_z - gravConst * (float)Math.Pow(totalTime, 2) / 2, totalTime * initSpeed_y);
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

    public void Shoot()
    {
        if(gameObject.transform.position == startPos)
        {
            isMoving = true;
            this.setHasLanded(false);
        }
    }

    private void UpdateCannonPosition()
    {
        if (isMoving)
        {
            PlaceObject();

            if(gameObject.transform.position.y < 0f)
            {
                this.setHasLanded(true);
                isMoving = false;
                totalTime = 0;
            }
        }
    }

    private void setHasLanded(bool value) {
        CannonState state = this.stateHandler.getCannonState();
        state.hasLanded = value;
        this.stateHandler.setCannonState(state);
    }

    public void Reload()
    {
        if(!isMoving && (gameObject.transform.position.x != startPos.x))
        {
            reLoading = true;
        }
    }

    private void UpdateReloadPosition()
    {
        if (reLoading)
        {
            FreeFall(reloadPos);

            if(gameObject.transform.position.y <= cannonHeight)
            {
                gameObject.transform.position = startPos;
                reLoading = false;
                totalTime = 0;
            }
        }
    }
}
