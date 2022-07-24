using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Place an always visible dot at the root position of the target object's transform so we can see it.
//
// This works moving this GameObject to be the child of the target. This results in smoother tracking than
// just using the Update function to move the object.


[RequireComponent(typeof(LineRenderer))]
public class PositionMarker : MonoBehaviour
{
    LineRenderer lineRenderer;

    public Material AlwaysOnTopMaterial;
    public Color color;
    public float size;

    [SerializeField]
    private GameObject _targetObject; // defaults to self if not set
    public GameObject TargetObject
	{
        get { return _targetObject; }
        set
		{
            if (value != null)
                transform.SetParent(value.transform, false);
            else
                transform.SetParent(null);
        }
	}

	private void Awake()
	{
        lineRenderer = GetComponent<LineRenderer>();
    }

	// Start is called before the first frame update
	void Start()
    {
        // set up line renderer
        lineRenderer.generateLightingData = false;
        lineRenderer.useWorldSpace = false;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = false;
        lineRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        lineRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
        lineRenderer.allowOcclusionWhenDynamic = false;

        AlwaysOnTopMaterial.color = color;
        lineRenderer.sharedMaterial = AlwaysOnTopMaterial;

        // line has 2 points at zero, the end caps will make it a dot circle
        Vector3[] points = { Vector3.zero, Vector3.zero };
        lineRenderer.numCornerVertices = 5;
        lineRenderer.numCapVertices = 5;
        lineRenderer.widthMultiplier = size;
        lineRenderer.SetPositions(points);

        // update position from inspector value
        TargetObject = _targetObject;
    }

	private void OnEnable()
	{
        //Debug.Log(gameObject.name + " : PositionMarker - ENABLE");
        lineRenderer.enabled = true;
	}
	private void OnDisable()
	{
        //Debug.Log(gameObject.name + " : PositionMarker - DISABLE");
        lineRenderer.enabled = false;
	}

    public void setColor(Color newColor)
	{
        color = newColor;
        AlwaysOnTopMaterial.color = color;
    }
}
