using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controller for Headset Readout UI fields
//
// This UI is displayed in the headset view for debugging. It should probably not be shown to the subject under normal circomstances.
//
// Connections:
// SubjectUI - HeadsetReadoutPanel

public class HeadsetReadoutController : MonoBehaviour
{
    public InputField distanceField;
    public InputField gazeField;
}
