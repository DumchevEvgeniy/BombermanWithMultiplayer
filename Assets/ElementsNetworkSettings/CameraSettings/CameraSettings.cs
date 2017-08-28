using System;
using System.Collections;
using UnityEngine;

public class CameraSettings : MonoBehaviour {
    private GameObject objectOfObservation;
    private Quaternion cameraRotation = Quaternion.Euler(55, 270, 0);
    private Vector3 positionOffset = new Vector3(5, 8, 0);
    private Vector3 oldPosition;

    private void Start() {
        oldPosition = positionOffset;
        transform.SetPositionAndRotation(positionOffset, cameraRotation);
    }

    private void FixedUpdate() {
        if(objectOfObservation == null) {
            var newObjectOfObservation = gameObject.scene.FindPlayer();
            if(newObjectOfObservation != null)
                SetObjectOfObservation(newObjectOfObservation);
        }
        if(objectOfObservation != null) {
            var newPosition = objectOfObservation.transform.position + positionOffset;
            transform.Translate((newPosition - oldPosition), Space.World);
            oldPosition = transform.position;
        }
    }

    public void SetObjectOfObservation(GameObject objectOfObservation) {
        this.objectOfObservation = objectOfObservation;
    }
    public void NearToPlayer(Single time) {
        if(objectOfObservation != null)
            StartCoroutine(Near(time));
    }
    private IEnumerator Near(Single time) {
        var oldPositionOddset = positionOffset; 
        positionOffset = new Vector3(1, 2, 0);
        yield return new WaitForSeconds(time);
        positionOffset = oldPositionOddset;
    }
}
