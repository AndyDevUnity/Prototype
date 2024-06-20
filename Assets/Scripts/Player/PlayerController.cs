using InteractableObjects;
using Photon.Pun;
using PlayerControl.InputSystem;
using System;
using UnityEngine;

namespace PlayerControl.PlayerController
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Transform mainCamera;

        [SerializeField]
        private Transform cameraRoot;

        [SerializeField]
        private Camera raycastCamera;

        [SerializeField]
        private Transform itemHolder;

        [SerializeField]
        private Canvas pauseCanvas;

        [SerializeField]
        private float animationBlendSpeed = 8.5f;

        [SerializeField]
        private float interactionDistance = 1f;

        [SerializeField]
        private float upperLimit = -40f;

        [SerializeField]
        private float bottomLimit = 60f;

        [SerializeField]
        private float mouseSensetivity = 21.5f;

        [SerializeField, Range(10, 100)]
        private float jumpFactor = 65f;

        [SerializeField]
        private float walkSpeed = 2.5f;

        [SerializeField]
        private float runSpeed = 6.5f;

        [SerializeField]
        private float distanceToGround = 0.8f;

        [SerializeField]
        private float airResistance = 0.9f;

        [SerializeField]
        private LayerMask groundLayer;

        private PhotonView _photonView;
        private InputManager _inputManager;
        private Rigidbody _playerRigidbody;
        private Animator _animator;
        private InteractableObject _currentInteractableObject;
        private Vector3 _currentVelocity;
        private bool _isGrounded = false;
        private bool _isHandsBusy = false;
        private float _yRotation;
        private string walkAnimationName = "Walk";

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
            _inputManager = GetComponent<InputManager>();
            _playerRigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            _inputManager.OnPaused += OnPause;
            _inputManager.OnInteract += Interaction;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            if (PhotonNetwork.IsMasterClient) return;

            CheckGround();
            Movement();
            Jumping();
            Raycasting();
        }

        private void LateUpdate()
        {
            if (PhotonNetwork.IsMasterClient) return;

            CanMovement();
        }

        private void CheckGround()
        {
            RaycastHit hit;

            if (Physics.Raycast(_playerRigidbody.worldCenterOfMass, Vector3.down, out hit, distanceToGround, groundLayer))
            {
                _isGrounded = true;
                return;
            }

            _isGrounded = false;
            return;
        }

        private void CanMovement()
        {
            var mouseX = _inputManager.Look.x;

            mainCamera.transform.position = cameraRoot.position;

            mainCamera.localRotation = Quaternion.Euler(24f, _yRotation, 0);
            _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0, mouseX * mouseSensetivity * Time.smoothDeltaTime, 0));
        }

        private void Movement()
        {
            float targetSpeed = _inputManager.Run ? runSpeed : walkSpeed;

            if (_inputManager.Move == Vector2.zero) targetSpeed = 0;

            Vector3 input = new Vector2(_inputManager.Move.x, _inputManager.Move.y);
            input = input.normalized;

            if (_isGrounded)
            {
                _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, input.x * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);
                _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, input.y * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);

                Vector3 velocity = transform.TransformVector(new Vector3(_currentVelocity.x, _playerRigidbody.velocity.y, _currentVelocity.y));
                _playerRigidbody.velocity = velocity;
            }
            else
            {
                Vector3 airControl = transform.TransformVector(new Vector3(_currentVelocity.x * airResistance, 0, _currentVelocity.y * airResistance));
                _playerRigidbody.velocity += airControl;
            }

            _animator.SetFloat(walkAnimationName, _currentVelocity.sqrMagnitude);
        }

        private void Jumping()
        {
            if (!_isGrounded) return;
            if (!_inputManager.Jump) return;

            _playerRigidbody.AddForce(-_playerRigidbody.velocity.y * Vector3.up, ForceMode.VelocityChange);
            _playerRigidbody.AddForce(Vector3.up * jumpFactor, ForceMode.Impulse);
        }

        private void Raycasting()
        {
            if (!_isGrounded) return;

            if (_currentInteractableObject != null) return;

            RaycastHit hit;

            var hitting = Physics.Raycast(raycastCamera.transform.position, raycastCamera.transform.forward, out hit, interactionDistance);

            if (hitting)
            {
                var interactableObject = hit.collider.GetComponent<InteractableObject>();

                if (!_isHandsBusy && interactableObject != null && interactableObject.IsReadyToInteract)
                {
                    _currentInteractableObject = interactableObject;
                }
            }
        }

        private void Interaction()
        {
            if (_currentInteractableObject == null) return;

            if (_isGrounded && _currentInteractableObject.IsReadyToInteract && !_isHandsBusy)
            {
                _currentInteractableObject.TakeItem(itemHolder);
                _isHandsBusy = true;
            }
            else
            {
                _currentInteractableObject.DropItem();
                _isHandsBusy = false;
                _currentInteractableObject = null;
            }
        }

        private void OnPause()
        {
            pauseCanvas.gameObject.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
