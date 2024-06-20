using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class OwnershipRequestHandler : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        ChangeOwnerShip();
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView == _photonView)
        {
            if (targetView.IsMine)
            {
                targetView.TransferOwnership(requestingPlayer);
            }
        }
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        //Debug.Log($"Change owner {targetView}, to {previousOwner}.");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        //Debug.Log("Error change owner.");
    }

    private void ChangeOwnerShip()
    {
        if (!_photonView.IsMine)
        {
            _photonView.RequestOwnership();
        }
    }
}