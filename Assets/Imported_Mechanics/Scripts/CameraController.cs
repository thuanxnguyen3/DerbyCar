using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // specify an object to follow
    [SerializeField] GameObject objectToFollow = null;

    Vector3 cameraOffset = Vector3.zero;

    private void Start()
    {
        if(objectToFollow == null)
        {
            Debug.LogError("CameraController does not have an object to follow! " +
                "Please specify a follow object on the CameraController script.");
            this.enabled = false;
            return;
        }
        // calculate the starting offset
        cameraOffset = transform.position - objectToFollow.transform.position;
    }

    private void LateUpdate()
    {
        // move camera position to maintain the original offset
        transform.position = objectToFollow.transform.position + cameraOffset;
    }

}
