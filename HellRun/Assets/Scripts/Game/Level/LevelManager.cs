using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GroundMover groundMover;

    public ScreenFlow screenFlow;
    private MonsterGenerator monsterGenerator;
    private GameObject[] player;
    public int currentLevel = 0;
    public float currentSpeed = 3f;
    public float speedIncreaseInterval = 5f;
    public float speedGrowth = 1.01f;
    public float maxSpeed = 6f;

    private float speedTimer;

    public event Action moveMosters;


    public int KillCount { get; set; } = 0;
    
    void Start()
    {
        groundMover = this.gameObject.GetComponentInChildren<GroundMover>();
        monsterGenerator = this.gameObject.GetComponentInChildren<MonsterGenerator>();
        screenFlow=FindObjectOfType<ScreenFlow>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeed();
        UpdateRun();
    }

    private void UpdateSpeed()
    {
        speedTimer += Time.deltaTime;

        if (speedTimer >= speedIncreaseInterval)
        {
            speedTimer -= speedIncreaseInterval;
            currentSpeed = Mathf.Min(currentSpeed * speedGrowth, maxSpeed);
        }
    }


    private void UpdateRun()
    {
        groundMover.MoveTiles();
        monsterGenerator.GenerateMonster();
        moveMosters?.Invoke();

    }

    public void EndRun()
    {
        screenFlow.CompleteLevel();
    }


}
