using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    private GameObject[] monsterPrefabs;

    public float spawnInterval = 4; // Time interval between monster spawns
    public float minSpawnInterval = 0.5f; // Minimum time interval between monster spawns

    public float spawnCurve = 0.97f;

    private float timer = 0f;
    void Start()
    {
       monsterPrefabs = Resources.LoadAll<GameObject>("Prefabs/Monsters"); 
    }

    public void GenerateMonster()
    {
        if(timer >= spawnInterval)
        {     
            timer = 0f;
            int randomIndex = Random.Range(0, monsterPrefabs.Length);
            GameObject monsterPrefab = monsterPrefabs[randomIndex];
            Vector3 spawnPosition = new Vector3(ChooseRandomLane(),transform.position.y,transform.position.z);
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, transform);
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval * spawnCurve);
        }
        else timer += Time.deltaTime; 
    }

    private int ChooseRandomLane()
    {
        int randomLane = Random.Range(-1, 2); // Assuming there are 3 lanes (0, 1, 2)
        return randomLane;
    }
}
