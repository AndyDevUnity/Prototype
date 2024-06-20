using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControl.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput playerInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }
        public bool Jump { get; private set; }
        public bool Crouch { get; private set; }
        public bool Interact { get; private set; }

        public event Action OnInteract;
        public event Action OnPaused;

        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _jumpAction;
        private InputAction _crouchAction;
        private InputAction _interactAction;
        private InputAction _pauseAction;

        private void Awake()
        {
            _currentMap = playerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction = _currentMap.FindAction("Run");
            _jumpAction = _currentMap.FindAction("Jump");
            _crouchAction = _currentMap.FindAction("Crouch");
            _interactAction = _currentMap.FindAction("Interact");
            _pauseAction = _currentMap.FindAction("Pause");

            _crouchAction.started += onCrouch;
            _interactAction.started += onInteract;
            _pauseAction.started += onPause;

            _moveAction.performed += onMove;
            _lookAction.performed += onLook;
            _runAction.performed += onRun;
            _jumpAction.performed += onJump;

            _moveAction.canceled += onMove;
            _lookAction.canceled += onLook;
            _runAction.canceled += onRun;
            _jumpAction.canceled += onJump;
            _crouchAction.canceled += onCrouch;
        }

        private void OnEnable()
        {
            _currentMap.Enable();
        }

        private void OnDisable()
        {
            _currentMap.Disable();
        }

        private void onMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        private void onLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void onRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }

        private void onJump(InputAction.CallbackContext context)
        {
            Jump = context.ReadValueAsButton();
        }

        private void onCrouch(InputAction.CallbackContext context)
        {
            Crouch = context.ReadValueAsButton();
        }

        private void onInteract(InputAction.CallbackContext context)
        {
            OnInteract?.Invoke();
        }

        private void onPause(InputAction.CallbackContext context)
        {
            OnPaused?.Invoke();
        }
    }
}