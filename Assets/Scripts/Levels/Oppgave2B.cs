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
        state.speedSolution = (float)rnd.Next(100, 200)/10;
        state.horizontalAngleSolution = (float)rnd.Next(-45, 45);
        // Given: (3D) vertical angle, x- and y-position of goal
        state.verticalAngle = (float)rnd.Next(30, 70);
        state.goalXPosition = (float)Math.Pow(state.speedSolution, 2) * (float)Math.Sin(2*state.verticalAngle * (float)Math.PI/180) * (float)Math.Cos(state.horizontalAngleSolution * (float)Math.PI/180) / state.gravConst;
        state.goalYPosition = (float)Math.Pow(state.speedSolution, 2) * (float)Math.Sin(2*state.verticalAngle * (float)Math.PI/180) * (float)Math.Sin(state.horizontalAngleSolution * (float)Math.PI/180) / state.gravConst;
        state.taskImagePath = "Images/SampleImage";
        state.isThreeD = true;

        return state;
    }
}
