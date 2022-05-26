﻿using CrazyMinnow.SALSA;
using UnityEngine;

public class SALSA_Template_EventControllerSubscriber : MonoBehaviour
{
    public string componentEventName;

    private void OnEnable()
    {
        EventController.AnimationStarting += OnAnimationStarting;
        EventController.AnimationON += OnAnimationON;
        EventController.AnimationEnding += OnAnimationEnding;
        EventController.AnimationOFF += OnAnimationOFF;
    }
    private void OnDisable()
    {
        EventController.AnimationStarting -= OnAnimationStarting;
        EventController.AnimationON -= OnAnimationON;
        EventController.AnimationEnding -= OnAnimationEnding;
        EventController.AnimationOFF -= OnAnimationOFF;
    }

    private void OnAnimationStarting(object sender, EventController.EventControllerNotificationArgs e)
    {
        if (e.eventName == componentEventName)
        {
            // do some stuff...
            Debug.Log("EventController fired OnAnimationStarting for: " + componentEventName+ " from sender: " + e.sender.name);
        }
    }
    private void OnAnimationON(object sender, EventController.EventControllerNotificationArgs e)
    {
        if (e.eventName == componentEventName)
        {
            // do some stuff...
            Debug.Log("EventController fired OnAnimationON for: " + componentEventName + " from sender: " + e.sender.name);
        }
    }
    private void OnAnimationEnding(object sender, EventController.EventControllerNotificationArgs e)
    {
        if (e.eventName == componentEventName)
        {
            // do some stuff...
            Debug.Log("EventController fired OnAnimationEnding for: " + componentEventName + " from sender: " + e.sender.name);
        }
    }
    private void OnAnimationOFF(object sender, EventController.EventControllerNotificationArgs e)
    {
        if (e.eventName == componentEventName)
        {
            // do some stuff...
            Debug.Log("EventController fired OnAnimationOFF for: " + componentEventName + " from sender: " + e.sender.name);
        }
    }
}
