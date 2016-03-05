using UnityEngine;
using System.Collections;

public class PowerController : MonoBehaviour
{
    private PortalPower portalPower;
    private SlowmoPower slowmoPower;
    private GravityPower gravityPower;
    private RewindPower rewindPower;

    private Power activePower;

    void Start()
    {
        portalPower = GetComponent<PortalPower>();
        slowmoPower = GetComponent<SlowmoPower>();
        gravityPower = GetComponent<GravityPower>();
        rewindPower = GetComponent<RewindPower>();

        activePower = portalPower;
    }

    void Update()
    {
        UpdateActivePower();
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
            activePower = slowmoPower;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activePower = gravityPower;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            activePower = rewindPower;
        }
    }
}
