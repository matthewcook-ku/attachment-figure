using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Try a directory
        string localFilePath = "file:///Users/matthewcook/Desktop/Data/";

        if(Directory.Exists(localFilePath))
        {
            Debug.Log("Local data directory does not exist:  " + localFilePath);
        }
        else
        {
            Debug.Log("Local data directory exists!:  " + localFilePath);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
