using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The controller facilitates tinting the color of a model by accessing a transparent overlay texture and changing it's color on the shader.  
 * 
 * Not Yet Working
 */

public class ColorTintController : MonoBehaviour
{
    public Color tintColor;

    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        tintColor = renderer.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
