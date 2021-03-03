using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public KanonKule kanonKule;

    private void OnMouseDown()
    {
        kanonKule.Shoot();
    }
}
