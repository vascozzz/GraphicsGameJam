using UnityEngine;
using System.Collections;

public class PowerController : MonoBehaviour {

    private PortalPower portalPower;
    private SlowmoPower slowmoPower;
    private GravityPower gravityPower;

    private Power activePower;

	// Use this for initialization
	void Start () {
        portalPower = GetComponent<PortalPower>();
        slowmoPower = GetComponent<SlowmoPower>();
        gravityPower = GetComponent<GravityPower>();
        activePower = portalPower;
	}
	
	// Update is called once per frame
	void Update () {
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
    }
}
