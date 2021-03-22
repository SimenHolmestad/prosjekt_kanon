using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oppgave1B : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.speed = (float)rnd.Next(10, 20);
        state.levelName = "Oppgave 1B";
        state.taskImagePath = "Images/SampleImage";

        state.speedIsLocked = false;
        state.horizontalAngleIsLocked = false;
        state.verticalAngleIsLocked = false;
        state.heightIsLocked = false;
        state.xPositionIsLocked = false;
        state.yPositionIsLocked = false;
        state.isThreeD = true;
        return state;
    }
}
