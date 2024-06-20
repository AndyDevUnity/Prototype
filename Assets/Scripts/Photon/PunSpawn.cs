using Photon.Pun;
using SpawnerManager;
using System.Collections.Generic;
using UnityEngine;

public class PUNSpawn : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private GameObject adminCameraPrefab;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private Transform adminCameraPoint;

    [SerializeField]
    private List<Transform> playerSpawnPoints = new();

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        InstantiatePlayer();
    }

    private int GetRadomSpawnPoint()
    {
        var randomPoint = Random.Range(0, playerSpawnPoints.Count);
        return randomPoint;
    }

    private void InstantiatePlayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var randomPoint = GetRadomSpawnPoint();

            PhotonNetwork.Instantiate(adminCameraPrefab.name, adminCameraPoint.position, adminCameraPoint.rotation);

            PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoints[randomPoint].position, playerSpawnPoints[randomPoint].rotation);

            spawner.Construct();
        }
    }
}
