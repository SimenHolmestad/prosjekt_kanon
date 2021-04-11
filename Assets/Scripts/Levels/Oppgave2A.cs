using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave2A : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 2A";
        // Variable: x- and y-position of goal 
        state.xPositionIsLocked = false;
        state.yPositionIsLocked = false;
        float tempAngle = (float)rnd.Next(20, 40); 
        // Choice of direction:
        int sign = rnd.Next(1, 3);
        if(sign == 1){
            tempAngle = -tempAngle;
        }
        float tempDist =  (float)rnd.Next(300, 800);
        state.goalXPositionSolution = (float)Math.Ceiling(tempDist * (float)Math.Cos(tempAngle * (float)Math.PI/180)) / 10;
        if (tempAngle < 0) { 
            state.goalYPositionSolution = (float)Math.Ceiling(tempDist * (float)Math.Sin(tempAngle * (float)Math.PI/180)) / 10; 
        }
        else {
            state.goalYPositionSolution = (float)Math.Floor(tempDist * (float)Math.Sin(tempAngle * (float)Math.PI/180)) / 10;
        }
        // Given: (3D) horizontal angle, vertical angle, speed 
        state.horizontalAngle = (float)Math.Atan2(state.goalYPositionSolution, state.goalXPositionSolution) * 180/(float)Math.PI;
        state.horizontalAngleSolution = state.horizontalAngle;
        state.verticalAngle = (float)rnd.Next(30, 60);
        state.verticalAngleSolution_1 = state.verticalAngle;
        state.speed = (float)Math.Sqrt(state.gravConst * state.goalXPositionSolution / ((float)Math.Sin(2*state.verticalAngle * (float)Math.PI/180) * (float)Math.Cos(state.horizontalAngle * (float)Math.PI/180)));
        state.speedSolution = state.speed;
        state.taskImagePath = "Images/Oppgave2A";
        state.isThreeD = true;
        
        return state;
    }
}
