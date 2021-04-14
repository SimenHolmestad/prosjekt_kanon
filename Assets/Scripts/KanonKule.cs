using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class KanonKule : MonoBehaviour, CannonStateObserver
{
    public CannonStateHandler stateHandler;

    public AudioSource cannonSound;  
    public AudioSource flyingSound;  

    private bool isMoving = false;
    private float totalTime = 0.0f;
    private float initSpeed; // Initial speed in m/s
    private float initHorizontalAngleDeg; // Initial horizontal angle in degrees
    private float initVerticalAngleDeg; // Initial vertical angle in degrees
    private float cannonHeight; // Height of cannon in m/s
    private float gravConst = 9.81f;
    private Vector3 startPos;

    private bool reLoading = false;
    private float reloadOffset = 10; // Reloading starting height (y-axis)
    private Vector3 reloadPos;

    public void applyChange(CannonState state){
        if (!isMoving && !state.hasLanded) // Michael: added !hasLanded condition
        {
            this.initSpeed = state.speed;
            this.initVerticalAngleDeg = state.verticalAngle;
            this.initHorizontalAngleDeg = state.horizontalAngle;
            this.cannonHeight = state.height;
            gameObject.transform.position = new Vector3(0f, cannonHeight, 0f);
            startPos = gameObject.transform.position;
            gameObject.transform.eulerAngles = new Vector3(initVerticalAngleDeg - 90f, 90f - initHorizontalAngleDeg, 0f);
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

    // Calculate Orientation of Cannon-Ball
    Vector3 CalculateOrientation(float totalTime)
    {
        // Only need to cast to float in the end (can clean up elsewhere)
        // Maybe move these out? Repeated in CalculateRelativePosition
        float initVerticalAngleRad = initVerticalAngleDeg * (float)Math.PI / 180f;
        float initHorizontalAngleRad = initHorizontalAngleDeg * (float)Math.PI / 180f;
        float initSpeed_x = initSpeed * (float)Math.Sin(initVerticalAngleRad) * (float)Math.Cos(initHorizontalAngleRad);
        float initSpeed_y = initSpeed * (float)Math.Sin(initVerticalAngleRad) * (float)Math.Sin(initHorizontalAngleRad);
        float initSpeed_z = initSpeed * (float)Math.Cos(initVerticalAngleRad);
        float radial_vel = (float)Math.Sqrt(Math.Pow(initSpeed_x, 2) + Math.Pow(initSpeed_y, 2));
        return new Vector3((float)Math.Atan2(initSpeed_x, initSpeed_z - gravConst * totalTime) * 180/(float)Math.PI - 90f, 90f - initHorizontalAngleDeg, 0f);
    }

    // Set the new position of the object based on the result of CalculateRelativePosition
    private void PlaceObject()
    {
        totalTime += Time.deltaTime;
        Vector3 movement = CalculateRelativePosition(totalTime);
        gameObject.transform.position = startPos + movement;
        gameObject.transform.eulerAngles = CalculateOrientation(totalTime);
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
            cannonSound.Play();
            flyingSound.Play();
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
                flyingSound.Stop();
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
            CannonState state = stateHandler.getCannonState(); // Michael: added getCannonState and + state.Height vector in FreeFall argument
            FreeFall(reloadPos + new Vector3(0, state.height, 0));

            if(gameObject.transform.position.y <= cannonHeight)
            {
                gameObject.transform.position = startPos;
                reLoading = false;
                totalTime = 0;
            }
        }
    }
}
