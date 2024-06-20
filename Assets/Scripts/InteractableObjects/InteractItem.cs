using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create InteractItem", fileName = "InteractItem", order = 0)]
    public class InteractItem : ScriptableObject
    {
        [SerializeField]
        private string itemName;

        [SerializeField]
        private GameObject itemToSpawn;

        public string ItemName => itemName;
        public GameObject ItemToSpawn => itemToSpawn;
    }
}