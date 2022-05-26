using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBoxToggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayError()
    {
        transform.gameObject.SetActive(true);
    }

    public void DisableError()
    {
        transform.gameObject.SetActive(false);
    }

}
