using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class PhaseNetworkManager : NetworkManager
{
    private Button hostBtn;
    private Button joinBtn;
    private Text ipAddress;
    private Text stateInfo;

    void Start()
    {
        SetMenuReferences();
    }

    /*
     * Starts up a new server.
     */
    public void CreateHost()
    {
        SetPort();
        DisplayInfo("The game will be starting in a moment...");

        NetworkManager.singleton.StartHost();
    }

    /*
     * Attempts to join a server.
     */
    public void JoinGame()
    {
        SetIpAddress();
        SetPort();
        DisplayInfo("You will be playing in a moment...");

        NetworkManager.singleton.StartClient();
        // client.RegisterHandler(MsgType.Error, OnNetworkError);
    }

    void OnNetworkError(NetworkMessage msg)
    {
        DisplayInfo("Something happened and you were unable to join...", true);
    }

    /*
     * 
     */
    void SetIpAddress()
    {
        if (ipAddress.text == "")
        {
            NetworkManager.singleton.networkAddress = "localhost";
        }
        else
        {
            NetworkManager.singleton.networkAddress = ipAddress.text;
        }
    }

    /*
     * 
     */
    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    /*
     * Saves the player's name so it can passed onto other scenes.
     */
    public void SetPlayerName()
    {
        //string playerName = playerNameInput.text;
        //PlayerPrefs.SetString("nickname", playerName);
    }

    /*
     * Creates references to all elements in the menu so they can be accessed anywhere.
     */
    void SetMenuReferences()
    {
        hostBtn = GameObject.Find("Host Button").GetComponent<Button>();
        joinBtn = GameObject.Find("Join Button").GetComponent<Button>();
        ipAddress = GameObject.Find("Address Input").transform.FindChild("Text").GetComponent<Text>();
        stateInfo = GameObject.Find("State Info").GetComponent<Text>();
    }

    /*
     * Checks if the player has already set a name before. If he has, the name is pre-selected.
     */
    void CheckPlayerPrefs()
    {
        //string storedName = PlayerPrefs.GetString("nickname");

        //if (storedName != "")
        //{
        //    playerNameInput.text = storedName;
        //}
    }

    /*
     * Used to display general state info and network warnings.
     */
    void DisplayInfo(string info, bool hideAfterTimeout = false)
    {
        stateInfo.text = info;
        stateInfo.enabled = true;

        if (hideAfterTimeout)
        {
            StopCoroutine(HideInfo());
            StartCoroutine(HideInfo());
        }

    }

    IEnumerator HideInfo()
    {
        yield return new WaitForSeconds(10f);

        stateInfo.enabled = false;
    }

    /*
     * Called when a new scene is loaded (as this has "Don't Destroy On Load").
     * Note that this is NOT called the very first time the script starts, but only when 
     * changing scenes (after disconnecting and returning to the menu, for example).
     */
    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            StartCoroutine(SetupMenuScene());
        }
        else
        {
            StartCoroutine(SetupGeneralScenes());
        }
    }

    /*
     * Sets up all references in the menu scene.
     * This needs to be coded, and not just linked in the editor, as editor links 
     * are lost on scene changes and Start() does not run.
     */
    IEnumerator SetupMenuScene()
    {
        // when returning to the menu, UNET's singleton handler needs a bit of time to handle 
        // the two NetworkManagers (the new one, and the previous one that should be deleted)
        yield return new WaitForSeconds(0.3f);

        SetMenuReferences();
        CheckPlayerPrefs();

        hostBtn.onClick.RemoveAllListeners();
        hostBtn.onClick.AddListener(CreateHost);

        joinBtn.onClick.RemoveAllListeners();
        joinBtn.onClick.AddListener(JoinGame);

        // p1Btn.onClick.RemoveAllListeners();
        // p1Btn.onClick.AddListener(delegate { SetPlayerRole("P1"); });
    }

    /*
     * Used to setup references in other scenes (as this has "Don't Destroy On Load").
     * Could be used to setup a Disconnect button, for example.
     */
    IEnumerator SetupGeneralScenes()
    {
        yield return new WaitForSeconds(0.3f);
    }
}