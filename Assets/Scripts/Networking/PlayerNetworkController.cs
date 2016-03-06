using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkController : NetworkBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void NetworkAnimate(bool right, bool left, bool grounded)
    {
        if (isLocalPlayer)
        {
            CmdNetworkAnimate(right, left, grounded);
        }
    }

    [Command]
    void CmdNetworkAnimate(bool right, bool left, bool grounded)
    {
        RpcNetworkAnimate(right, left, grounded);
    }

    [ClientRpc]
    void RpcNetworkAnimate(bool right, bool left, bool grounded)
    {
        if (playerController == null)
            return;
        
        if (!isLocalPlayer)
        {
            playerController.NetworkAnimate(right, left, grounded);
        }
    }
}
