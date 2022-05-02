using UnityEngine;

//
// Allow Enum Arguments to OnClick Button Function
//
// Attach this component to a UI Button, and set the desired enum value.
// Then in the OnClick function if the button, connect another script with a function that takes an AgentAnimationPanelSelector as an argument. Then pass this instance to the script as the argument. Now you can just read the enum out of this class.
//
// See: https://answers.unity.com/questions/1549639/enum-as-a-function-param-in-a-button-onclick.html

public class AgentAnimationPanelSelector : MonoBehaviour
{
    public AgentController.BodyAction BodyAction;
    public AgentController.FaceExpression FaceExpression;
}
