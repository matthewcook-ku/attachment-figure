using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allow a game object's active status to be easily toggled, such as by a UI button.

public class ToggleActiveBehavior : MonoBehaviour
{
    [Tooltip("(Optional) GameObject to toggle. If none is provided, the GameObject this behavior is attached to will be use.")]
    public GameObject TargetGameObject;

    private void Start()
    {
        if(TargetGameObject == null)
        {
            TargetGameObject = gameObject;
        }
    }

    public void ToggleActive()
    {
        if(TargetGameObject.activeInHierarchy)
        {
            TargetGameObject.SetActive(false);
        }
        else
        {
            TargetGameObject.SetActive(true);
        }
    }
}
