using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubjectReticleController : MonoBehaviour
{
    public Image Dot;
    public Color DotDefaultColor;
    public Color DotHighlightColor;
    private bool _highlighted;
    public bool highlighted
    {
        get { return _highlighted; }
        set
		{
            if(value)
			{
                Dot.color = DotHighlightColor;
                if (UseCircleForHighlight) Circle.enabled = true;
                Circle.color = CircleHighlightColor;

                gazeTargetMarker.setColor(Color.green);
            }
            else
			{
                Dot.color = DotDefaultColor;
                Circle.color = DotDefaultColor;
                if (UseCircleForHighlight) Circle.enabled = false;

                gazeTargetMarker.setColor(Color.white);
            }
            _highlighted = value;
		}
	}

    public Image Circle;
    public Color CircleDefaultColor;
    public Color CircleHighlightColor;
    public bool UseCircleForHighlight;
    public bool CircleActive
	{
        get { return Circle.enabled; }
        set { Circle.enabled = value; }
	}

    public PositionMarker gazeTargetMarker;
    public AgentController agent;


    private void OnEnable()
	{
        ProxemicsTracker.OnTrackerRaycast += UpdateUI;
        AgentController.ActiveSkinChanged += UpdateGazeTargetMarker;

        // need to keep active in sync, since we moved the object out of hierarchy
        gazeTargetMarker.gameObject.SetActive(true);

        // always start not highlighted
        highlighted = false;
	}

	private void OnDisable()
	{
        ProxemicsTracker.OnTrackerRaycast -= UpdateUI;
        AgentController.ActiveSkinChanged -= UpdateGazeTargetMarker;

        // need to keep active in sync, since we moved the object out of hierarchy
        // at program close, this object might have already been destroyed, so check first
        if(gazeTargetMarker != null) gazeTargetMarker.gameObject.SetActive(false);
    }

    void UpdateUI(bool raycasthit)
	{
        highlighted = raycasthit;
    }

    void UpdateGazeTargetMarker()
	{
        // move the marker to the gaze target of the new skin
        gazeTargetMarker.TargetObject = agent.activeSkin.gazeTarget;
	}
}
