using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave3A : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 3A";
        // Variable: x-position of goal 
        state.xPositionIsLocked = false;
        state.goalXPositionSolution = (float)rnd.Next(120, 400)/10;
        // Given: (2D) height, vertical angle, speed
        state.height = 3f; // Fixed value for predictable range
        state.heightSolution = state.height;
        state.verticalAngle = (float)rnd.Next(30, 70);
        state.verticalAngleSolution_1 = state.verticalAngle;
        state.speed = (float)Math.Sqrt(state.gravConst * (float)Math.Pow(state.goalXPositionSolution, 2) / (2 * state.height * (float)Math.Pow((float)Math.Sin(state.verticalAngle * (float)Math.PI/180), 2) + state.goalXPositionSolution * (float)Math.Sin(2*state.verticalAngle * (float)Math.PI/180)));
        state.speedSolution = state.speed;
        state.taskImagePath = "Images/hello-world";
        
        return state;
    }
}
