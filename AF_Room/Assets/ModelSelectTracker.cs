using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ModelSelectTracker : MonoBehaviour
{
    public int keyIndex = 0;
    private void Start()
    {
        for (int i = 0;  i < transform.childCount; i++)
        {
            int j = i; // local copy of i for the lambda function
            Transform child = transform.GetChild(j);
            Button btn = child.GetComponent<Button>();
            btn.onClick.AddListener(() => OnKeyPressed(j));
        }
    }

    void Update()
    {
        
    }

    private void OnKeyPressed(int selectedIndex)
    {
        keyIndex = selectedIndex;
        Debug.Log(selectedIndex);
    }
}
