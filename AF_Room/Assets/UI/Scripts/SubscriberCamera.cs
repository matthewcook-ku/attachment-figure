using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscriberCamera : MonoBehaviour
{
    HashSet<MonoBehaviour> subscribers;
    public Camera TargetCamera;

    private void Awake()
    {
        subscribers = new HashSet<MonoBehaviour>();
        
        // turn the camera off until we have subscribers.
        // this allows us to keep the camera ON in the editor
        TargetCamera.enabled = false;

        //Debug.Log(gameObject.name + " awake!!!!!!!!!!!!!");
    }

    public void Subscribe(MonoBehaviour client)
    {
        bool present = subscribers.Add(client);
        if (!present) Debug.Log(gameObject.name + ": " + client + " is already subscribed to this camera.");
        else Debug.Log(gameObject.name + ": Client Added, " + subscribers.Count + " total clients.");

        if (subscribers.Count != 0)
        {
            Debug.Log(gameObject.name + ": switching camera on.");
            TargetCamera.enabled = true;
        }
    }
    public void Unsubscribe(MonoBehaviour client)
    {
        bool present = subscribers.Remove(client);
        if (!present) Debug.Log(gameObject.name + ": " + client + " was not subscribed to this camera.");
        else Debug.Log(gameObject.name + ": Client Removed, " + subscribers.Count + " total clients.");

        if (subscribers.Count == 0)
        {
            Debug.Log(gameObject.name + ": switching camera off.");
            TargetCamera.enabled = false;
        }
    }
}