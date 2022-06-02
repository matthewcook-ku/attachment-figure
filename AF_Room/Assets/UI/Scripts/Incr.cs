using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Incr : MonoBehaviour
{
    public TMP_InputField content;
    int num = 0;
    // Start is called before the first frame update
    void Start()
    {
        content.text = "1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void incr()
    {
        try
        {
            num = System.Convert.ToInt32(content.text) + 1;
            content.text = System.Convert.ToString(num);
        }
        catch (System.FormatException)
        {
            System.Console.WriteLine("Input string is not a sequence of digits.");
        }
    }
}
