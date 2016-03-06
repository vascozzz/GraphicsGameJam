using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncRotation : NetworkBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform camTransform;
    [SerializeField] private float lerpRate = 15;

    [SyncVar] private Quaternion syncPlayerRotation;
    [SyncVar] private Quaternion syncCamRotation;

    private Quaternion lastPlayerRotation;
    private Quaternion lastCamRotation;
    private float threshold = 5f;

    void Start()
    {
        playerTransform = transform;
        camTransform = GetComponentInChildren<GunPointerController>().gameObject.transform;
    }

    void FixedUpdate()
    {
        TransmitRotations();
        LerpRotations();
    }

    [ClientCallback]
    void TransmitRotations()
    {
        if (isLocalPlayer && (Quaternion.Angle(playerTransform.rotation, lastPlayerRotation) > threshold || Quaternion.Angle(camTransform.rotation, lastCamRotation) > threshold))
        {
            lastPlayerRotation = playerTransform.rotation;
            lastCamRotation = camTransform.rotation;
            CmdProvideRotationsToServer(playerTransform.rotation, camTransform.rotation);
        }
    }

    void LerpRotations()
    {
        if (!isLocalPlayer)
        {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion camRot)
    {
        syncPlayerRotation = playerRot;
        syncCamRotation = camRot;
    }
}
