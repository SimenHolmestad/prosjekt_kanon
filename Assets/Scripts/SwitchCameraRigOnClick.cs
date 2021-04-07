using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchCameraRigOnClick : MonoBehaviour, IPointerClickHandler, CannonStateObserver
{
    public GameObject cameraRig;
    public GameObject originalParent;
    public GameObject newParent;
    public CannonStateHandler stateHandler;
    public bool hasChanged = false;
    public bool hasLanded = false;
    public Vector3 startPosition;
    public Quaternion startRotation;

     IEnumerator returnToStartPosition()
     {
        yield return new WaitForSeconds(2.0f);

        // Code to execute after the delay
        this.cameraRig.transform.SetParent(this.originalParent.transform, false);
        this.hasChanged = false;
     }

    public void applyChange(CannonState state){
        // Checks if the user is at bullet camera and the bullet has landed
        if (this.hasChanged && this.hasLanded == false && state.hasLanded == true) {
            StartCoroutine(returnToStartPosition());
        }
        this.hasLanded = state.hasLanded;
    }

    void Start()
    {
        this.startPosition = this.originalParent.transform.position;
        this.startRotation = this.originalParent.transform.rotation;
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
    }

    void changeCamera()
    {
        if (!this.hasChanged && this.hasLanded == false) {
            this.cameraRig.transform.SetParent(this.newParent.transform, false);
        } else {
            this.cameraRig.transform.SetParent(this.originalParent.transform, false);
        }
        this.hasChanged = !this.hasChanged;
    }

    private void OnMouseDown()
    {
        this.changeCamera();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.changeCamera();
    }
}
