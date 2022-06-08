// GENERATED AUTOMATICALLY FROM 'Assets/InputControls/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""FPSControls"",
            ""id"": ""a84cca7a-48f3-43b0-81e0-35c574f1a74b"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""06f75c0a-8592-4163-8ce1-16e7f6cf729e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""943d7259-daf8-42b9-aee9-5d7781466b2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""0eb0f16a-6b5c-466b-8b42-e427383f9e13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c7ab3ea8-6d62-4840-a8f8-ae54d274ee24"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7ceea07b-6951-422b-88f5-07697a90659e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""397711d5-1999-432d-86ab-bcaf8ca12055"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9cffe2e3-be63-4ed0-843d-9f910ed5407c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aa5a92fc-07e8-4936-be71-5851e4f66ae4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""92001401-74dc-45bb-9bdb-bc809b1c51eb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4c7fbb5a-94ba-4f4b-b9ab-94728279f488"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""ad957a4a-4273-491f-8b8f-d94eba3b4cd7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""775ba5cb-8f9f-4577-8e6b-a88a1a1e3047"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fb7a1309-ca73-40a7-b52f-cedcdb16de89"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""49d83c85-b489-4de3-8289-929a2e01355d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9adc9773-14b5-4d10-9e81-366badacb1cd"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f780ea01-2b6e-429f-a492-270f27cc648c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e2d42d9-42a2-4680-9b51-ef5a83c88d27"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a834506-86cf-489a-964e-302ef522abb2"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7596521-09f4-4e96-86db-8b8a89b1fedd"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""AgentControls"",
            ""id"": ""0043854d-3f8d-4363-9378-347122c9e7a6"",
            ""actions"": [
                {
                    ""name"": ""SwapSkins"",
                    ""type"": ""Button"",
                    ""id"": ""57a6f702-117d-4f23-9d05-0dd382ecd1de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleHandIK"",
                    ""type"": ""Button"",
                    ""id"": ""c306be2d-00a8-4a07-8d9b-5668a40ba31e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleLean"",
                    ""type"": ""Button"",
                    ""id"": ""e1fd6a66-e59c-456f-a3c2-16cf75849c9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleHeadLookAt"",
                    ""type"": ""Button"",
                    ""id"": ""64fdc6b6-373a-478d-bcbe-0ad916ad7301"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleEyeLookAt"",
                    ""type"": ""Button"",
                    ""id"": ""1713e97e-2b15-497a-9e08-0cdaf0852b95"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RandomLook"",
                    ""type"": ""Button"",
                    ""id"": ""25d1f342-e3e4-4624-b8de-3b060b8efffc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e9c8e85f-343d-45d7-bbdc-0c8185b3e0da"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapSkins"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aef0e09f-b052-42e5-9211-9fb200225966"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleHandIK"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dee04299-f3ad-46a4-a2ee-64fc030d3e12"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleLean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d97e956-9f04-47a3-9a7d-912aecc2c63f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleHeadLookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cab1a63e-208c-4dcc-bfb6-fa85f611e6f8"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleEyeLookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2977c270-884b-4454-853a-e547c34226ff"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RandomLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TestingActions"",
            ""id"": ""1f418907-a742-4f78-8b0e-584d623ccf21"",
            ""actions"": [
                {
                    ""name"": ""TestAction1"",
                    ""type"": ""Button"",
                    ""id"": ""9c669ee9-30c8-4e35-8cbb-d38038e01c5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestAction2"",
                    ""type"": ""Button"",
                    ""id"": ""5e45974a-57cf-4e95-a506-bcd6402272c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestAction3"",
                    ""type"": ""Button"",
                    ""id"": ""668d913d-b4c0-442d-a0a0-2b6f6ea25a76"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestAction4"",
                    ""type"": ""Button"",
                    ""id"": ""af28cfbb-23f7-4b85-9819-dd0b4dee5192"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestAction5"",
                    ""type"": ""Button"",
                    ""id"": ""2d528f34-310b-4bc6-b81e-8e8e611048f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestAction6"",
                    ""type"": ""Button"",
                    ""id"": ""d844d8f3-26de-4896-854a-9c881fe50181"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestAction7"",
                    ""type"": ""Button"",
                    ""id"": ""b4d6de21-f6aa-48d9-bbbd-715792707249"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestAction8"",
                    ""type"": ""Button"",
                    ""id"": ""be3f522c-7ebd-4120-a3fd-4e37d9ea469a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestAction9"",
                    ""type"": ""Button"",
                    ""id"": ""98fc5e18-a069-45f2-b75f-5b4fa37c57c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestAction0"",
                    ""type"": ""Button"",
                    ""id"": ""f785faec-7d3b-4530-8901-77cdf480d725"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1a8d55ab-0b65-4e62-a4fd-c8273e996493"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8050c12-4b37-4905-97a9-d37a264323fa"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66d39e2b-5013-49e0-ad5a-5a7dd44524bb"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ceae8f1c-ecad-4238-96da-1769449b92ec"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f28f9da9-b580-4234-a4a0-279ed1633a21"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bae4545-8735-481c-9dbe-3821455c070c"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""155db55c-0669-49ff-bfe5-7011ba6f4a95"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce6e4749-a37e-4309-a2cb-9b5086617536"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2300407a-3371-43ea-9197-f20d687228d8"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e1ffd55-66ff-4bca-8e69-4a4ddc8685a9"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestAction0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ExperimenterControls"",
            ""id"": ""6001ffde-2977-4be9-8055-cd98d4dfb2f2"",
            ""actions"": [
                {
                    ""name"": ""ToggleFPS"",
                    ""type"": ""Button"",
                    ""id"": ""bd1b9888-8e98-4881-b6b8-ed9170019b03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9a4d9013-d8f7-4de9-b81c-9766f57222f2"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleFPS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9811008d-196c-4fb7-b81a-7ed97dd8bff9"",
                    ""path"": ""<Keyboard>/equals"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleFPS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // FPSControls
        m_FPSControls = asset.FindActionMap("FPSControls", throwIfNotFound: true);
        m_FPSControls_HorizontalMovement = m_FPSControls.FindAction("HorizontalMovement", throwIfNotFound: true);
        m_FPSControls_Jump = m_FPSControls.FindAction("Jump", throwIfNotFound: true);
        m_FPSControls_Run = m_FPSControls.FindAction("Run", throwIfNotFound: true);
        m_FPSControls_LookX = m_FPSControls.FindAction("LookX", throwIfNotFound: true);
        m_FPSControls_LookY = m_FPSControls.FindAction("LookY", throwIfNotFound: true);
        // AgentControls
        m_AgentControls = asset.FindActionMap("AgentControls", throwIfNotFound: true);
        m_AgentControls_SwapSkins = m_AgentControls.FindAction("SwapSkins", throwIfNotFound: true);
        m_AgentControls_ToggleHandIK = m_AgentControls.FindAction("ToggleHandIK", throwIfNotFound: true);
        m_AgentControls_ToggleLean = m_AgentControls.FindAction("ToggleLean", throwIfNotFound: true);
        m_AgentControls_ToggleHeadLookAt = m_AgentControls.FindAction("ToggleHeadLookAt", throwIfNotFound: true);
        m_AgentControls_ToggleEyeLookAt = m_AgentControls.FindAction("ToggleEyeLookAt", throwIfNotFound: true);
        m_AgentControls_RandomLook = m_AgentControls.FindAction("RandomLook", throwIfNotFound: true);
        // TestingActions
        m_TestingActions = asset.FindActionMap("TestingActions", throwIfNotFound: true);
        m_TestingActions_TestAction1 = m_TestingActions.FindAction("TestAction1", throwIfNotFound: true);
        m_TestingActions_TestAction2 = m_TestingActions.FindAction("TestAction2", throwIfNotFound: true);
        m_TestingActions_TestAction3 = m_TestingActions.FindAction("TestAction3", throwIfNotFound: true);
        m_TestingActions_TestAction4 = m_TestingActions.FindAction("TestAction4", throwIfNotFound: true);
        m_TestingActions_TestAction5 = m_TestingActions.FindAction("TestAction5", throwIfNotFound: true);
        m_TestingActions_TestAction6 = m_TestingActions.FindAction("TestAction6", throwIfNotFound: true);
        m_TestingActions_TestAction7 = m_TestingActions.FindAction("TestAction7", throwIfNotFound: true);
        m_TestingActions_TestAction8 = m_TestingActions.FindAction("TestAction8", throwIfNotFound: true);
        m_TestingActions_TestAction9 = m_TestingActions.FindAction("TestAction9", throwIfNotFound: true);
        m_TestingActions_TestAction0 = m_TestingActions.FindAction("TestAction0", throwIfNotFound: true);
        // ExperimenterControls
        m_ExperimenterControls = asset.FindActionMap("ExperimenterControls", throwIfNotFound: true);
        m_ExperimenterControls_ToggleFPS = m_ExperimenterControls.FindAction("ToggleFPS", throwIfNotFound: true);
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

    // FPSControls
    private readonly InputActionMap m_FPSControls;
    private IFPSControlsActions m_FPSControlsActionsCallbackInterface;
    private readonly InputAction m_FPSControls_HorizontalMovement;
    private readonly InputAction m_FPSControls_Jump;
    private readonly InputAction m_FPSControls_Run;
    private readonly InputAction m_FPSControls_LookX;
    private readonly InputAction m_FPSControls_LookY;
    public struct FPSControlsActions
    {
        private @InputControls m_Wrapper;
        public FPSControlsActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMovement => m_Wrapper.m_FPSControls_HorizontalMovement;
        public InputAction @Jump => m_Wrapper.m_FPSControls_Jump;
        public InputAction @Run => m_Wrapper.m_FPSControls_Run;
        public InputAction @LookX => m_Wrapper.m_FPSControls_LookX;
        public InputAction @LookY => m_Wrapper.m_FPSControls_LookY;
        public InputActionMap Get() { return m_Wrapper.m_FPSControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FPSControlsActions set) { return set.Get(); }
        public void SetCallbacks(IFPSControlsActions instance)
        {
            if (m_Wrapper.m_FPSControlsActionsCallbackInterface != null)
            {
                @HorizontalMovement.started -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.performed -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.canceled -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnHorizontalMovement;
                @Jump.started -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnJump;
                @Run.started -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnRun;
                @LookX.started -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnLookX;
                @LookX.performed -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnLookX;
                @LookX.canceled -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnLookX;
                @LookY.started -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnLookY;
                @LookY.performed -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnLookY;
                @LookY.canceled -= m_Wrapper.m_FPSControlsActionsCallbackInterface.OnLookY;
            }
            m_Wrapper.m_FPSControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HorizontalMovement.started += instance.OnHorizontalMovement;
                @HorizontalMovement.performed += instance.OnHorizontalMovement;
                @HorizontalMovement.canceled += instance.OnHorizontalMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @LookX.started += instance.OnLookX;
                @LookX.performed += instance.OnLookX;
                @LookX.canceled += instance.OnLookX;
                @LookY.started += instance.OnLookY;
                @LookY.performed += instance.OnLookY;
                @LookY.canceled += instance.OnLookY;
            }
        }
    }
    public FPSControlsActions @FPSControls => new FPSControlsActions(this);

    // AgentControls
    private readonly InputActionMap m_AgentControls;
    private IAgentControlsActions m_AgentControlsActionsCallbackInterface;
    private readonly InputAction m_AgentControls_SwapSkins;
    private readonly InputAction m_AgentControls_ToggleHandIK;
    private readonly InputAction m_AgentControls_ToggleLean;
    private readonly InputAction m_AgentControls_ToggleHeadLookAt;
    private readonly InputAction m_AgentControls_ToggleEyeLookAt;
    private readonly InputAction m_AgentControls_RandomLook;
    public struct AgentControlsActions
    {
        private @InputControls m_Wrapper;
        public AgentControlsActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SwapSkins => m_Wrapper.m_AgentControls_SwapSkins;
        public InputAction @ToggleHandIK => m_Wrapper.m_AgentControls_ToggleHandIK;
        public InputAction @ToggleLean => m_Wrapper.m_AgentControls_ToggleLean;
        public InputAction @ToggleHeadLookAt => m_Wrapper.m_AgentControls_ToggleHeadLookAt;
        public InputAction @ToggleEyeLookAt => m_Wrapper.m_AgentControls_ToggleEyeLookAt;
        public InputAction @RandomLook => m_Wrapper.m_AgentControls_RandomLook;
        public InputActionMap Get() { return m_Wrapper.m_AgentControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AgentControlsActions set) { return set.Get(); }
        public void SetCallbacks(IAgentControlsActions instance)
        {
            if (m_Wrapper.m_AgentControlsActionsCallbackInterface != null)
            {
                @SwapSkins.started -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnSwapSkins;
                @SwapSkins.performed -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnSwapSkins;
                @SwapSkins.canceled -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnSwapSkins;
                @ToggleHandIK.started -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleHandIK;
                @ToggleHandIK.performed -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleHandIK;
                @ToggleHandIK.canceled -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleHandIK;
                @ToggleLean.started -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleLean;
                @ToggleLean.performed -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleLean;
                @ToggleLean.canceled -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleLean;
                @ToggleHeadLookAt.started -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleHeadLookAt;
                @ToggleHeadLookAt.performed -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleHeadLookAt;
                @ToggleHeadLookAt.canceled -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleHeadLookAt;
                @ToggleEyeLookAt.started -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleEyeLookAt;
                @ToggleEyeLookAt.performed -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleEyeLookAt;
                @ToggleEyeLookAt.canceled -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnToggleEyeLookAt;
                @RandomLook.started -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnRandomLook;
                @RandomLook.performed -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnRandomLook;
                @RandomLook.canceled -= m_Wrapper.m_AgentControlsActionsCallbackInterface.OnRandomLook;
            }
            m_Wrapper.m_AgentControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SwapSkins.started += instance.OnSwapSkins;
                @SwapSkins.performed += instance.OnSwapSkins;
                @SwapSkins.canceled += instance.OnSwapSkins;
                @ToggleHandIK.started += instance.OnToggleHandIK;
                @ToggleHandIK.performed += instance.OnToggleHandIK;
                @ToggleHandIK.canceled += instance.OnToggleHandIK;
                @ToggleLean.started += instance.OnToggleLean;
                @ToggleLean.performed += instance.OnToggleLean;
                @ToggleLean.canceled += instance.OnToggleLean;
                @ToggleHeadLookAt.started += instance.OnToggleHeadLookAt;
                @ToggleHeadLookAt.performed += instance.OnToggleHeadLookAt;
                @ToggleHeadLookAt.canceled += instance.OnToggleHeadLookAt;
                @ToggleEyeLookAt.started += instance.OnToggleEyeLookAt;
                @ToggleEyeLookAt.performed += instance.OnToggleEyeLookAt;
                @ToggleEyeLookAt.canceled += instance.OnToggleEyeLookAt;
                @RandomLook.started += instance.OnRandomLook;
                @RandomLook.performed += instance.OnRandomLook;
                @RandomLook.canceled += instance.OnRandomLook;
            }
        }
    }
    public AgentControlsActions @AgentControls => new AgentControlsActions(this);

    // TestingActions
    private readonly InputActionMap m_TestingActions;
    private ITestingActionsActions m_TestingActionsActionsCallbackInterface;
    private readonly InputAction m_TestingActions_TestAction1;
    private readonly InputAction m_TestingActions_TestAction2;
    private readonly InputAction m_TestingActions_TestAction3;
    private readonly InputAction m_TestingActions_TestAction4;
    private readonly InputAction m_TestingActions_TestAction5;
    private readonly InputAction m_TestingActions_TestAction6;
    private readonly InputAction m_TestingActions_TestAction7;
    private readonly InputAction m_TestingActions_TestAction8;
    private readonly InputAction m_TestingActions_TestAction9;
    private readonly InputAction m_TestingActions_TestAction0;
    public struct TestingActionsActions
    {
        private @InputControls m_Wrapper;
        public TestingActionsActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TestAction1 => m_Wrapper.m_TestingActions_TestAction1;
        public InputAction @TestAction2 => m_Wrapper.m_TestingActions_TestAction2;
        public InputAction @TestAction3 => m_Wrapper.m_TestingActions_TestAction3;
        public InputAction @TestAction4 => m_Wrapper.m_TestingActions_TestAction4;
        public InputAction @TestAction5 => m_Wrapper.m_TestingActions_TestAction5;
        public InputAction @TestAction6 => m_Wrapper.m_TestingActions_TestAction6;
        public InputAction @TestAction7 => m_Wrapper.m_TestingActions_TestAction7;
        public InputAction @TestAction8 => m_Wrapper.m_TestingActions_TestAction8;
        public InputAction @TestAction9 => m_Wrapper.m_TestingActions_TestAction9;
        public InputAction @TestAction0 => m_Wrapper.m_TestingActions_TestAction0;
        public InputActionMap Get() { return m_Wrapper.m_TestingActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestingActionsActions set) { return set.Get(); }
        public void SetCallbacks(ITestingActionsActions instance)
        {
            if (m_Wrapper.m_TestingActionsActionsCallbackInterface != null)
            {
                @TestAction1.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction1;
                @TestAction1.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction1;
                @TestAction1.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction1;
                @TestAction2.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction2;
                @TestAction2.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction2;
                @TestAction2.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction2;
                @TestAction3.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction3;
                @TestAction3.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction3;
                @TestAction3.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction3;
                @TestAction4.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction4;
                @TestAction4.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction4;
                @TestAction4.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction4;
                @TestAction5.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction5;
                @TestAction5.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction5;
                @TestAction5.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction5;
                @TestAction6.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction6;
                @TestAction6.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction6;
                @TestAction6.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction6;
                @TestAction7.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction7;
                @TestAction7.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction7;
                @TestAction7.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction7;
                @TestAction8.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction8;
                @TestAction8.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction8;
                @TestAction8.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction8;
                @TestAction9.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction9;
                @TestAction9.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction9;
                @TestAction9.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction9;
                @TestAction0.started -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction0;
                @TestAction0.performed -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction0;
                @TestAction0.canceled -= m_Wrapper.m_TestingActionsActionsCallbackInterface.OnTestAction0;
            }
            m_Wrapper.m_TestingActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TestAction1.started += instance.OnTestAction1;
                @TestAction1.performed += instance.OnTestAction1;
                @TestAction1.canceled += instance.OnTestAction1;
                @TestAction2.started += instance.OnTestAction2;
                @TestAction2.performed += instance.OnTestAction2;
                @TestAction2.canceled += instance.OnTestAction2;
                @TestAction3.started += instance.OnTestAction3;
                @TestAction3.performed += instance.OnTestAction3;
                @TestAction3.canceled += instance.OnTestAction3;
                @TestAction4.started += instance.OnTestAction4;
                @TestAction4.performed += instance.OnTestAction4;
                @TestAction4.canceled += instance.OnTestAction4;
                @TestAction5.started += instance.OnTestAction5;
                @TestAction5.performed += instance.OnTestAction5;
                @TestAction5.canceled += instance.OnTestAction5;
                @TestAction6.started += instance.OnTestAction6;
                @TestAction6.performed += instance.OnTestAction6;
                @TestAction6.canceled += instance.OnTestAction6;
                @TestAction7.started += instance.OnTestAction7;
                @TestAction7.performed += instance.OnTestAction7;
                @TestAction7.canceled += instance.OnTestAction7;
                @TestAction8.started += instance.OnTestAction8;
                @TestAction8.performed += instance.OnTestAction8;
                @TestAction8.canceled += instance.OnTestAction8;
                @TestAction9.started += instance.OnTestAction9;
                @TestAction9.performed += instance.OnTestAction9;
                @TestAction9.canceled += instance.OnTestAction9;
                @TestAction0.started += instance.OnTestAction0;
                @TestAction0.performed += instance.OnTestAction0;
                @TestAction0.canceled += instance.OnTestAction0;
            }
        }
    }
    public TestingActionsActions @TestingActions => new TestingActionsActions(this);

    // ExperimenterControls
    private readonly InputActionMap m_ExperimenterControls;
    private IExperimenterControlsActions m_ExperimenterControlsActionsCallbackInterface;
    private readonly InputAction m_ExperimenterControls_ToggleFPS;
    public struct ExperimenterControlsActions
    {
        private @InputControls m_Wrapper;
        public ExperimenterControlsActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleFPS => m_Wrapper.m_ExperimenterControls_ToggleFPS;
        public InputActionMap Get() { return m_Wrapper.m_ExperimenterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ExperimenterControlsActions set) { return set.Get(); }
        public void SetCallbacks(IExperimenterControlsActions instance)
        {
            if (m_Wrapper.m_ExperimenterControlsActionsCallbackInterface != null)
            {
                @ToggleFPS.started -= m_Wrapper.m_ExperimenterControlsActionsCallbackInterface.OnToggleFPS;
                @ToggleFPS.performed -= m_Wrapper.m_ExperimenterControlsActionsCallbackInterface.OnToggleFPS;
                @ToggleFPS.canceled -= m_Wrapper.m_ExperimenterControlsActionsCallbackInterface.OnToggleFPS;
            }
            m_Wrapper.m_ExperimenterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToggleFPS.started += instance.OnToggleFPS;
                @ToggleFPS.performed += instance.OnToggleFPS;
                @ToggleFPS.canceled += instance.OnToggleFPS;
            }
        }
    }
    public ExperimenterControlsActions @ExperimenterControls => new ExperimenterControlsActions(this);
    public interface IFPSControlsActions
    {
        void OnHorizontalMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnLookX(InputAction.CallbackContext context);
        void OnLookY(InputAction.CallbackContext context);
    }
    public interface IAgentControlsActions
    {
        void OnSwapSkins(InputAction.CallbackContext context);
        void OnToggleHandIK(InputAction.CallbackContext context);
        void OnToggleLean(InputAction.CallbackContext context);
        void OnToggleHeadLookAt(InputAction.CallbackContext context);
        void OnToggleEyeLookAt(InputAction.CallbackContext context);
        void OnRandomLook(InputAction.CallbackContext context);
    }
    public interface ITestingActionsActions
    {
        void OnTestAction1(InputAction.CallbackContext context);
        void OnTestAction2(InputAction.CallbackContext context);
        void OnTestAction3(InputAction.CallbackContext context);
        void OnTestAction4(InputAction.CallbackContext context);
        void OnTestAction5(InputAction.CallbackContext context);
        void OnTestAction6(InputAction.CallbackContext context);
        void OnTestAction7(InputAction.CallbackContext context);
        void OnTestAction8(InputAction.CallbackContext context);
        void OnTestAction9(InputAction.CallbackContext context);
        void OnTestAction0(InputAction.CallbackContext context);
    }
    public interface IExperimenterControlsActions
    {
        void OnToggleFPS(InputAction.CallbackContext context);
    }
}