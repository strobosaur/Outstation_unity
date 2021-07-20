using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.5f;

    void FixedUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 camTarget = Vector3.Lerp(transform.position, target.position, smoothing);
            transform.position = new Vector3(camTarget.x, camTarget.y, transform.position.z);
            
             
        }        
    }
}
