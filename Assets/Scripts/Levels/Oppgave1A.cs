using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave1A : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 1A";
        // Variable: x-position of goal
        state.xPositionIsLocked = false;
        state.goalXPositionSolution = (float)rnd.Next(100, 400)/10;
        // Given: (2D) vertical angle, speed
        state.verticalAngle = (float)rnd.Next(30, 70);
        state.verticalAngleSolution_1 = state.verticalAngle;
        state.speed = (float)Math.Sqrt(state.gravConst * state.goalXPositionSolution / (float)Math.Sin(2*state.verticalAngle * (float)Math.PI/180));
        state.speedSolution = state.speed;
        state.taskImagePath = "Images/hello-world";

        return state;
    }
}
