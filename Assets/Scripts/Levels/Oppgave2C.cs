using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave2C : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 2C";
        // Variable: horizontal angle, vertical angle
        state.horizontalAngleIsLocked = false;
        state.verticalAngleIsLocked = false;
        state.horizontalAngleSolution = (float)rnd.Next(20, 40);
        // Choice of direction:
        int sign = rnd.Next(1, 3);
        if(sign == 1){
            state.horizontalAngleSolution = -state.horizontalAngleSolution;
        }
        state.verticalAngleSolution_1 = (float)rnd.Next(30, 42);
        // ^ max 42 deg is important to avoid rounding errors
        state.verticalAngleSolution_2 = 90 - state.verticalAngleSolution_1; 
        // Given: (3D) speed, x- and y-position of goal
        state.speed = (float)rnd.Next(180, 280)/10;
        state.speedSolution = state.speed;
        state.goalXPosition = (float)Math.Pow(state.speed, 2) * (float)Math.Sin(2*state.verticalAngleSolution_1 * (float)Math.PI/180) * (float)Math.Cos(state.horizontalAngleSolution * (float)Math.PI/180) / state.gravConst;
        state.goalXPositionSolution = state.goalXPosition;
        state.goalYPosition = (float)Math.Pow(state.speed, 2) * (float)Math.Sin(2*state.verticalAngleSolution_1 * (float)Math.PI/180) * (float)Math.Sin(state.horizontalAngleSolution * (float)Math.PI/180) / state.gravConst;
        state.goalYPositionSolution = state.goalYPosition;
        state.taskImagePath = "Images/Oppgave2C";
        state.isThreeD = true;

        return state;
    }
}
