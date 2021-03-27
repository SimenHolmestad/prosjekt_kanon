using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oppgave2C : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.levelName = "Oppgave 2C";
        // Variable: horizontal angle, vertical angle
        state.horizontalAngleIsLocked = false;
        state.verticalAngleIsLocked = false;
        state.horizontalAngleSolution = (float)rnd.Next(-45, 45);
        state.verticalAngleSolution_1 = (float)rnd.Next(30, 45);
        state.verticalAngleSolution_2 = 90 - state.verticalAngleSolution_1; 
        // NB! ^ requires theta_max > (90 - theta_min)
        // Given: (3D) speed, x- and y-position of goal
        state.speed = (float)rnd.Next(100, 200)/10;
        state.goalXPosition = (float)Math.Pow(state.speed, 2) * (float)Math.Sin(2*state.verticalAngleSolution_1 * (float)Math.PI/180) * (float)Math.Cos(state.horizontalAngleSolution * (float)Math.PI/180) / state.gravConst;
        state.goalYPosition = (float)Math.Pow(state.speed, 2) * (float)Math.Sin(2*state.verticalAngleSolution_1 * (float)Math.PI/180) * (float)Math.Sin(state.horizontalAngleSolution * (float)Math.PI/180) / state.gravConst;
        state.taskImagePath = "Images/SampleImage";
        state.isThreeD = true;

        return state;
    }
}
