using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;

    [SerializeField] private int nPlayers = 2;
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private GameObject[] playerSpawnsLocs;

    [SerializeField] private GameObject[] juicePrefabs;
    [SerializeField] private GameObject[] juiceSpawnLocs;
    [SerializeField] private float juiceSpawnLocVariation; // max random distance between a juice and its original spawn point
    [SerializeField] private float minJuiceSpawnInterval;
    [SerializeField] private float maxJuiceSpawnInterval;

    [SerializeField] private float timeToDeathCam = 1.5f;
    [SerializeField] private float deathCamTime = 5f;

    private List<PlayerController> players;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }
        
	void Start () 
    {
        players = new List<PlayerController>();

        for (int i = 0; i < nPlayers; i++)
        {
            GameObject playerObj = Instantiate(playerPrefabs[i], playerSpawnsLocs[i].transform.position, Quaternion.identity) as GameObject;
            PlayerController controller = playerObj.GetComponent<PlayerController>();
            controller.SetPlayer(i + 1);

            players.Add(controller);
        }

        StartCoroutine(SpawnJuice());
	}

    IEnumerator SpawnJuice()
    {
        float nextSpawn = Random.Range(minJuiceSpawnInterval, maxJuiceSpawnInterval);
        yield return new WaitForSeconds(nextSpawn);

        int juice = Random.Range(0, juicePrefabs.Length);
        int location = Random.Range(0, juiceSpawnLocs.Length);
        float variation = Random.Range(0f, juiceSpawnLocVariation);

        Vector3 spawnPos = juiceSpawnLocs[location].transform.position;
        spawnPos.x += variation;
        spawnPos.y += variation;

        GameObject juiceObj = Instantiate(juicePrefabs[juice], spawnPos, Quaternion.identity) as GameObject;
        juiceObj.name = juicePrefabs[juice].name;

        StartCoroutine(SpawnJuice());
    }

    public void OnPlayerDeath()
    {
        PlayerController lastPLayer = null;
        int livingPlayers = 0;

        // check number of living players
        foreach (PlayerController player in players)
        {
            if (!player.isDead)
            {
                livingPlayers++;
                lastPLayer = player;
            }
        }

        // if only one, end game
        if (livingPlayers == 1)
        {
            StartCoroutine(ReloadScene(lastPLayer.transform.position));
        }
    }

    IEnumerator ReloadScene(Vector3 lastPlayerPos)
    {
        yield return new WaitForSeconds(timeToDeathCam);

        CameraManager.instance.ZoomOnTarget(lastPlayerPos, 1.5f);

        yield return new WaitForSeconds(deathCamTime);

        Application.LoadLevel(1);
    }
}
