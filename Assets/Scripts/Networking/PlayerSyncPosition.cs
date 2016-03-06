using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncPosition : NetworkBehaviour
{

    [SerializeField] Transform myTransform;
    [SerializeField] float lerpRate = 15;

    [SyncVar] private Vector3 syncPos;

    private Vector3 lastPos;
    private float threshold = 0.5f;

    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshold)
        {
            lastPos = myTransform.position;
            CmdProvidePositionToServer(myTransform.position);
        }
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }
}