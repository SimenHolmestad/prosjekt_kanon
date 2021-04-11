using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave2B : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 2B";
        // Variable: speed, horizontal angle
        state.speedIsLocked = false;
        state.horizontalAngleIsLocked = false;
        state.speedSolution = (float)rnd.Next(180, 280)/10;
        state.horizontalAngleSolution = (float)rnd.Next(20, 40);
        // Choice of direction:
        int sign = rnd.Next(1, 3);
        if(sign == 1){
            state.horizontalAngleSolution = -state.horizontalAngleSolution;
        }
        // Given: (3D) vertical angle, x- and y-position of goal
        state.verticalAngle = (float)rnd.Next(30, 60);
        state.verticalAngleSolution_1 = state.verticalAngle;
        state.goalXPosition = (float)Math.Pow(state.speedSolution, 2) * (float)Math.Sin(2*state.verticalAngle * (float)Math.PI/180) * (float)Math.Cos(state.horizontalAngleSolution * (float)Math.PI/180) / state.gravConst;
        state.goalXPositionSolution = state.goalXPosition;
        state.goalYPosition = (float)Math.Pow(state.speedSolution, 2) * (float)Math.Sin(2*state.verticalAngle * (float)Math.PI/180) * (float)Math.Sin(state.horizontalAngleSolution * (float)Math.PI/180) / state.gravConst;
        state.goalYPositionSolution = state.goalYPosition;
        state.taskImagePath = "Images/Oppgave2B";
        state.isThreeD = true;

        return state;
    }
}
