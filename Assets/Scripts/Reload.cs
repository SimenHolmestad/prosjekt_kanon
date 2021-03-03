using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public KanonKule kanonKule;

    private void OnMouseDown()
    {
        kanonKule.Reload();
    }
}
