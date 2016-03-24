using UnityEngine;
using System.Collections;

public class PowerController : MonoBehaviour
{
    [SerializeField] private float maxJuice = 100f;
    private float currentJuice;

    private PlayerController playerCtrl;

    private PortalPower portalPower;
    private GravityPower gravityPower;
    private RewindPower rewindPower;
    private ShockwavePower shockwavePower;

    private Power activePower;

    void Start()
    {
        playerCtrl = GetComponent<PlayerController>();

        portalPower = GetComponent<PortalPower>();
        gravityPower = GetComponent<GravityPower>();
        rewindPower = GetComponent<RewindPower>();
        shockwavePower = GetComponent<ShockwavePower>();

        activePower = rewindPower;
        currentJuice = 0f;
    }

    void Update()
    {
        UpdateShockwave();

        if (currentJuice >= activePower.juiceCost)
        {
            activePower.Step();
        }
    }

    private void UpdateShockwave()
    {
        if (Input.GetButtonDown("Shockwave" + playerCtrl.GetPlayer()))
        {
            shockwavePower.Step();
        }
    }

    public void UsedPower()
    {
        currentJuice -= activePower.juiceCost;

        HUDManager.instance.UpdatePlayerJuice(currentJuice, playerCtrl.GetPlayer());
    }

    public void RefillJuice(string juiceType)
    {
        currentJuice = maxJuice;

        if (juiceType == "JuiceGravity")
        {
            activePower = gravityPower;
        }
        else if (juiceType == "JuicePortal")
        {
            activePower = portalPower;
        }
        else if (juiceType == "JuiceRewind")
        {
            activePower = rewindPower;
        }

        HUDManager.instance.UpdatePlayerJuice(currentJuice, juiceType, playerCtrl.GetPlayer());
    }
}
