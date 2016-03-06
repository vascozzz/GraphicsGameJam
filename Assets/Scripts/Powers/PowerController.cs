using UnityEngine;
using System.Collections;

public class PowerController : MonoBehaviour
{
    [SerializeField] private float maxJuice;
    private float currentJuice;

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

        activePower = rewindPower;
        currentJuice = 0;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            shockwavePower.Step();
        }
    }

    public void UsedPower()
    {
        currentJuice -= activePower.juiceCost;
    }

    public void RefillJuice(string juiceName)
    {
        if (juiceName == "JuiceGravity")
        {
            Debug.Log("picked up JuiceGravity");
            activePower = gravityPower;
        }
        else if (juiceName == "JuicePortal")
        {
            Debug.Log("picked up JuicePortal");
            activePower = portalPower;
        }
        else if (juiceName == "JuiceRewind")
        {
            Debug.Log("picked up JuiceRewind");
            activePower = rewindPower;
        }
            
        currentJuice = maxJuice;
    }
}
