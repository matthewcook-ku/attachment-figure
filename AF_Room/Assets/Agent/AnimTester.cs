using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimTester : MonoBehaviour
{
    public float AnimationSpeed = 1.0f;
    private Coroutine AnimationCoroutine = null;

    public Transform FromLocation;
    public Transform ToLocation;
    public GameObject Mover;


    private void OnEnable()
    {
        // trigger on keyboard 1
        AFManager.Instance.InputManager.InputActions.TestingActions.TestAction1.performed += TriggerAnimation;
        // reset on keybaord 2
        AFManager.Instance.InputManager.InputActions.TestingActions.TestAction2.performed += ResetAnimation;
    }
    private void OnDisable()
    {
        AFManager.Instance.InputManager.InputActions.TestingActions.TestAction1.performed -= TriggerAnimation;
        AFManager.Instance.InputManager.InputActions.TestingActions.TestAction2.performed -= ResetAnimation;
    }

    private void TriggerAnimation(InputAction.CallbackContext context)
    {
        Debug.Log("Triggering Animation");
        Animate();
    }
    private void ResetAnimation(InputAction.CallbackContext context)
    {
        Debug.Log("Reset Animation");
        Mover.transform.SetPositionAndRotation(FromLocation.position, FromLocation.rotation);
    }

    void AnimateConstraintToPosition(GameObject constraintObj, Vector3 from, Vector3 to)
    {

    }

    void Animate()
    {
        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine); // if it's already running, stop it so we only have one going.
        AnimationCoroutine = StartCoroutine(AnimateTransition(Mover, FromLocation, ToLocation));
    }

    IEnumerator AnimateTransition(GameObject obj, Transform from, Transform to)
    {
        float interpolationRatio = 0.0f;

        while (true)
        {
            // update position
            Vector3 newPosition = Vector3.Lerp(from.position, to.position, Easing.Quadratic.InOut(interpolationRatio));
            Quaternion newRotatation = Quaternion.Lerp(from.rotation, to.rotation, Easing.Quadratic.InOut(interpolationRatio));
            obj.transform.SetPositionAndRotation(newPosition, newRotatation);

            // update time
            interpolationRatio += Time.deltaTime * AnimationSpeed;
            if (interpolationRatio >= 1.0)
            {
                Debug.Log("Animation complete");
                break;

            }
            // wait till the next frame to update again
            yield return null;
        }
    }
}
