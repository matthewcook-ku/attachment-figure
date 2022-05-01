using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TestAnimations : MonoBehaviour
{
    private Animator animator;

    public TwoBoneIKConstraint RightHandConstraint;
    public TwoBoneIKConstraint LeftHandConstraint;
    public ChainIKConstraint LeanConstraint;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnNodButtonPressed()
    {
        animator.SetTrigger("nod head");
    }
    void OnShakeButtonPressed()
    {
        animator.SetTrigger("shake head");
    }
    void OnSitButtonPressed()
    {
        animator.SetTrigger("shift sit");
    }
    void OnWeightSliderChanged(float value)
    {
        RightHandConstraint.weight = value;
        LeftHandConstraint.weight = value;
        LeanConstraint.weight = value;
    }
}
