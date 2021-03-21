using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oppgave1B : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.speed = (float)rnd.Next(100, 200);
        state.levelName = "Oppgave 1B";
        state.taskImagePath = "Images/SampleImage";
        return state;
    }
}
