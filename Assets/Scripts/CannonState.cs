using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonState
{
    public float height = 0.0f;
    public float horizontalAngle = 0.0f;
    public float verticalAngle = 45.0f;
    public float goalXPosition = 50.0f;
    public float goalYPosition = 0.0f;
    public float speed = 20.0f;
    public float gravConst = 9.81f;
    public string levelName = "";
    public string taskImagePath = "";
    public bool isThreeD = false;

    public bool speedIsLocked = true;
    public bool horizontalAngleIsLocked = true;
    public bool verticalAngleIsLocked = true;
    public bool heightIsLocked = true;
    public bool xPositionIsLocked = true;
    public bool yPositionIsLocked = true;
    public bool hasLanded = false;

    public float heightSolution = 0.0f;
    public float horizontalAngleSolution = 0.0f;
    public float verticalAngleSolution_1 = 45.0f;
    public float verticalAngleSolution_2 = 45.0f;
    public float goalXPositionSolution = 50.0f;
    public float goalYPositionSolution = 0.0f;
    public float speedSolution = 20.0f;

    public bool isCorrect(){
        return this.heightSolution == this.height &&
               this.horizontalAngleSolution == this.horizontalAngle &&
               (this.verticalAngleSolution_1 == this.verticalAngle ||
               this.verticalAngleSolution_2 == this.verticalAngle) &&
               (this.goalXPositionSolution <= this.goalXPosition + 0.3f &&
               this.goalXPositionSolution >= this.goalXPosition - 0.3f) &&
               (this.goalYPositionSolution <= this.goalYPosition + 0.3f &&
               this.goalYPositionSolution >= this.goalYPosition - 0.3f) &&
               this.speedSolution == this.speed;
    }
}
