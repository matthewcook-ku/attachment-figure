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
        }
    ],
    ""controlSchemes"": []
}");
        // Skin
        m_Skin = asset.FindActionMap("Skin", throwIfNotFound: true);
        m_Skin_SwapSkins = m_Skin.FindAction("SwapSkins", throwIfNotFound: true);
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
    public interface ISkinActions
    {
        void OnSwapSkins(InputAction.CallbackContext context);
    }
}
