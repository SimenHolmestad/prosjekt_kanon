using System.Collections;
using System.Collections.Generic;

public class CannonState
{
    public float height = 0.0f;
    public float horizontalAngle = 0.0f;
    public float verticalAngle = 60.0f;
    public float speed = 15.0f;
    public string levelName = "";
    // TODO:
    // Må legge til flere variabler her etterhvert, for eksempel
    // - Oppgavenavn
    // - BildeURL
    // - Om vinkelen er låst
    // - Om starthastigheten er låst
    // - Om målet er låst i x-retning
    // - Om målet er låst i y-retning
    // - O.s.v...
    // - Egentlig burde dette objektet inneholde alt
    // - Trenger kanskje fasit-verdier her også? Disse kan jo genereres fra oppgavene???
    // - Fasitene burde gjøres slik at de er ints (helst)
}
