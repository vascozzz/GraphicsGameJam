using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;

    [SerializeField] private GameObject[] juices;
    [SerializeField] private GameObject[] juiceSpawnLocs;
    [SerializeField] private float juiceSpawnLocVariation; // max random distance between a juice and its original spawn point
    [SerializeField] private float minJuiceSpawnInterval;
    [SerializeField] private float maxJuiceSpawnInterval;

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
        StartCoroutine(SpawnJuice());
	}
	
	void Update () 
    {
	
	}

    IEnumerator SpawnJuice()
    {
        float nextSpawn = Random.Range(minJuiceSpawnInterval, maxJuiceSpawnInterval);
        yield return new WaitForSeconds(nextSpawn);

        int juice = Random.Range(0, juices.Length);
        int location = Random.Range(0, juiceSpawnLocs.Length);
        float variation = Random.Range(0f, juiceSpawnLocVariation);

        Vector3 spawnPos = juiceSpawnLocs[location].transform.position;
        spawnPos.x += variation;
        spawnPos.y += variation;

        GameObject juiceObj = Instantiate(juices[juice], spawnPos, Quaternion.identity) as GameObject;
        juiceObj.name = juices[juice].name;

        StartCoroutine(SpawnJuice());
    }
}
