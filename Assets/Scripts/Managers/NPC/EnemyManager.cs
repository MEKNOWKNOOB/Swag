using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : NetworkEntity
{
    public static EnemyManager Instance;

    [Header("Initialization")]
    [SerializeField] private List<GameObject> enemyReferences = new();


    [Header("Active")]
    public List<Enemy> AllEnemies = new();
    [NonSerialized] public Dictionary<string, GameObject> EnemyReferences = new(); // use for precise enemy
    [NonSerialized] public List<List<Enemy>> EnemyContainer = new(); // use for random enemy, sorted by difficulty

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;


        foreach (GameObject enemyRefObj in enemyReferences)
        {
            Enemy enemy = enemyRefObj.GetComponent<Enemy>();

            EnemyReferences[enemy.Name] = enemyRefObj;

            int expandCotainer = enemy.Difficulty+1 - EnemyContainer.Count;
            for (int i = 0; i < expandCotainer; i++)
            {
                EnemyContainer.Add(new());
            }

            EnemyContainer[enemy.Difficulty].Add(enemy);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if(IsServer)
        {
            ((EnemySpawner)NetworkComponents["EnemySpawner"]).AttemptSpawn(5);
        }
    }
}
