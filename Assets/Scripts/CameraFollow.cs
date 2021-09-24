using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform Target, normalTarget, zoomTarget;
    [SerializeField]
    private Transform camTransform;
    [SerializeField]
    private Transform Offset, normalOffset, zoomOffset;
    [SerializeField]
    private float SmoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
 
    private void Start()
    {
        Target = normalTarget;
        Offset = normalOffset;
    }
 
    private void Update() {
        // if (Input.GetKeyDown(KeyCode.Mouse1)) {
        //     Target = zoomTarget;
        //     Offset = zoomOffset;
        // }
        // if (Input.GetKeyUp(KeyCode.Mouse1)) {
        //     Target = normalTarget;
        //     Offset = normalOffset;
        // }
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = Offset.position;
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        transform.LookAt(Target);
    }
}
