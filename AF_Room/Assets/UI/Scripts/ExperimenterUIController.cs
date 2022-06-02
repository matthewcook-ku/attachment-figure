using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller for the Experimenter UI panel
//
// This is a good place to provide access to individual panels to higher up systems like the StudyController 
//
// Connections
// App - StudyController uses this to access UI elements to update.

public class ExperimenterUIController : MonoBehaviour
{
    public AnimationPanelController animationPanelController;
    public SpeechPanelController speechPanelController;
    public ExperimentPanelController experimentPanelController;

}
