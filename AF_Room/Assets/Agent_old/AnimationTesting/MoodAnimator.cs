using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MoodAnimator : MonoBehaviour
{
    public MultiAimConstraint HeadAim;
    public MultiAimConstraint ArmAim;
    public float targetWeight;
    public float currentWeight;
    public bool constraintsActive = true;
    public bool transitioning = false;
    public float transitionSpeed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.InputSystem.Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if(constraintsActive)
            {
                targetWeight = 0.0f;
                Debug.Log("Constraints Off");
            } 
            else
            {
                targetWeight = 1.0f;
                Debug.Log("Constraints On");
            }
            currentWeight = HeadAim.weight;
            constraintsActive = !constraintsActive;
            transitioning = true;
        }

        if(transitioning)
        {
            if(currentWeight != targetWeight)
            {
                currentWeight = Mathf.MoveTowards(currentWeight, targetWeight, transitionSpeed * Time.deltaTime);
                HeadAim.weight = currentWeight;
                ArmAim.weight = currentWeight;
            }
            else
            {
                transitioning = false;
            }
        }

        if(UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            transitioning = false;
        }
    }
}
