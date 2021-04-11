using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave1C : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 1C";
        // Variable: vertical angle
        state.verticalAngleIsLocked = false;
        state.verticalAngleSolution_1 = (float)rnd.Next(30, 42);
        // ^ max 42 deg is important to avoid rounding errors
        state.verticalAngleSolution_2 = 90 - state.verticalAngleSolution_1; 
        // Given: (2D) speed, x-position of goal
        state.speed = (float)rnd.Next(180, 280)/10;
        state.speedSolution = state.speed;
        state.goalXPosition = (float)Math.Pow(state.speed, 2) * (float)Math.Sin(2*state.verticalAngleSolution_1 * (float)Math.PI/180) / state.gravConst;
        state.goalXPositionSolution = state.goalXPosition;
        state.taskImagePath = "Images/Oppgave1C";

        return state;
    }
}
