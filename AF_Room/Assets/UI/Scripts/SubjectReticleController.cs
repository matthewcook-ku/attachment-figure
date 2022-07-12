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
			}
            else
			{
                Dot.color = DotDefaultColor;
                Circle.color = DotDefaultColor;
                if (UseCircleForHighlight) Circle.enabled = false;
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

	private void OnEnable()
	{
        ProxemicsTracker.OnTrackerRaycast += UpdateUI;
        
        // always start not highlighted
        highlighted = false;
	}

	private void OnDisable()
	{
        ProxemicsTracker.OnTrackerRaycast -= UpdateUI;
    }

    void UpdateUI(bool raycasthit)
	{
        highlighted = raycasthit;
	}
}
