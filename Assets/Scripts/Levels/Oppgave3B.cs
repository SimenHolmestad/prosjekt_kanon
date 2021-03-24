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
        state.speedSolution = (float)rnd.Next(100, 200)/10;
        // Given: (2D) height, vertical angle, x-position of goal
        state.height = 3f; // Fixed value for predictable range
        state.verticalAngle = (float)rnd.Next(30, 70);
        state.goalXPosition = state.speedSolution * (float)Math.Sin(state.verticalAngle * (float)Math.PI/180) * (state.speedSolution * (float)Math.Cos(state.verticalAngle * (float)Math.PI/180) + (float)Math.Sqrt((float)Math.Pow(state.speedSolution * (float)Math.Cos(state.verticalAngle * (float)Math.PI/180), 2) + 2*state.gravConst*state.height)) / state.gravConst;
        state.taskImagePath = "Images/SampleImage";

        return state;
    }
}
