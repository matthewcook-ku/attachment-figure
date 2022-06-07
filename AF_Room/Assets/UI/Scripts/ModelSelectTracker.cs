using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ModelSelectTracker : MonoBehaviour
{
    public int keyIndex = 0;
    private Image border;
    private void Start()
    {
        for (int i = 0;  i < transform.childCount; i++)
        {
            int j = i; // local copy of i for the lambda function
            Transform child = transform.GetChild(j);
            Button btn = child.GetComponent<Button>();
            if(i == 0)
            {
                btn.Select();
            }
            btn.onClick.AddListener(() => OnKeyPressed(j));
        }
        border = transform.parent.Find("Border").GetComponent<Image>();
        // border.transform.position = transform.GetChild(0).position;
    }

    void Update()
    {
        
    }

    private void OnKeyPressed(int selectedIndex)
    {
        border.transform.position = transform.GetChild(selectedIndex).position;
        keyIndex = selectedIndex;
        Debug.Log(selectedIndex);
    }
}
