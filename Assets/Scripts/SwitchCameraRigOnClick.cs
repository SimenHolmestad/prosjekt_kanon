using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System; //

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

    public GameObject shootButton; 
    private float dist = 2.0f; 
    private float offsetAngle = 55.0f;

     IEnumerator returnToStartPosition()
     {
        yield return new WaitForSeconds(2.0f);

        // Code to execute after the delay
        this.cameraRig.transform.SetParent(this.originalParent.transform, false);
        this.shootButton.SetActive(false); //
        this.hasChanged = false;
     }

    public void applyChange(CannonState state){
        // Checks if the user is at bullet camera and the bullet has landed
        if (this.hasChanged && this.hasLanded == false && state.hasLanded == true) {
            StartCoroutine(returnToStartPosition());
        }
        this.hasLanded = state.hasLanded;
        this.shootButton.transform.position = new Vector3(dist * (float)Math.Sin(state.verticalAngle * Math.PI/180) * (float)Math.Cos((state.horizontalAngle + offsetAngle)* Math.PI/180), state.height + dist * (float)Math.Cos(state.verticalAngle * Math.PI/180), dist * (float)Math.Sin(state.verticalAngle * Math.PI/180) * (float)Math.Sin((state.horizontalAngle + offsetAngle)* Math.PI/180));
        this.shootButton.transform.localEulerAngles = new Vector3(0f,-state.verticalAngle, -state.horizontalAngle - offsetAngle/2f); 
    }

    void Start()
    {
        this.startPosition = this.originalParent.transform.position;
        this.startRotation = this.originalParent.transform.rotation;
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());

        this.shootButton.SetActive(false); 
    }

    void changeCamera()
    {
        if (!this.hasChanged && this.hasLanded == false) {
            this.cameraRig.transform.SetParent(this.newParent.transform, false);
            this.shootButton.SetActive(true); 
        } else {
            this.cameraRig.transform.SetParent(this.originalParent.transform, false);
            this.shootButton.SetActive(false); 
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
