using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Based on https://answers.unity.com/questions/991411/how-to-have-a-callback-when-a-toggle-is-clicked-in.html
// Creates a toggle group with an associated change event that can be registered in the editor.

[RequireComponent(typeof(ToggleGroup))]
public class EventToggleGroup : MonoBehaviour
{
    [System.Serializable]
    public class ToggleEvent : UnityEvent<Toggle> { }

    [SerializeField]
    public ToggleEvent onActiveTogglesChanged;

    // it's not necessary to register every toggle with the toggle group
    // you just need to add them here and the script will register them for you
    [SerializeField]
    private Toggle[] _toggles;
    private ToggleGroup _toggleGroup;

    private void Awake()
    {
        _toggleGroup = GetComponent<ToggleGroup>();
    }

    void OnEnable()
    {
        foreach (Toggle toggle in _toggles)
        {
            // set all toggles to use this toggle group
            if (toggle.group != null && toggle.group != _toggleGroup)
            {
                // give a warning if adding toggles from multiple groups
                Debug.LogError($"EventToggleGroup is trying to register a Toggle that is a member of another group.");
            }
            toggle.group = _toggleGroup;

            // tell the toggles to report to us
            toggle.onValueChanged.AddListener(HandleToggleValueChanged);
        }
    }

    // Event handler will be called when one of the toggles is toggled
    void HandleToggleValueChanged(bool isOn)
    {
        // we'll get an event from the activated toggle, and the deacrtivated toggle, but only one is now true
        // wait for the event from the one that turned true, then send our own event
        // NOTE: this does not handle the case when all are off, which can occur if the toggle group allows
        if (isOn)
        {
            Toggle activeToggle = _toggleGroup.ActiveToggles().FirstOrDefault(); // will be null if none
            //Debug.Log("EventToggleGroup: Active Toggle = " + activeToggle);
            onActiveTogglesChanged?.Invoke(activeToggle);
        }
    }

    void OnDisable()
    {
        foreach (Toggle toggle in _toggles)
        {
            toggle.onValueChanged.RemoveListener(HandleToggleValueChanged);
            toggle.group = null;
        }
    }
    // return the index of the active toggle in the list of toggles, or -1 if not found.
    public int getActiveIndex()
	{
        for(int i = 0; i < _toggles.Length; i++)
		{
            if (_toggles[i].isOn) return i;
		}
        return -1;
    }

    // return the index of this toggle in the list of toggles, or -1 if not found.
    public int getIndexForToggle(Toggle item)
	{
        return System.Array.IndexOf(_toggles, item);
	}

    public void setActiveIndex(int index)
	{
        _toggles[index].isOn = true;
    }

    public void setActiveIndexWithoutNotify(int index)
	{
        _toggles[index].SetIsOnWithoutNotify(true);
	}

    // return a shadow copy of the array for searching purposes
    public Toggle[] getToggles()
	{
        return (Toggle[])_toggles.Clone();
	}
}