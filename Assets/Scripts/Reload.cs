using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reload : MonoBehaviour, IPointerClickHandler
{
    public KanonKule kanonKule;
    public CannonStateHandler stateHandler;
    public CannonCylinder cannonCylinder;
    public VelocityVector velocityVector;
    public VelocityTriangle velocityTriangle;
    public XYVelocityVector xyVelocityVector;
    public ThetaPath thetaPath;
    public PhiPath phiPath;

    private void OnMouseDown()
    {
        this.kanonKule.Reload();
        this.stateHandler.resetLevel();

        this.cannonCylinder.ReloadCannonCylinder(); 
        this.velocityVector.ReloadVelocityVector();
        this.velocityTriangle.ReloadVelocityTriangle();
        this.xyVelocityVector.ReloadXYVelocityVector();
        this.thetaPath.ReloadThetaPath();
        this.phiPath.ReloadPhiPath();

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.kanonKule.Reload();
        this.stateHandler.resetLevel();

        this.cannonCylinder.ReloadCannonCylinder(); 
        this.velocityVector.ReloadVelocityVector();
        this.velocityTriangle.ReloadVelocityTriangle();
        this.xyVelocityVector.ReloadXYVelocityVector();
        this.thetaPath.ReloadThetaPath();
        this.phiPath.ReloadPhiPath();
    }
}
