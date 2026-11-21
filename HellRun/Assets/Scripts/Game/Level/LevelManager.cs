using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GroundMover groundMover;

    public ScreenFlow screenFlow;
    private MonsterGenerator monsterGenerator;
    private GameObject[] player;
    public int currentLevel = 0;
    public int currentSpeed = 1;

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
        UpdateRun();
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
