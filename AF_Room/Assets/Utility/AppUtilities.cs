using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class of staic utility methods.

public class AppUtilities
{

    // Given a game object, perform a depth first traversal of the hierarcy under the game object and return a flattened array of the object and all its children.
    public static List<GameObject> ListDescendants(GameObject obj)
    {
        List<GameObject> list = new List<GameObject>();
        Stack<Transform> stack = new Stack<Transform>();

        list.Add(obj.gameObject);
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            stack.Push(obj.transform.GetChild(i));
        }

        Transform t;
        while (stack.Count != 0)
        {
            t = stack.Pop();
            list.Add(t.gameObject);
            for (int i = 0; i < t.childCount; i++)
            {
                stack.Push(t.GetChild(i));
            }
        }

        return list;
    }

    public static GameObject[] GetDescendants(GameObject obj)
    {
        return ListDescendants(obj).ToArray();
    }

    public static List<GameObject> ListChildren(GameObject obj)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            list.Add(obj.transform.GetChild(i).gameObject);
        }
        return list;
    }
    public static GameObject[] GetChildren(GameObject obj)
    {
        return ListChildren(obj).ToArray();
    }
}
