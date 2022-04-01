/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExperimenterUIController : MonoBehaviour
{
    public Button resButton01;
    public Button resButton02;
    public Button resButton03;
    public Button unresButton01;
    public Button unresButton02;
    public Button unresButton03;

    public TextField chatTextField;
    public Button chatSendButton;

    public Button smileExpButton;
    public Button neutralExpButton;
    public Button concernExpButton;
    public Button frownExpButton;
    public Button disgustExpButton;
    public Button angerExpButton;
    public Button laughExpButton;

    public Button shakeHeadButton;
    public Button nodHeadButton;

    public Button leanForwardButton;
    public Button leanNeutralButton;
    public Button leanBackButton;
    public Button crossLegsButton;


    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        resButton01 = root.Q<Button>("resButton01");
        resButton02 = root.Q<Button>("resButton02");
        resButton03 = root.Q<Button>("resButton03");
        resButton01.clicked += resButton01Pressed;
        resButton02.clicked += resButton02Pressed;
        resButton03.clicked += resButton03Pressed;

        unresButton01 = root.Q<Button>("unresButton01");
        unresButton02 = root.Q<Button>("unresButton02");
        unresButton03 = root.Q<Button>("unresButton03");
        unresButton01.clicked += unresButton01Pressed;
        unresButton02.clicked += unresButton02Pressed;
        unresButton03.clicked += unresButton03Pressed;

        chatTextField = root.Q<TextField>("chatTextField");
        chatSendButton = root.Q<Button>("chatSendButton");
        chatSendButton.clicked += chatSendButtonPressed;



        smileExpButton = root.Q<Button>("smileExpButton");
        neutralExpButton = root.Q<Button>("neutralExpButton");
        concernExpButton = root.Q<Button>("concernExpButton");
        frownExpButton = root.Q<Button>("frownExpButton");
        disgustExpButton = root.Q<Button>("disgustExpButton");
        angerExpButton = root.Q<Button>("angerExpButton");
        laughExpButton = root.Q<Button>("laughExpButton");
        smileExpButton.clicked += smileExpButtonPressed;
        neutralExpButton.clicked += neutralExpButtonPressed;
        concernExpButton.clicked += concernExpButtonPressed;
        frownExpButton.clicked += frownExpButtonPressed;
        disgustExpButton.clicked += disgustExpButtonPressed;
        angerExpButton.clicked += angerExpButtonPressed;
        laughExpButton.clicked += laughExpButtonPressed;

        shakeHeadButton = root.Q<Button>("shakeHeadButton");
        nodHeadButton = root.Q<Button>("nodHeadButton");
        shakeHeadButton.clicked += shakeHeadButtonPressed;
        nodHeadButton.clicked += nodHeadButtonPressed;

        leanForwardButton = root.Q<Button>("leanForwardButton");
        leanNeutralButton = root.Q<Button>("leanNeutralButton");
        leanBackButton = root.Q<Button>("leanBackButton");
        crossLegsButton = root.Q<Button>("crossLegsButton");
        leanForwardButton.clicked += leanForwardButtonPressed;
        leanNeutralButton.clicked += leanNeutralButtonPressed;
        leanBackButton.clicked += leanBackButtonPressed;
        crossLegsButton.clicked += crossLegsButtonPressed;
    }


    void resButton01Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void resButton02Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void resButton03Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void unresButton01Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void unresButton02Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void unresButton03Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void chatSendButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void smileExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void neutralExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void concernExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void frownExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void disgustExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void angerExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void laughExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void shakeHeadButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void nodHeadButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void leanForwardButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void leanNeutralButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void leanBackButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    void crossLegsButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
}
*/