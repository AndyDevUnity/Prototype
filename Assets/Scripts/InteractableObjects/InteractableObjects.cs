using UnityEngine;

namespace InteractableObjects
{
    [RequireComponent(typeof(Collider))]
    public class InteractableObject : MonoBehaviour
    {
        public bool IsReadyToInteract { get; private set; }

        private Rigidbody _rigidbody;

        private Transform _itemHolder;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            SetInteractable(true);
        }

        private void FixedUpdate()
        {
            if (_itemHolder == null) return;

            gameObject.transform.position = _itemHolder.position;
        }

        public void TakeItem(Transform itemHolder)
        {
            if (IsReadyToInteract)
            {
                _rigidbody.isKinematic = true;
                _itemHolder = itemHolder;
                _rigidbody.MovePosition(_itemHolder.position);
                SetInteractable(false);
            }
        }

        public void DropItem()
        {
            _itemHolder = null;
            _rigidbody.isKinematic = false;
            SetInteractable(true);
        }

        public void SetInteractable(bool isReady)
        {
            IsReadyToInteract = isReady;
        }
    }
}