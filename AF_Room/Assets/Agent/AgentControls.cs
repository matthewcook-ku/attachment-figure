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
            ""name"": ""PuppetControls"",
            ""id"": ""e6fe6c36-3343-4f0b-917c-78a46bef4e96"",
            ""actions"": [
                {
                    ""name"": ""SwapSkins"",
                    ""type"": ""Button"",
                    ""id"": ""1470731d-c799-4781-a9fc-441580d692e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Nod Head"",
                    ""type"": ""Button"",
                    ""id"": ""611b4865-9b63-4b8e-bb1d-abfbe50cf585"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shake Head"",
                    ""type"": ""Button"",
                    ""id"": ""5e0d3c1f-2eaa-4b67-9c3f-6de93da902f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lean"",
                    ""type"": ""Button"",
                    ""id"": ""4275cce8-12fb-475c-bfb7-8c45f9c1bb79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Smile"",
                    ""type"": ""Button"",
                    ""id"": ""5b86faf4-e805-432f-a243-1df8853952c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Frown"",
                    ""type"": ""Button"",
                    ""id"": ""2a69f452-64b0-4335-a758-35369113aed7"",
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
        }
    ],
    ""controlSchemes"": []
}");
        // PuppetControls
        m_PuppetControls = asset.FindActionMap("PuppetControls", throwIfNotFound: true);
        m_PuppetControls_SwapSkins = m_PuppetControls.FindAction("SwapSkins", throwIfNotFound: true);
        m_PuppetControls_NodHead = m_PuppetControls.FindAction("Nod Head", throwIfNotFound: true);
        m_PuppetControls_ShakeHead = m_PuppetControls.FindAction("Shake Head", throwIfNotFound: true);
        m_PuppetControls_Lean = m_PuppetControls.FindAction("Lean", throwIfNotFound: true);
        m_PuppetControls_Smile = m_PuppetControls.FindAction("Smile", throwIfNotFound: true);
        m_PuppetControls_Frown = m_PuppetControls.FindAction("Frown", throwIfNotFound: true);
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

    // PuppetControls
    private readonly InputActionMap m_PuppetControls;
    private IPuppetControlsActions m_PuppetControlsActionsCallbackInterface;
    private readonly InputAction m_PuppetControls_SwapSkins;
    private readonly InputAction m_PuppetControls_NodHead;
    private readonly InputAction m_PuppetControls_ShakeHead;
    private readonly InputAction m_PuppetControls_Lean;
    private readonly InputAction m_PuppetControls_Smile;
    private readonly InputAction m_PuppetControls_Frown;
    public struct PuppetControlsActions
    {
        private @AgentControls m_Wrapper;
        public PuppetControlsActions(@AgentControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SwapSkins => m_Wrapper.m_PuppetControls_SwapSkins;
        public InputAction @NodHead => m_Wrapper.m_PuppetControls_NodHead;
        public InputAction @ShakeHead => m_Wrapper.m_PuppetControls_ShakeHead;
        public InputAction @Lean => m_Wrapper.m_PuppetControls_Lean;
        public InputAction @Smile => m_Wrapper.m_PuppetControls_Smile;
        public InputAction @Frown => m_Wrapper.m_PuppetControls_Frown;
        public InputActionMap Get() { return m_Wrapper.m_PuppetControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PuppetControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPuppetControlsActions instance)
        {
            if (m_Wrapper.m_PuppetControlsActionsCallbackInterface != null)
            {
                @SwapSkins.started -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnSwapSkins;
                @SwapSkins.performed -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnSwapSkins;
                @SwapSkins.canceled -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnSwapSkins;
                @NodHead.started -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnNodHead;
                @NodHead.performed -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnNodHead;
                @NodHead.canceled -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnNodHead;
                @ShakeHead.started -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnShakeHead;
                @ShakeHead.performed -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnShakeHead;
                @ShakeHead.canceled -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnShakeHead;
                @Lean.started -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnLean;
                @Lean.performed -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnLean;
                @Lean.canceled -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnLean;
                @Smile.started -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnSmile;
                @Smile.performed -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnSmile;
                @Smile.canceled -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnSmile;
                @Frown.started -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnFrown;
                @Frown.performed -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnFrown;
                @Frown.canceled -= m_Wrapper.m_PuppetControlsActionsCallbackInterface.OnFrown;
            }
            m_Wrapper.m_PuppetControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SwapSkins.started += instance.OnSwapSkins;
                @SwapSkins.performed += instance.OnSwapSkins;
                @SwapSkins.canceled += instance.OnSwapSkins;
                @NodHead.started += instance.OnNodHead;
                @NodHead.performed += instance.OnNodHead;
                @NodHead.canceled += instance.OnNodHead;
                @ShakeHead.started += instance.OnShakeHead;
                @ShakeHead.performed += instance.OnShakeHead;
                @ShakeHead.canceled += instance.OnShakeHead;
                @Lean.started += instance.OnLean;
                @Lean.performed += instance.OnLean;
                @Lean.canceled += instance.OnLean;
                @Smile.started += instance.OnSmile;
                @Smile.performed += instance.OnSmile;
                @Smile.canceled += instance.OnSmile;
                @Frown.started += instance.OnFrown;
                @Frown.performed += instance.OnFrown;
                @Frown.canceled += instance.OnFrown;
            }
        }
    }
    public PuppetControlsActions @PuppetControls => new PuppetControlsActions(this);
    public interface IPuppetControlsActions
    {
        void OnSwapSkins(InputAction.CallbackContext context);
        void OnNodHead(InputAction.CallbackContext context);
        void OnShakeHead(InputAction.CallbackContext context);
        void OnLean(InputAction.CallbackContext context);
        void OnSmile(InputAction.CallbackContext context);
        void OnFrown(InputAction.CallbackContext context);
    }
}
