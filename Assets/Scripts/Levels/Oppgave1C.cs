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
        state.verticalAngleSolution_1 = (float)rnd.Next(30, 45);
        state.verticalAngleSolution_2 = 90 - state.verticalAngleSolution_1; 
        // NB! ^ requires theta_max > (90 - theta_min)
        // Given: (2D) speed, x-position of goal
        state.speed = (float)rnd.Next(100, 200)/10;
        state.goalXPosition = (float)Math.Pow(state.speed, 2) * (float)Math.Sin(2*state.verticalAngleSolution_1 * (float)Math.PI/180) / state.gravConst;
        state.taskImagePath = "Images/hello-world";

        return state;
    }
}
