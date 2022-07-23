using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBoxToggle : MonoBehaviour
{
    public void DisplayError()
    {
        transform.gameObject.SetActive(true);
    }

    public void DisableError()
    {
        transform.gameObject.SetActive(false);
    }

}
