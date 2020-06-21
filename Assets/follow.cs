using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offSet;

    private void FixedUpdate()
    {
        Vector3 desiredPosition  = transform.position = target.position  + offSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);

    }
}
