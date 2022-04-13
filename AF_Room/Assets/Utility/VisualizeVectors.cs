using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeVectors : MonoBehaviour
{
    public float rayLength = 0.3f;
    public bool x = true;
    public bool y = true;
    public bool z = true;
    public bool depthTest = false;

    public GameObject connect = null;
    
    // Update is called once per frame
    void Update()
    {
        if (z) Debug.DrawRay(transform.position, transform.forward * rayLength, Color.blue, 0.0f, depthTest);
        if (y) Debug.DrawRay(transform.position, transform.up * rayLength, Color.green, 0.0f, depthTest);
        if (x) Debug.DrawRay(transform.position, transform.right * rayLength, Color.red, 0.0f, depthTest);

        if(connect != null) Debug.DrawLine(transform.position, connect.transform.position, Color.cyan, 0.0f, depthTest);
    }
}
