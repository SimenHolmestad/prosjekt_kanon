using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reload : MonoBehaviour, IPointerClickHandler
{
    public KanonKule kanonKule;
    public CannonStateHandler stateHandler;
    public CannonAndCoordinateSystemManager cannonAndCoordinateSystemManager;

    private void OnMouseDown()
    {
        this.kanonKule.Reload();
        this.stateHandler.resetLevel();
        this.cannonAndCoordinateSystemManager.ReloadMotion();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.kanonKule.Reload();
        this.stateHandler.resetLevel();
        this.cannonAndCoordinateSystemManager.ReloadMotion();
    }
}
