using Photon.Pun;
using ScriptableObjects;
using UnityEngine;

namespace SpawnerManager
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private bool spawnInParentTransform;

        [SerializeField]
        private InteractItem itemToSpawn;

        private GameObject currentSpawnItem;

        public void Construct()
        {
            SpawnItem();
        }

        public GameObject GetCurrentItem()
        {
            if (currentSpawnItem == null) return null;

            return currentSpawnItem;
        }

        private void SpawnItem()
        {
            if (itemToSpawn == null) return;

            if (spawnInParentTransform)
            {
                currentSpawnItem = Instantiate(itemToSpawn.ItemToSpawn, gameObject.transform.position, gameObject.transform.rotation, transform);
            }
            else
            {
                currentSpawnItem = PhotonNetwork.Instantiate(itemToSpawn.ItemToSpawn.name, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}