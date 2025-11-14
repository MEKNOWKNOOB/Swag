using System.Collections.Generic;
using UnityEngine.SocialPlatforms;

public class GameManager : NetworkEntity
{
    public static GameManager Instance;

    public List<LocalPlayer> Players = new();

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
