using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UXF.UI;
using SubjectNerd.Utilities; 

public class UIStartController : MonoBehaviour
{
    private Canvas canvas;

    [Tooltip("Name of experiment used in data output.")]
    public string experimentName = "my_experiment";

    [SubjectNerd.Utilities.Reorderable]
    public List<FormElementEntry> participantDataPoints = new List<FormElementEntry>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
