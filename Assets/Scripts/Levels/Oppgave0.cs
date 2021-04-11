using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave0 : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 0";
        // Variable: Speed, vertical and horizontal angles
        state.isThreeD = true;
        state.speedIsLocked = false;
        state.speedSolution = 29.8f;
        state.verticalAngleIsLocked = false;
        state.verticalAngleSolution_1 = 36.0f;
        state.verticalAngleSolution_2 = 90.0f - state.verticalAngleSolution_1;
        state.horizontalAngleIsLocked = false;
        state.horizontalAngleSolution = 32.0f;
        // Given: Goal position
        state.goalXPosition = (float)Math.Pow(state.speedSolution, 2) / state.gravConst * (float)Math.Sin(2f* state.verticalAngleSolution_1 * Math.PI/180) * (float)Math.Cos(state.horizontalAngleSolution * Math.PI/180);
        state.goalXPositionSolution = state.goalXPosition;
        state.goalYPosition = (float)Math.Pow(state.speedSolution, 2) / state.gravConst * (float)Math.Sin(2f* state.verticalAngleSolution_1 * Math.PI/180) * (float)Math.Sin(state.horizontalAngleSolution * Math.PI/180);
        state.goalYPositionSolution = state.goalYPosition;
        state.taskImagePath = "Images/Oppgave0";

        return state;
    }
}
