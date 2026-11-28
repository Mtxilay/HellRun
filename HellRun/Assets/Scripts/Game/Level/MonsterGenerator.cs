using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    private GameObject[] monsterPrefabs;

    private float spawnInterval = 3; // Time interval between monster spawns

    public float maxIntervalTime=3;
    public float minSpawnInterval = 1f; // Minimum time interval between monster spawns

    public float spawnCurve = 0.99f;

    private float timer = 0f;
    void Start()
    {
       monsterPrefabs = Resources.LoadAll<GameObject>("Prefabs/Monsters"); 
       spawnInterval=maxIntervalTime;
    }

    public void GenerateMonster()
    {
        if(timer >= spawnInterval)
        {
            spawnInterval=Random.Range(minSpawnInterval,maxIntervalTime);    
            timer = 0f;
            int randomIndex = Random.Range(0, monsterPrefabs.Length);
            GameObject monsterPrefab = monsterPrefabs[randomIndex];
            Vector3 spawnPosition = new Vector3(ChooseRandomLane(),transform.position.y,transform.position.z);
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, transform);
            maxIntervalTime = Mathf.Max(minSpawnInterval, maxIntervalTime * spawnCurve);
        }
        else timer += Time.deltaTime; 
    }

    private int ChooseRandomLane()
    {
        int randomLane = Random.Range(-1, 2); // Assuming there are 3 lanes (0, 1, 2)
        return randomLane;
    }
}
