using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UXF;

public class StartController : MonoBehaviour
{
    public Session session;

    // Start is called before the first frame update
    void Start()
    {
        session.BeginNextTrial();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
