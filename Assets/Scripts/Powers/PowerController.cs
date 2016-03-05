using UnityEngine;
using System.Collections;

public class PowerController : MonoBehaviour
{
    private PortalPower portalPower;
    private GravityPower gravityPower;
    private RewindPower rewindPower;
    private ShockwavePower shockwavePower;

    private Power activePower;

    void Start()
    {
        portalPower = GetComponent<PortalPower>();
        gravityPower = GetComponent<GravityPower>();
        rewindPower = GetComponent<RewindPower>();
        shockwavePower = GetComponent<ShockwavePower>();

        activePower = portalPower;
    }

    void Update()
    {
        UpdateActivePower();
        UpdateShockwave();
        activePower.Step();
    }

    private void UpdateActivePower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activePower = portalPower;
        }
            
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activePower = gravityPower;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activePower = rewindPower;
        }
    }

    private void UpdateShockwave()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shockwavePower.Step();
        }
    }
}
