using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave3B : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 3B";
        // Variable: speed
        state.speedIsLocked = false;
        state.speedSolution = (float)rnd.Next(180, 280)/10;
        // Given: (2D) height, vertical angle, x-position of goal
        state.height = 3.0f; // Fixed value for predictable range
        state.heightSolution = state.height;
        state.verticalAngle = (float)rnd.Next(30, 60);
        state.verticalAngleSolution_1 = state.verticalAngle;
        state.goalXPosition = state.speedSolution * (float)Math.Sin(state.verticalAngle * (float)Math.PI/180) * (state.speedSolution * (float)Math.Cos(state.verticalAngle * (float)Math.PI/180) + (float)Math.Sqrt((float)Math.Pow(state.speedSolution * (float)Math.Cos(state.verticalAngle * (float)Math.PI/180), 2) + 2*state.gravConst*state.height)) / state.gravConst;
        state.goalXPositionSolution = state.goalXPosition;
        state.taskImagePath = "Images/Oppgave3B";

        return state;
    }
}
