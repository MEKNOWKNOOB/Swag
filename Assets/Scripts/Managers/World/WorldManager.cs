using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldManager : NetworkEntity
{
    public static WorldManager Instance;

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
