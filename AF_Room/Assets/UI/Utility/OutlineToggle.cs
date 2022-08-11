using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Toggle))]
public class OutlineToggle : MonoBehaviour
{
    Toggle toggle;
    Outline selectionOutline;

    private void OnEnable()
    {
        toggle = GetComponent<Toggle>();
        selectionOutline = GetComponentInChildren<Outline>();

        if (selectionOutline == null)
            Debug.LogError("OutlineToggle: No child components with an Outline found!");
        selectionOutline.enabled = toggle.isOn;

        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }
    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    void OnToggleValueChanged(bool value)
    {
        selectionOutline.enabled = toggle.isOn;
    }
}
