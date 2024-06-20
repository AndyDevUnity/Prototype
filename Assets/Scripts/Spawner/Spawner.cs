using System.Collections.Generic;
using UnityEngine;

namespace SpawnerManager
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private List<SpawnPoint> allInteractableCubeSpawnPoints = new();

        [SerializeField]
        private List<SpawnPoint> allStaticCubeSpawnPoints = new();

        private int minCubeCountToSpawn = 5;

        public void Construct()
        {
            InitialInstantiateInteractableObjects();
        }

        private void InitialInstantiateInteractableObjects()
        {
            for (int i = 0; i < minCubeCountToSpawn; i++)
            {
                var pointIndex = GetRandomPointIndex(allStaticCubeSpawnPoints);

                allStaticCubeSpawnPoints[pointIndex].Construct();

                allStaticCubeSpawnPoints.RemoveAt(pointIndex);
            }

            foreach (var item in allInteractableCubeSpawnPoints) 
                item.Construct();
        }

        private int GetRandomPointIndex(List<SpawnPoint> points)
        {
            var randomPoint = Random.Range(0, points.Count);

            return randomPoint;
        }
    }
}
