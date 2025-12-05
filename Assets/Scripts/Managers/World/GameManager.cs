using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : NetworkEntity
{
    public static GameManager Instance;

    [Header("GameManager")]
    public List<LocalPlayer> Players = new();
    public int Difficulty = 0;

    public delegate void PlayerAddedHandler(LocalPlayer player);

    public event PlayerAddedHandler OnPlayerAdded;

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

    public void AddPlayer(LocalPlayer player)
    {
        Players.Add(player);

        OnPlayerAdded?.Invoke(player);
    }

    void Update()
    {

    }
}
