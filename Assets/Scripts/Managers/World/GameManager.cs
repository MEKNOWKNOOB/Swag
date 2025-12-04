using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkEntity
{
    public static GameManager Instance;

    [Header("GameManager")]
    public List<LocalPlayer> Players = new();
    public int Difficulty = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {

    }
}
