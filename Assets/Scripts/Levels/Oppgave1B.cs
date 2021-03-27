using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave1B : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 1B";
        // Variable: speed
        state.speedIsLocked = false;
        state.speedSolution = (float)rnd.Next(100, 200)/10;
        // Given: (2D) vertical angle, x-position of goal
        state.verticalAngle = (float)rnd.Next(30, 70);
        state.verticalAngleSolution_1 = state.verticalAngle;
        state.goalXPosition = (float)Math.Pow(state.speedSolution, 2) * (float)Math.Sin(2*state.verticalAngle * (float)Math.PI/180) / state.gravConst;
        state.goalXPositionSolution = state.goalXPosition;
        state.taskImagePath = "Images/SampleImage";

        return state;
    }
}
