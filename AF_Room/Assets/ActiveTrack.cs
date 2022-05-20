using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActiveTrack : MonoBehaviour
{
    public int activeModel;
    public int keyIndex = 0;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            keyIndex = i + 1;
            Transform child = transform.GetChild(i);
            Button btn = child.GetComponent<Button>();
            btn.onClick.AddListener(() => OnKeyPressed(keyIndex));
        }
    }

    void Update()
    {
        
    }

    private void OnKeyPressed(int keyIndex)
    {
        Debug.Log(keyIndex);
    }
}
