using Photon.Pun;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();

        if (!_photonView.IsMine)
            gameObject.SetActive(false);
    }
}
