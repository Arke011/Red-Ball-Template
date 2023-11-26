using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float smoothness;
    public Transform target;

    void Update()
    {
        if (target == null) return;
        

        
        var pos = target.position;
        pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, pos, smoothness * Time.deltaTime);
    }
}
