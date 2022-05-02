// GENERATED AUTOMATICALLY FROM 'Assets/Agent/AgentControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @AgentControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @AgentControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""AgentControls"",
    ""maps"": [
        {
            ""name"": ""Skin"",
            ""id"": ""e6fe6c36-3343-4f0b-917c-78a46bef4e96"",
            ""actions"": [
                {
                    ""name"": ""SwapSkins"",
                    ""type"": ""Button"",
                    ""id"": ""1470731d-c799-4781-a9fc-441580d692e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9e9eec1d-c329-4e1a-8372-c4b183ee2a47"",
                    ""path"": ""<Keyboard>/#(`)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapSkins"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Action"",
            ""id"": ""d9764c24-4a09-4dd7-8ce3-007a09c71fac"",
            ""actions"": [
                {
                    ""name"": ""HeadNod"",
                    ""type"": ""Button"",
                    ""id"": ""36b1bb4e-a997-46e3-8f0b-f1e14bbe7947"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeadShake"",
                    ""type"": ""Button"",
                    ""id"": ""b6db6193-01d8-4efd-94fe-4980f18614ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeadTiltRight"",
                    ""type"": ""Button"",
                    ""id"": ""aa361222-9750-4a3a-92e8-036719b123f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeadTiltNeutral"",
                    ""type"": ""Button"",
                    ""id"": ""bc0f7699-1458-4aad-8684-40c3f99996f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeadTiltLeft"",
                    ""type"": ""Button"",
                    ""id"": ""3c27bc81-0152-43d4-8f51-8948716e2290"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BodyLeanForward"",
                    ""type"": ""Button"",
                    ""id"": ""50793462-d497-4216-9866-0207b54ff014"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BodyLeanNeutral"",
                    ""type"": ""Button"",
                    ""id"": ""31df238c-84f7-40af-8f56-7f1da82590ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BodyLeanBack"",
                    ""type"": ""Button"",
                    ""id"": ""6ab117a3-33de-4514-a0a3-b37f3a66083e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Blink"",
                    ""type"": ""Button"",
                    ""id"": ""c08ce428-ee7a-4724-984e-e383439ef6c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": []
        },
        {
            ""name"": ""Expression"",
            ""id"": ""d8be00ff-7b5a-47a6-b3ad-65ece1dbf425"",
            ""actions"": [
                {
                    ""name"": ""Neutral"",
                    ""type"": ""Button"",
                    ""id"": ""d4e7af21-94ee-42c8-a24a-570e041e1648"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Smile"",
                    ""type"": ""Button"",
                    ""id"": ""d689f726-5373-4f1d-9c18-ef16f00f1fd3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Frown"",
                    ""type"": ""Button"",
                    ""id"": ""990644d0-323c-41e1-a69a-acc2883bc84e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Concern"",
                    ""type"": ""Button"",
                    ""id"": ""acec615e-df7e-49d8-b6a5-2809b8b43fca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Disgust"",
                    ""type"": ""Button"",
                    ""id"": ""9ccc2825-7a5b-43c0-b9e7-8761aa1c3ff0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anger"",
                    ""type"": ""Button"",
                    ""id"": ""90c5baf0-04b0-4753-a28e-39fb2593bf5a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Laugh"",
                    ""type"": ""Button"",
                    ""id"": ""36a29725-816e-4784-bead-c5a67b463286"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": []
}");
        // Skin
        m_Skin = asset.FindActionMap("Skin", throwIfNotFound: true);
        m_Skin_SwapSkins = m_Skin.FindAction("SwapSkins", throwIfNotFound: true);
        // Action
        m_Action = asset.FindActionMap("Action", throwIfNotFound: true);
        m_Action_HeadNod = m_Action.FindAction("HeadNod", throwIfNotFound: true);
        m_Action_HeadShake = m_Action.FindAction("HeadShake", throwIfNotFound: true);
        m_Action_HeadTiltRight = m_Action.FindAction("HeadTiltRight", throwIfNotFound: true);
        m_Action_HeadTiltNeutral = m_Action.FindAction("HeadTiltNeutral", throwIfNotFound: true);
        m_Action_HeadTiltLeft = m_Action.FindAction("HeadTiltLeft", throwIfNotFound: true);
        m_Action_BodyLeanForward = m_Action.FindAction("BodyLeanForward", throwIfNotFound: true);
        m_Action_BodyLeanNeutral = m_Action.FindAction("BodyLeanNeutral", throwIfNotFound: true);
        m_Action_BodyLeanBack = m_Action.FindAction("BodyLeanBack", throwIfNotFound: true);
        m_Action_Blink = m_Action.FindAction("Blink", throwIfNotFound: true);
        // Expression
        m_Expression = asset.FindActionMap("Expression", throwIfNotFound: true);
        m_Expression_Neutral = m_Expression.FindAction("Neutral", throwIfNotFound: true);
        m_Expression_Smile = m_Expression.FindAction("Smile", throwIfNotFound: true);
        m_Expression_Frown = m_Expression.FindAction("Frown", throwIfNotFound: true);
        m_Expression_Concern = m_Expression.FindAction("Concern", throwIfNotFound: true);
        m_Expression_Disgust = m_Expression.FindAction("Disgust", throwIfNotFound: true);
        m_Expression_Anger = m_Expression.FindAction("Anger", throwIfNotFound: true);
        m_Expression_Laugh = m_Expression.FindAction("Laugh", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Skin
    private readonly InputActionMap m_Skin;
    private ISkinActions m_SkinActionsCallbackInterface;
    private readonly InputAction m_Skin_SwapSkins;
    public struct SkinActions
    {
        private @AgentControls m_Wrapper;
        public SkinActions(@AgentControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SwapSkins => m_Wrapper.m_Skin_SwapSkins;
        public InputActionMap Get() { return m_Wrapper.m_Skin; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SkinActions set) { return set.Get(); }
        public void SetCallbacks(ISkinActions instance)
        {
            if (m_Wrapper.m_SkinActionsCallbackInterface != null)
            {
                @SwapSkins.started -= m_Wrapper.m_SkinActionsCallbackInterface.OnSwapSkins;
                @SwapSkins.performed -= m_Wrapper.m_SkinActionsCallbackInterface.OnSwapSkins;
                @SwapSkins.canceled -= m_Wrapper.m_SkinActionsCallbackInterface.OnSwapSkins;
            }
            m_Wrapper.m_SkinActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SwapSkins.started += instance.OnSwapSkins;
                @SwapSkins.performed += instance.OnSwapSkins;
                @SwapSkins.canceled += instance.OnSwapSkins;
            }
        }
    }
    public SkinActions @Skin => new SkinActions(this);

    // Action
    private readonly InputActionMap m_Action;
    private IActionActions m_ActionActionsCallbackInterface;
    private readonly InputAction m_Action_HeadNod;
    private readonly InputAction m_Action_HeadShake;
    private readonly InputAction m_Action_HeadTiltRight;
    private readonly InputAction m_Action_HeadTiltNeutral;
    private readonly InputAction m_Action_HeadTiltLeft;
    private readonly InputAction m_Action_BodyLeanForward;
    private readonly InputAction m_Action_BodyLeanNeutral;
    private readonly InputAction m_Action_BodyLeanBack;
    private readonly InputAction m_Action_Blink;
    public struct ActionActions
    {
        private @AgentControls m_Wrapper;
        public ActionActions(@AgentControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @HeadNod => m_Wrapper.m_Action_HeadNod;
        public InputAction @HeadShake => m_Wrapper.m_Action_HeadShake;
        public InputAction @HeadTiltRight => m_Wrapper.m_Action_HeadTiltRight;
        public InputAction @HeadTiltNeutral => m_Wrapper.m_Action_HeadTiltNeutral;
        public InputAction @HeadTiltLeft => m_Wrapper.m_Action_HeadTiltLeft;
        public InputAction @BodyLeanForward => m_Wrapper.m_Action_BodyLeanForward;
        public InputAction @BodyLeanNeutral => m_Wrapper.m_Action_BodyLeanNeutral;
        public InputAction @BodyLeanBack => m_Wrapper.m_Action_BodyLeanBack;
        public InputAction @Blink => m_Wrapper.m_Action_Blink;
        public InputActionMap Get() { return m_Wrapper.m_Action; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionActions set) { return set.Get(); }
        public void SetCallbacks(IActionActions instance)
        {
            if (m_Wrapper.m_ActionActionsCallbackInterface != null)
            {
                @HeadNod.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadNod;
                @HeadNod.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadNod;
                @HeadNod.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadNod;
                @HeadShake.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadShake;
                @HeadShake.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadShake;
                @HeadShake.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadShake;
                @HeadTiltRight.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadTiltRight;
                @HeadTiltRight.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadTiltRight;
                @HeadTiltRight.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadTiltRight;
                @HeadTiltNeutral.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadTiltNeutral;
                @HeadTiltNeutral.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadTiltNeutral;
                @HeadTiltNeutral.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadTiltNeutral;
                @HeadTiltLeft.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadTiltLeft;
                @HeadTiltLeft.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadTiltLeft;
                @HeadTiltLeft.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnHeadTiltLeft;
                @BodyLeanForward.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnBodyLeanForward;
                @BodyLeanForward.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnBodyLeanForward;
                @BodyLeanForward.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnBodyLeanForward;
                @BodyLeanNeutral.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnBodyLeanNeutral;
                @BodyLeanNeutral.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnBodyLeanNeutral;
                @BodyLeanNeutral.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnBodyLeanNeutral;
                @BodyLeanBack.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnBodyLeanBack;
                @BodyLeanBack.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnBodyLeanBack;
                @BodyLeanBack.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnBodyLeanBack;
                @Blink.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnBlink;
                @Blink.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnBlink;
                @Blink.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnBlink;
            }
            m_Wrapper.m_ActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HeadNod.started += instance.OnHeadNod;
                @HeadNod.performed += instance.OnHeadNod;
                @HeadNod.canceled += instance.OnHeadNod;
                @HeadShake.started += instance.OnHeadShake;
                @HeadShake.performed += instance.OnHeadShake;
                @HeadShake.canceled += instance.OnHeadShake;
                @HeadTiltRight.started += instance.OnHeadTiltRight;
                @HeadTiltRight.performed += instance.OnHeadTiltRight;
                @HeadTiltRight.canceled += instance.OnHeadTiltRight;
                @HeadTiltNeutral.started += instance.OnHeadTiltNeutral;
                @HeadTiltNeutral.performed += instance.OnHeadTiltNeutral;
                @HeadTiltNeutral.canceled += instance.OnHeadTiltNeutral;
                @HeadTiltLeft.started += instance.OnHeadTiltLeft;
                @HeadTiltLeft.performed += instance.OnHeadTiltLeft;
                @HeadTiltLeft.canceled += instance.OnHeadTiltLeft;
                @BodyLeanForward.started += instance.OnBodyLeanForward;
                @BodyLeanForward.performed += instance.OnBodyLeanForward;
                @BodyLeanForward.canceled += instance.OnBodyLeanForward;
                @BodyLeanNeutral.started += instance.OnBodyLeanNeutral;
                @BodyLeanNeutral.performed += instance.OnBodyLeanNeutral;
                @BodyLeanNeutral.canceled += instance.OnBodyLeanNeutral;
                @BodyLeanBack.started += instance.OnBodyLeanBack;
                @BodyLeanBack.performed += instance.OnBodyLeanBack;
                @BodyLeanBack.canceled += instance.OnBodyLeanBack;
                @Blink.started += instance.OnBlink;
                @Blink.performed += instance.OnBlink;
                @Blink.canceled += instance.OnBlink;
            }
        }
    }
    public ActionActions @Action => new ActionActions(this);

    // Expression
    private readonly InputActionMap m_Expression;
    private IExpressionActions m_ExpressionActionsCallbackInterface;
    private readonly InputAction m_Expression_Neutral;
    private readonly InputAction m_Expression_Smile;
    private readonly InputAction m_Expression_Frown;
    private readonly InputAction m_Expression_Concern;
    private readonly InputAction m_Expression_Disgust;
    private readonly InputAction m_Expression_Anger;
    private readonly InputAction m_Expression_Laugh;
    public struct ExpressionActions
    {
        private @AgentControls m_Wrapper;
        public ExpressionActions(@AgentControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Neutral => m_Wrapper.m_Expression_Neutral;
        public InputAction @Smile => m_Wrapper.m_Expression_Smile;
        public InputAction @Frown => m_Wrapper.m_Expression_Frown;
        public InputAction @Concern => m_Wrapper.m_Expression_Concern;
        public InputAction @Disgust => m_Wrapper.m_Expression_Disgust;
        public InputAction @Anger => m_Wrapper.m_Expression_Anger;
        public InputAction @Laugh => m_Wrapper.m_Expression_Laugh;
        public InputActionMap Get() { return m_Wrapper.m_Expression; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ExpressionActions set) { return set.Get(); }
        public void SetCallbacks(IExpressionActions instance)
        {
            if (m_Wrapper.m_ExpressionActionsCallbackInterface != null)
            {
                @Neutral.started -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnNeutral;
                @Neutral.performed -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnNeutral;
                @Neutral.canceled -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnNeutral;
                @Smile.started -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnSmile;
                @Smile.performed -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnSmile;
                @Smile.canceled -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnSmile;
                @Frown.started -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnFrown;
                @Frown.performed -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnFrown;
                @Frown.canceled -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnFrown;
                @Concern.started -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnConcern;
                @Concern.performed -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnConcern;
                @Concern.canceled -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnConcern;
                @Disgust.started -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnDisgust;
                @Disgust.performed -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnDisgust;
                @Disgust.canceled -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnDisgust;
                @Anger.started -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnAnger;
                @Anger.performed -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnAnger;
                @Anger.canceled -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnAnger;
                @Laugh.started -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnLaugh;
                @Laugh.performed -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnLaugh;
                @Laugh.canceled -= m_Wrapper.m_ExpressionActionsCallbackInterface.OnLaugh;
            }
            m_Wrapper.m_ExpressionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Neutral.started += instance.OnNeutral;
                @Neutral.performed += instance.OnNeutral;
                @Neutral.canceled += instance.OnNeutral;
                @Smile.started += instance.OnSmile;
                @Smile.performed += instance.OnSmile;
                @Smile.canceled += instance.OnSmile;
                @Frown.started += instance.OnFrown;
                @Frown.performed += instance.OnFrown;
                @Frown.canceled += instance.OnFrown;
                @Concern.started += instance.OnConcern;
                @Concern.performed += instance.OnConcern;
                @Concern.canceled += instance.OnConcern;
                @Disgust.started += instance.OnDisgust;
                @Disgust.performed += instance.OnDisgust;
                @Disgust.canceled += instance.OnDisgust;
                @Anger.started += instance.OnAnger;
                @Anger.performed += instance.OnAnger;
                @Anger.canceled += instance.OnAnger;
                @Laugh.started += instance.OnLaugh;
                @Laugh.performed += instance.OnLaugh;
                @Laugh.canceled += instance.OnLaugh;
            }
        }
    }
    public ExpressionActions @Expression => new ExpressionActions(this);
    public interface ISkinActions
    {
        void OnSwapSkins(InputAction.CallbackContext context);
    }
    public interface IActionActions
    {
        void OnHeadNod(InputAction.CallbackContext context);
        void OnHeadShake(InputAction.CallbackContext context);
        void OnHeadTiltRight(InputAction.CallbackContext context);
        void OnHeadTiltNeutral(InputAction.CallbackContext context);
        void OnHeadTiltLeft(InputAction.CallbackContext context);
        void OnBodyLeanForward(InputAction.CallbackContext context);
        void OnBodyLeanNeutral(InputAction.CallbackContext context);
        void OnBodyLeanBack(InputAction.CallbackContext context);
        void OnBlink(InputAction.CallbackContext context);
    }
    public interface IExpressionActions
    {
        void OnNeutral(InputAction.CallbackContext context);
        void OnSmile(InputAction.CallbackContext context);
        void OnFrown(InputAction.CallbackContext context);
        void OnConcern(InputAction.CallbackContext context);
        void OnDisgust(InputAction.CallbackContext context);
        void OnAnger(InputAction.CallbackContext context);
        void OnLaugh(InputAction.CallbackContext context);
    }
}
