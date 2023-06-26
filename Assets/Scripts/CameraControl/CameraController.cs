//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/CameraControl/CameraController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @CameraController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraController"",
    ""maps"": [
        {
            ""name"": ""Controller"",
            ""id"": ""cf14089e-bbf0-4fc8-b905-d4d914616769"",
            ""actions"": [
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""618ff652-0156-484a-829e-e24cf6f82f61"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Scale"",
                    ""type"": ""Value"",
                    ""id"": ""53f192f4-a44a-4790-94f0-dfe8f65d1167"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""EnabledDisabledPauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""9d9d99de-2cb8-4585-a4cb-531060f9f229"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""35fb5339-7a83-4f08-9c48-a197c9df667f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""da7b37a6-1799-4fc2-9773-211b92393ab8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""86196299-d523-480d-b091-750e25cb6566"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""af047544-ea0e-44d9-8cd9-a73c0901acdc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6b9da7f1-3947-48b1-bc43-0a62fd515626"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""UpDownLeftRight"",
                    ""id"": ""f0a1ca62-28bd-4292-8291-97f5fd3ab8a1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2a84e58d-0d51-4279-b5a8-ad0ed797276e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""95d25464-be57-47b6-b58e-b82e618a0d71"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""04e98839-dc0b-4d9a-bea0-d0c6d0a79bd3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9b50ab5d-7a4f-4bc1-a419-9f406a68e432"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""30137690-47fc-4064-ab0a-281f97f63148"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scale"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9209bae6-86bd-460e-90af-6359cb118be3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnabledDisabledPauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controller
        m_Controller = asset.FindActionMap("Controller", throwIfNotFound: true);
        m_Controller_MoveCamera = m_Controller.FindAction("MoveCamera", throwIfNotFound: true);
        m_Controller_Scale = m_Controller.FindAction("Scale", throwIfNotFound: true);
        m_Controller_EnabledDisabledPauseMenu = m_Controller.FindAction("EnabledDisabledPauseMenu", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Controller
    private readonly InputActionMap m_Controller;
    private IControllerActions m_ControllerActionsCallbackInterface;
    private readonly InputAction m_Controller_MoveCamera;
    private readonly InputAction m_Controller_Scale;
    private readonly InputAction m_Controller_EnabledDisabledPauseMenu;
    public struct ControllerActions
    {
        private @CameraController m_Wrapper;
        public ControllerActions(@CameraController wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCamera => m_Wrapper.m_Controller_MoveCamera;
        public InputAction @Scale => m_Wrapper.m_Controller_Scale;
        public InputAction @EnabledDisabledPauseMenu => m_Wrapper.m_Controller_EnabledDisabledPauseMenu;
        public InputActionMap Get() { return m_Wrapper.m_Controller; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerActions set) { return set.Get(); }
        public void SetCallbacks(IControllerActions instance)
        {
            if (m_Wrapper.m_ControllerActionsCallbackInterface != null)
            {
                @MoveCamera.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMoveCamera;
                @Scale.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnScale;
                @Scale.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnScale;
                @Scale.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnScale;
                @EnabledDisabledPauseMenu.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnEnabledDisabledPauseMenu;
                @EnabledDisabledPauseMenu.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnEnabledDisabledPauseMenu;
                @EnabledDisabledPauseMenu.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnEnabledDisabledPauseMenu;
            }
            m_Wrapper.m_ControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @Scale.started += instance.OnScale;
                @Scale.performed += instance.OnScale;
                @Scale.canceled += instance.OnScale;
                @EnabledDisabledPauseMenu.started += instance.OnEnabledDisabledPauseMenu;
                @EnabledDisabledPauseMenu.performed += instance.OnEnabledDisabledPauseMenu;
                @EnabledDisabledPauseMenu.canceled += instance.OnEnabledDisabledPauseMenu;
            }
        }
    }
    public ControllerActions @Controller => new ControllerActions(this);
    public interface IControllerActions
    {
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnScale(InputAction.CallbackContext context);
        void OnEnabledDisabledPauseMenu(InputAction.CallbackContext context);
    }
}