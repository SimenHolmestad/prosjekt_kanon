using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oppgave1A : LevelInterface
{
    public CannonState getInitialState()
    {
        CannonState state = new CannonState();
        System.Random rnd = new System.Random();
        state.speed = (float)rnd.Next(10, 20);
        state.levelName = "Oppgave 1A";
        return state;
    }
}
