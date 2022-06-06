using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller for the Animation UI Panel
//
// Each Button should have an AgentAnimationPanelSelector attached with the desired nehavior set in the inspector. Pass that instance to this class in the OnClick callbacks below.
//
//

public class AnimationPanelController : MonoBehaviour
{
    private AgentController agent;

    private void Start()
    {
        agent = AFManager.Instance.agent;
    }

    public void expressionButtonPressed(AgentAnimationPanelSelector action)
    {
        agent.MakeFace(action.FaceExpression);
    }

    public void actionButtonPressed(AgentAnimationPanelSelector action)
    {
        agent.PerformBodyAction(action.BodyAction);
    }

    public void LeanValueSliderChanged(float value)
    {
        agent.activeSkin.LeanDegree = value;
    }
}
