using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private bool isMoving = false;
    private float totalTime = 0.0f;
    private float speed = 1.0f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    Vector3 CalculateRelativePosition(float totalTime) 
    {
        // Til Michael: Fiks denne
        return new Vector3(totalTime * speed, 0, 0);
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
        if (isMoving) {
            PlaceObject();
        }
    }
}
