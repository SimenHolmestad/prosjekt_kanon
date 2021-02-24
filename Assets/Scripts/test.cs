using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class test : MonoBehaviour
{
    private bool isMoving = false;
    private float totalTime = 0.0f;
    private float initSpeed = 15f; // Initial speed in m/s
    private float initAngleDeg = 90f; // Initial angle in degrees
    private float gravConst = 9.81f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    Vector3 CalculateRelativePosition(float totalTime) 
    {
        // How do we only calculate these once? Maybe calculate before passing to class? 
        float initAngleRad = initAngleDeg * (float)Math.PI / 180f;
        float initSpeed_x = initSpeed * (float)Math.Cos(initAngleRad);
        float initSpeed_y = initSpeed * (float)Math.Sin(initAngleRad);
        return new Vector3(totalTime * initSpeed_x, totalTime * initSpeed_y - gravConst * (float)Math.Pow(totalTime, 2) / 2, 0);
    }

    // Set the new position of the object based on the result of CalculateRelativePosition
    private void PlaceObject() 
    {
        totalTime += Time.deltaTime;
        Vector3 movement = CalculateRelativePosition(totalTime);
        gameObject.transform.position = startPos + movement;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            isMoving = !isMoving;
        }
        if (isMoving) 
        {
            PlaceObject();
            
            if(gameObject.transform.position.y <= 0f)
            {
                isMoving = false;
            }
        }
        
    }
}
