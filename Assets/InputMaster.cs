// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""2b839929-f5e9-4a41-9d47-a1ec1ea9e487"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""3eeac351-e1c8-47c2-ada5-128d89e9a45b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""3b240775-598b-4d8d-a031-3aac5b08025f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShootL"",
                    ""type"": ""Button"",
                    ""id"": ""052d53d3-e9cc-423c-8f55-a83855582b63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ShootR"",
                    ""type"": ""Button"",
                    ""id"": ""96af447a-b654-4ce0-b001-3567a8c550b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b9e5b493-95ef-4157-bb97-6ca49bb3b556"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Pickup"",
                    ""type"": ""Button"",
                    ""id"": ""b000e5ef-9b79-4288-ae3b-588f3ccf63bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ResetGame"",
                    ""type"": ""Button"",
                    ""id"": ""187b8b21-4cf7-4d96-9bb3-01fca4ad241e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ResetMagnetism"",
                    ""type"": ""Button"",
                    ""id"": ""a4102fc6-5efd-48f2-9ee3-d22e6728df7b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ShootNeutral"",
                    ""type"": ""Button"",
                    ""id"": ""29484501-de25-4a1e-aa45-a48216afaf23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""1"",
                    ""type"": ""Button"",
                    ""id"": ""72e1dc26-a9b9-4d57-b15f-eb54dcc2f6b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""2"",
                    ""type"": ""Button"",
                    ""id"": ""0777606b-2c6c-4865-8368-4cb4b59651bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""3"",
                    ""type"": ""Button"",
                    ""id"": ""44776222-0977-4bfa-9f74-979f424793ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""4"",
                    ""type"": ""Button"",
                    ""id"": ""47213128-adf4-45dc-b058-d1c2f44b256a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""5"",
                    ""type"": ""Button"",
                    ""id"": ""04f3af1f-b775-4492-8023-167a90e2ced3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""17d86393-4810-4faf-b90a-14f5df1709a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""86d7829c-090f-4b17-bc85-d0d26efe9ccd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d37e59a9-1a79-42cb-bf1d-2edf932eaac3"",
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
                    ""id"": ""975d7705-a28f-49b9-a2f8-2f1a698c1fa6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b37c5da8-d57b-48a0-a73f-6cf631b5204a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""641b3b72-a1a8-4f63-966a-c2f378e21d3f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5f460080-82a2-4dab-b5ad-0fca9562e0ed"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""95d50846-9793-48ae-ad43-1a1570b4d9bc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""585c385f-8670-4f2b-9e8a-19d93ad9245f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.1,y=0.1)"",
                    ""groups"": ""Default"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""985e8320-02eb-4226-bb26-9c0977a53f3d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Default"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""caf7d83f-0433-4eaf-a18d-60127b149388"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""ShootL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed60867e-2ee6-4bc6-9c66-8b4db3b0dab1"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""ShootL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5747b149-c73b-4111-9edc-4a930c4bff14"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""ShootR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1faf1289-3907-47b8-86cb-bb2853de0d3c"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""ShootR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""791ad981-8181-47a3-98e3-9ccdfbefe169"",
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
                    ""id"": ""82b860e5-2ee5-451e-9a25-c27469877bbd"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95e20ec1-9f58-49fd-a071-072ac9f29367"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""568e828c-da8f-4ef8-8cb6-33f053eedd1d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""111fc1d8-8e44-4b11-b747-6d4db25bb9e7"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ce79a81-d652-442e-a9f4-e4299326b140"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c347808f-9152-4d24-a651-ceb57d43c51a"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b3071c2-c135-4c45-8eaa-5bab580e84bf"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""ResetMagnetism"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""957cca63-b21c-4ff5-b1f6-d9ec84ad1e12"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""ResetMagnetism"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f2eb34d-d96c-42f3-8073-21262750cdaf"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""ResetMagnetism"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1139d085-5c4c-458c-a8f7-9cb150646017"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootNeutral"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d702a77-8f76-400c-a19a-938497b6ada9"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eca72345-87a3-4536-9c82-e97c407d330b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13b3880d-ff15-46f2-9258-95cbc2db932e"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60eccf9f-3619-40f2-80b8-e1c52c292cd4"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""118d2ce0-4d87-4898-a38c-654c044a84c4"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2d96f72-1174-4f29-872e-011804cb031f"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c58a6ca-0285-4ac1-b6c6-e677b367b679"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61f33cdf-fcd1-4533-8562-927b37f907cb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default"",
            ""bindingGroup"": ""Default"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_ShootL = m_Player.FindAction("ShootL", throwIfNotFound: true);
        m_Player_ShootR = m_Player.FindAction("ShootR", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Pickup = m_Player.FindAction("Pickup", throwIfNotFound: true);
        m_Player_ResetGame = m_Player.FindAction("ResetGame", throwIfNotFound: true);
        m_Player_ResetMagnetism = m_Player.FindAction("ResetMagnetism", throwIfNotFound: true);
        m_Player_ShootNeutral = m_Player.FindAction("ShootNeutral", throwIfNotFound: true);
        m_Player__1 = m_Player.FindAction("1", throwIfNotFound: true);
        m_Player__2 = m_Player.FindAction("2", throwIfNotFound: true);
        m_Player__3 = m_Player.FindAction("3", throwIfNotFound: true);
        m_Player__4 = m_Player.FindAction("4", throwIfNotFound: true);
        m_Player__5 = m_Player.FindAction("5", throwIfNotFound: true);
        m_Player_Tab = m_Player.FindAction("Tab", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_ShootL;
    private readonly InputAction m_Player_ShootR;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Pickup;
    private readonly InputAction m_Player_ResetGame;
    private readonly InputAction m_Player_ResetMagnetism;
    private readonly InputAction m_Player_ShootNeutral;
    private readonly InputAction m_Player__1;
    private readonly InputAction m_Player__2;
    private readonly InputAction m_Player__3;
    private readonly InputAction m_Player__4;
    private readonly InputAction m_Player__5;
    private readonly InputAction m_Player_Tab;
    private readonly InputAction m_Player_Pause;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @ShootL => m_Wrapper.m_Player_ShootL;
        public InputAction @ShootR => m_Wrapper.m_Player_ShootR;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Pickup => m_Wrapper.m_Player_Pickup;
        public InputAction @ResetGame => m_Wrapper.m_Player_ResetGame;
        public InputAction @ResetMagnetism => m_Wrapper.m_Player_ResetMagnetism;
        public InputAction @ShootNeutral => m_Wrapper.m_Player_ShootNeutral;
        public InputAction @_1 => m_Wrapper.m_Player__1;
        public InputAction @_2 => m_Wrapper.m_Player__2;
        public InputAction @_3 => m_Wrapper.m_Player__3;
        public InputAction @_4 => m_Wrapper.m_Player__4;
        public InputAction @_5 => m_Wrapper.m_Player__5;
        public InputAction @Tab => m_Wrapper.m_Player_Tab;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @ShootL.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootL;
                @ShootL.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootL;
                @ShootL.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootL;
                @ShootR.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootR;
                @ShootR.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootR;
                @ShootR.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootR;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Pickup.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Pickup.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Pickup.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @ResetGame.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnResetGame;
                @ResetGame.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnResetGame;
                @ResetGame.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnResetGame;
                @ResetMagnetism.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnResetMagnetism;
                @ResetMagnetism.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnResetMagnetism;
                @ResetMagnetism.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnResetMagnetism;
                @ShootNeutral.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootNeutral;
                @ShootNeutral.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootNeutral;
                @ShootNeutral.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootNeutral;
                @_1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.On_1;
                @_1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.On_1;
                @_1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.On_1;
                @_2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.On_2;
                @_2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.On_2;
                @_2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.On_2;
                @_3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.On_3;
                @_3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.On_3;
                @_3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.On_3;
                @_4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.On_4;
                @_4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.On_4;
                @_4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.On_4;
                @_5.started -= m_Wrapper.m_PlayerActionsCallbackInterface.On_5;
                @_5.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.On_5;
                @_5.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.On_5;
                @Tab.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTab;
                @Tab.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTab;
                @Tab.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTab;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @ShootL.started += instance.OnShootL;
                @ShootL.performed += instance.OnShootL;
                @ShootL.canceled += instance.OnShootL;
                @ShootR.started += instance.OnShootR;
                @ShootR.performed += instance.OnShootR;
                @ShootR.canceled += instance.OnShootR;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Pickup.started += instance.OnPickup;
                @Pickup.performed += instance.OnPickup;
                @Pickup.canceled += instance.OnPickup;
                @ResetGame.started += instance.OnResetGame;
                @ResetGame.performed += instance.OnResetGame;
                @ResetGame.canceled += instance.OnResetGame;
                @ResetMagnetism.started += instance.OnResetMagnetism;
                @ResetMagnetism.performed += instance.OnResetMagnetism;
                @ResetMagnetism.canceled += instance.OnResetMagnetism;
                @ShootNeutral.started += instance.OnShootNeutral;
                @ShootNeutral.performed += instance.OnShootNeutral;
                @ShootNeutral.canceled += instance.OnShootNeutral;
                @_1.started += instance.On_1;
                @_1.performed += instance.On_1;
                @_1.canceled += instance.On_1;
                @_2.started += instance.On_2;
                @_2.performed += instance.On_2;
                @_2.canceled += instance.On_2;
                @_3.started += instance.On_3;
                @_3.performed += instance.On_3;
                @_3.canceled += instance.On_3;
                @_4.started += instance.On_4;
                @_4.performed += instance.On_4;
                @_4.canceled += instance.On_4;
                @_5.started += instance.On_5;
                @_5.performed += instance.On_5;
                @_5.canceled += instance.On_5;
                @Tab.started += instance.OnTab;
                @Tab.performed += instance.OnTab;
                @Tab.canceled += instance.OnTab;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_DefaultSchemeIndex = -1;
    public InputControlScheme DefaultScheme
    {
        get
        {
            if (m_DefaultSchemeIndex == -1) m_DefaultSchemeIndex = asset.FindControlSchemeIndex("Default");
            return asset.controlSchemes[m_DefaultSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnShootL(InputAction.CallbackContext context);
        void OnShootR(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPickup(InputAction.CallbackContext context);
        void OnResetGame(InputAction.CallbackContext context);
        void OnResetMagnetism(InputAction.CallbackContext context);
        void OnShootNeutral(InputAction.CallbackContext context);
        void On_1(InputAction.CallbackContext context);
        void On_2(InputAction.CallbackContext context);
        void On_3(InputAction.CallbackContext context);
        void On_4(InputAction.CallbackContext context);
        void On_5(InputAction.CallbackContext context);
        void OnTab(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
