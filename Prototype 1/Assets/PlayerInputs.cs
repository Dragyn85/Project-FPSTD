//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.8.2
//     from Assets/PlayerInputs.inputactions
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
using UnityEngine;

public partial class @PlayerInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""FPS"",
            ""id"": ""1e36f06e-2dfc-465a-855c-f1c0800c94d8"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""84a26ead-c376-4cdd-b840-ff7dc9cf98ee"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ecbc2c51-4a36-4b75-8528-d3ba20d99c14"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1de9d2af-07ad-4419-8b04-087e2e3779ed"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5eaaff0-02ec-4ca6-bfb5-5b97f256ba8c"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e47a820-c55e-45c5-8ff1-e9f1c2304754"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a2dc4bd1-c784-4cab-9365-3b1cbb3ce8e1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d17261a4-8fb2-465b-b708-dbe5452d5b17"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""12c249c1-0801-4568-852b-a78f5d41c409"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b4c737b2-4a7d-4d19-a86e-12140f1e9dc9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""711c887e-9489-4c8a-9a60-875e699ecd62"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""God Mode"",
            ""id"": ""b8312908-00ef-4f0b-a247-e22d4d1e60b7"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9136494f-9658-47cf-bafe-7e045ac8da89"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""47fff66b-00fd-4643-843f-568117952dc3"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86469de2-d706-41a3-9582-ca52452bc298"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // FPS
        m_FPS = asset.FindActionMap("FPS", throwIfNotFound: true);
        m_FPS_Look = m_FPS.FindAction("Look", throwIfNotFound: true);
        m_FPS_Move = m_FPS.FindAction("Move", throwIfNotFound: true);
        // God Mode
        m_GodMode = asset.FindActionMap("God Mode", throwIfNotFound: true);
        m_GodMode_Move = m_GodMode.FindAction("Move", throwIfNotFound: true);
    }

    ~@PlayerInputs()
    {
        Debug.Assert(!m_FPS.enabled, "This will cause a leak and performance issues, PlayerInputs.FPS.Disable() has not been called.");
        Debug.Assert(!m_GodMode.enabled, "This will cause a leak and performance issues, PlayerInputs.GodMode.Disable() has not been called.");
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

    // FPS
    private readonly InputActionMap m_FPS;
    private List<IFPSActions> m_FPSActionsCallbackInterfaces = new List<IFPSActions>();
    private readonly InputAction m_FPS_Look;
    private readonly InputAction m_FPS_Move;
    public struct FPSActions
    {
        private @PlayerInputs m_Wrapper;
        public FPSActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_FPS_Look;
        public InputAction @Move => m_Wrapper.m_FPS_Move;
        public InputActionMap Get() { return m_Wrapper.m_FPS; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FPSActions set) { return set.Get(); }
        public void AddCallbacks(IFPSActions instance)
        {
            if (instance == null || m_Wrapper.m_FPSActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_FPSActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IFPSActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IFPSActions instance)
        {
            if (m_Wrapper.m_FPSActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IFPSActions instance)
        {
            foreach (var item in m_Wrapper.m_FPSActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_FPSActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public FPSActions @FPS => new FPSActions(this);

    // God Mode
    private readonly InputActionMap m_GodMode;
    private List<IGodModeActions> m_GodModeActionsCallbackInterfaces = new List<IGodModeActions>();
    private readonly InputAction m_GodMode_Move;
    public struct GodModeActions
    {
        private @PlayerInputs m_Wrapper;
        public GodModeActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GodMode_Move;
        public InputActionMap Get() { return m_Wrapper.m_GodMode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GodModeActions set) { return set.Get(); }
        public void AddCallbacks(IGodModeActions instance)
        {
            if (instance == null || m_Wrapper.m_GodModeActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GodModeActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IGodModeActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IGodModeActions instance)
        {
            if (m_Wrapper.m_GodModeActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGodModeActions instance)
        {
            foreach (var item in m_Wrapper.m_GodModeActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GodModeActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GodModeActions @GodMode => new GodModeActions(this);
    public interface IFPSActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IGodModeActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
